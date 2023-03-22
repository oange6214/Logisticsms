using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.SeaTransports
{
    public class AddSeaTransportViewModel : ObservableRecipient
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();
        private readonly SeaTransportProvider _seaTransportProvider = new();
        private readonly SeaTransportDetailProvider _seaTransportDetailProvider = new();

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

        private SeaTransport _seaTransport = new() { SendDate = DateTime.Now };
        /// <summary>
        /// 海運單號
        /// </summary>
        public SeaTransport SeaTransport
        {
            get => _seaTransport;
            set
            {
                SetProperty(ref _seaTransport, value);
            }
        }

        private ObservableCollection<SeaTransportDetail> _seaTransportDetails = new();
        /// <summary>
        /// 全部詳細海運單號
        /// </summary>
        public ObservableCollection<SeaTransportDetail> SeaTransportDetails
        {
            get => _seaTransportDetails;
            set
            {
                SetProperty(ref _seaTransportDetails, value);
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
                    SeaTransport.SeaTransportDetails = SeaTransportDetails;
                });
            }
        }

        /// <summary>
        /// 按下一步 插入 SeaTransport 物件到資料庫中
        /// </summary>
        public RelayCommand<AddSeaTransportWindow> NextStepCommand
        {
            get
            {
                return new RelayCommand<AddSeaTransportWindow>((window) =>
                {
                    if (Customer == null) return;
                    if (string.IsNullOrEmpty(SeaTransport.TargetPlace)) return;
                    if (SeaTransport?.SendDate == null) return;

                    window.firstGrid.Visibility = Visibility.Collapsed;
                    window.secondGrid.Visibility = Visibility.Visible;

                    SeaTransport.CustomerId = _customers.FirstOrDefault(t => t.Name == Customer.Name).Id;
                    SeaTransport.MemberId = AppData.Instance.CurrentUser.Id;
                    _seaTransportProvider.Insert(SeaTransport);
                });
            }
        }

        /// <summary>
        /// 增加海運詳細記錄
        /// </summary>
        public RelayCommand AddDetailCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SeaTransportDetail entity = new()
                    {
                        MemberId = AppData.Instance.CurrentUser.Id,
                        SeaTransportId = SeaTransport.Id,
                        ReceiveDate = DateTime.Now,
                        SeaTransport = SeaTransport
                    };

                    var count = _seaTransportDetailProvider.Insert(entity);
                    if (count > 0)
                    {
                        SeaTransportDetails.Add(entity);
                    }
                });
            }
        }

        /// <summary>
        /// 儲存海運單號
        /// </summary>
        public RelayCommand<Window> SaveCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    int count = 0;
                    count += _seaTransportProvider.Save();
                    count += _seaTransportDetailProvider.Save();

                    string message = count >= 0 ? "操作成功" : "操作失敗";

                    MessageBox.Show(message);

                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 保存海運單號並退出
        /// </summary>
        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    _seaTransportProvider.Save();
                    _seaTransportDetailProvider.Save();
                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 刪除一個項目
        /// </summary>
        public RelayCommand<SeaTransportDetail> DeleteDetailCommand
        {
            get
            {
                return new RelayCommand<SeaTransportDetail>((seaTransportDetail) =>
                {
                    if (seaTransportDetail == null) return;

                    if (MessageBox.Show($"Delete this [ {seaTransportDetail.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _seaTransportDetailProvider.Delete(seaTransportDetail);
                        if (count > 0)
                        {
                            SeaTransportDetails.Remove(seaTransportDetail);
                        }
                        SeaTransport.UpdateProperties();
                    }

                });
            }
        }

        #endregion
    }
}
