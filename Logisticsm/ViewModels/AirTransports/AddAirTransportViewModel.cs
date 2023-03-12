using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.AirTransports
{
    public class AddAirTransportViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();
        private readonly AirTransportProvider _airTransportProvider = new();
        private readonly AirTransportDetailProvider _airTransportDetailProvider = new();

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
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    customers.ForEach(item => Customers.Add(item));
                    AirTransport.AirTransportDetails = AirTransportDetails;
                });
            }
        }

        /// <summary>
        /// 按下一步
        /// </summary>
        public RelayCommand<AddAirTransportWindow> NextStepCommand
        {
            get
            {
                return new RelayCommand<AddAirTransportWindow>((window) =>
                {
                    if (Customer == null) return;
                    if (string.IsNullOrEmpty(AirTransport.TargetPlace)) return;
                    if (AirTransport?.SendDate == null) return;

                    window.firstGrid.Visibility = Visibility.Collapsed;
                    window.secondGrid.Visibility = Visibility.Visible;

                    AirTransport.CustomerId = _customers.FirstOrDefault(t => t.Name == Customer.Name)?.Id;
                    AirTransport.MemberId = AppData.Instance.CurrentUser.Id;
                    _airTransportProvider.Insert(AirTransport);
                });
            }
        }

        /// <summary>
        /// 增加空運詳細記錄
        /// </summary>
        public RelayCommand AddDetailCommand
        {
            get
            {
                return new RelayCommand(() =>
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

                    string message = count > 0 ? "操作成功" : "操作失敗";

                    MessageBox.Show(message);

                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 按下關閉鈕
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
                return new RelayCommand<AirTransportDetail>((airTransportDetail) =>
                {
                    if (airTransportDetail == null) return;

                    if (MessageBox.Show($"Delete this [ {airTransportDetail.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _airTransportDetailProvider.Delete(airTransportDetail);
                        if (count > 0)
                        {
                            AirTransportDetails.Remove(airTransportDetail);
                        }
                        AirTransport.UpdateProperties();
                    }

                });
            }
        }

        #endregion
    }
}
