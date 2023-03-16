using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.ExpressTransports
{
    public class EditExpressTransportViewModel : ObservableObject
    {
        #region Fields

        public CustomerProvider _customerProvider = null;
        public ExpressTransportProvider _expressTransportProvider = null;
        public ExpressTransportDetailProvider _expressTransportDetailProvider = null;

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
                    // 載入當前單號
                    ExpressTransport = _expressTransportProvider.GetItemById(ExpressTransport.Id);

                    // 載入全部客戶
                    Customers.Clear();
                    _customerProvider.GetAll().ForEach(cust => Customers.Add(cust));

                    // 當前客戶
                    Customer = Customers.FirstOrDefault(t => t.Id == ExpressTransport.CustomerId);

                    // 載入當前單號的詳細記錄
                    List<ExpressTransportDetail> expressTransportDetails = _expressTransportDetailProvider.GetAll().FindAll(t => t.ExpressTransportId == ExpressTransport.Id);
                    ExpressTransportDetails.Clear();
                    foreach (var item in expressTransportDetails)
                    {
                        ExpressTransportDetails.Add(item);
                        item.ExpressTransport = ExpressTransport;
                    }

                    ExpressTransport.ExpressTransportDetails = ExpressTransportDetails;
                    //ExpressTransport.UpdateProperties();
                });
            }
        }

        /// <summary>
        /// 增加快遞詳細記錄
        /// </summary>
        public RelayCommand<ExpressTransportDetail> AddDetailCommand
        {
            get
            {
                return new RelayCommand<ExpressTransportDetail>((e) =>
                {
                    ExpressTransportDetail entity = new()
                    {
                        MemberId = AppData.Instance.CurrentUser.Id,
                        ExpressTransportId = ExpressTransport.Id,
                        ReceiveDate = DateTime.Now,
                        //ExpressTransport = ExpressTransport
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

                    //ExpressTransport.UpdateProperties();

                    string message = count >= 0 ? "操作成功" : "操作失敗";

                    MessageBox.Show(message);

                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 儲存並關閉
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
                return new RelayCommand<ExpressTransportDetail>((entity) =>
                {
                    if (entity == null) return;

                    if (MessageBox.Show($"Delete this [ {entity.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _expressTransportDetailProvider.Delete(entity);
                        if (count > 0)
                        {
                            ExpressTransportDetails.Remove(entity);
                        }
                    }
                });
            }
        }

        #endregion

        #region Public Methods

        public void SetProvider(CustomerProvider customerProvider, ExpressTransportProvider ExpressTransportProvider, ExpressTransportDetailProvider ExpressTransportDetailProvider)
        {
            _customerProvider = customerProvider;
            _expressTransportProvider = ExpressTransportProvider;
            _expressTransportDetailProvider = ExpressTransportDetailProvider;
        }

        #endregion
    }
}
