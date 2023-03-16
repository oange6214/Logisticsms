using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.AirTransports
{
    public class EditAirTransportViewModel : ObservableObject
    {
        #region Fields

        public CustomerProvider _customerProvider = null;
        public AirTransportProvider _airTransportProvider = null;
        public AirTransportDetailProvider _airTransportDetailProvider = null;

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

        private AirTransport _airTransport = new() { SendDate = DateTime.Now };
        /// <summary>
        /// 空運單號
        /// </summary>
        public AirTransport AirTransport
        {
            get => _airTransport;
            set
            {
                SetProperty(ref _airTransport, value);
            }
        }

        private ObservableCollection<AirTransportDetail> _airTransportDetails = new();
        /// <summary>
        /// 全部詳細空運單號
        /// </summary>
        public ObservableCollection<AirTransportDetail> AirTransportDetails
        {
            get => _airTransportDetails;
            set
            {
                SetProperty(ref _airTransportDetails, value);
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
                    AirTransport = _airTransportProvider.GetItemById(AirTransport.Id);

                    // 載入全部客戶
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    foreach (var item in customers)
                    {
                        Customers.Add(item);
                    }

                    // 當前客戶
                    Customer = Customers.FirstOrDefault(t => t.Id == AirTransport.CustomerId);

                    // 載入當前單號的詳細記錄
                    var airTransportDetails = _airTransportDetailProvider.GetAll().FindAll(t => t.AirTransportId == AirTransport.Id);
                    AirTransportDetails.Clear();
                    foreach (var item in airTransportDetails)
                    {
                        AirTransportDetails.Add(item);
                        item.AirTransport = AirTransport;
                    }

                    AirTransport.AirTransportDetails = AirTransportDetails;
                    AirTransport.UpdateProperties();
                });
            }
        }

        /// <summary>
        /// 增加空運詳細記錄
        /// </summary>
        public RelayCommand<AirTransportDetail> AddDetailCommand
        {
            get
            {
                return new RelayCommand<AirTransportDetail>((e) =>
                {
                    AirTransportDetail entity = new()
                    {
                        MemberId = AppData.Instance.CurrentUser.Id,
                        AirTransportId = AirTransport.Id,
                        ReceiveDate = DateTime.Now,
                        AirTransport = AirTransport
                    };

                    var count = _airTransportDetailProvider.Insert(entity);
                    if (count > 0)
                    {
                        AirTransportDetails.Add(entity);
                    }
                });
            }
        }

        /// <summary>
        /// 儲存空運單號
        /// </summary>
        public RelayCommand<Window> SaveCommand
        {
            get
            {
                return new RelayCommand<Window>((window) =>
                {
                    int count = 0;
                    count += _airTransportProvider.Save();
                    count += _airTransportDetailProvider.Save();

                    //AirTransport.UpdateProperties();

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
                    _airTransportProvider.Save();
                    _airTransportDetailProvider.Save();
                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 刪除一個項目
        /// </summary>
        public RelayCommand<AirTransportDetail> DeleteDetailCommand
        {
            get
            {
                return new RelayCommand<AirTransportDetail>((entity) =>
                {
                    if (entity == null) return;

                    if (MessageBox.Show($"Delete this [ {entity.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _airTransportDetailProvider.Delete(entity);
                        if (count > 0)
                        {
                            AirTransportDetails.Remove(entity);
                        }
                        AirTransport.UpdateProperties();
                    }
                });
            }
        }

        #endregion

        #region Public Methods

        public void SetProvider(CustomerProvider customerProvider, AirTransportProvider airTransportProvider, AirTransportDetailProvider airTransportDetailProvider)
        {
            _customerProvider = customerProvider;
            _airTransportProvider = airTransportProvider;
            _airTransportDetailProvider = airTransportDetailProvider;
        }

        #endregion
    }
}
