using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using System.Collections.ObjectModel;
using System.Windows;


namespace Logisticsm.ViewModels.ExpressTransports
{
    public class AddExpressTransportViewModel : ObservableRecipient
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();
        private readonly ExpressTransportProvider _expressTransportProvider = new();
        private readonly ExpressTransportDetailProvider _expressTransportDetailProvider = new();

        #endregion

        #region Properties

        private Customer _customer = null!;
        /// <summary>
        /// 客戶
        /// </summary>
        public Customer Customer
        {
            get => _customer;
            set
            {
                SetProperty(ref _customer, value);
            }
        }

        private ObservableCollection<Customer> _customers = new();
        /// <summary>
        /// 全部客戶
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                SetProperty(ref _customers, value);
            }
        }

        private ExpressTransport _expressTransport = new() { SendDate = DateTime.Now };
        /// <summary>
        /// 快遞單號
        /// </summary>
        public ExpressTransport ExpressTransport
        {
            get => _expressTransport;
            set
            {
                SetProperty(ref _expressTransport, value);
            }
        }

        private ObservableCollection<ExpressTransportDetail> _expressTransportDetails = new();
        /// <summary>
        /// 全部詳細快遞單號
        /// </summary>
        public ObservableCollection<ExpressTransportDetail> ExpressTransportDetails
        {
            get => _expressTransportDetails;
            set
            {
                SetProperty(ref _expressTransportDetails, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// 載入頁面時
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    customers.ForEach(item => Customers.Add(item));
                    ExpressTransport.ExpressTransportDetails = ExpressTransportDetails;
                });
            }
        }

        /// <summary>
        /// 按下一步 插入 ExpressTransport 物件到資料庫中
        /// </summary>
        public RelayCommand<AddExpressTransportWindow> NextStepCommand
        {
            get
            {
                return new RelayCommand<AddExpressTransportWindow>((window) =>
                {
                    if (Customer == null) return;
                    if (string.IsNullOrEmpty(ExpressTransport.TargetPlace)) return;
                    if (ExpressTransport?.SendDate == null) return;

                    window.firstGrid.Visibility = Visibility.Collapsed;
                    window.secondGrid.Visibility = Visibility.Visible;

                    ExpressTransport.CustomerId = _customers.FirstOrDefault(t => t.Name == Customer.Name).Id;
                    ExpressTransport.MemberId = AppData.Instance.CurrentUser.Id;
                    _expressTransportProvider.Insert(ExpressTransport);
                });
            }
        }

        /// <summary>
        /// 增加快遞詳細記錄
        /// </summary>
        public RelayCommand AddDetailCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ExpressTransportDetail entity = new()
                    {
                        MemberId = AppData.Instance.CurrentUser.Id,
                        ExpressTransportId = ExpressTransport.Id,
                        ReceiveDate = DateTime.Now,
                        ExpressTransport = ExpressTransport
                    };

                    var count = _expressTransportDetailProvider.Insert(entity);
                    if (count > 0)
                    {
                        ExpressTransportDetails.Add(entity);
                    }
                });
            }
        }

        /// <summary>
        /// 儲存快遞單號
        /// </summary>
        public RelayCommand<Window> SaveCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    int count = 0;
                    count += _expressTransportProvider.Save();
                    count += _expressTransportDetailProvider.Save();

                    string message = count >= 0 ? "操作成功" : "操作失敗";

                    MessageBox.Show(message);

                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 保存快遞單號並退出
        /// </summary>
        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    _expressTransportProvider.Save();
                    _expressTransportDetailProvider.Save();
                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 刪除一個項目
        /// </summary>
        public RelayCommand<ExpressTransportDetail> DeleteDetailCommand
        {
            get
            {
                return new RelayCommand<ExpressTransportDetail>((expressTransportDetail) =>
                {
                    if (expressTransportDetail == null) return;

                    if (MessageBox.Show($"Delete this [ {expressTransportDetail.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _expressTransportDetailProvider.Delete(expressTransportDetail);
                        if (count > 0)
                        {
                            ExpressTransportDetails.Remove(expressTransportDetail);
                        }
                        ExpressTransport.UpdateProperties();
                    }

                });
            }
        }

        #endregion
    }
}
