using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Models;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels
{
    public class AddAirTransportViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();
        private readonly AirTransportProvider _airTransportProvider = new();
        private readonly AirTransportDetailProvider _airTransportDetailProvider = new();

        #endregion

        #region Properties

        private ObservableCollection<Customer> _customers = new();
        /// <summary>
        /// 客戶群
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                SetProperty(ref _customers, value);
            }
        }

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

        private AirTransportModel _airTransport = new() { SendDate = DateTime.Now };
        /// <summary>
        /// 空運單號
        /// </summary>
        public AirTransportModel AirTransport
        {
            get => _airTransport;
            set
            {
                SetProperty(ref _airTransport, value);
            }
        }


        private ObservableCollection<AirTransportDetail> _airTransportDetails = new();
        /// <summary>
        /// 詳細空運單號
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
                });
            }
        }

        /// <summary>
        /// 按下關閉視窗
        /// </summary>
        public RelayCommand<AddAirTransportWindow> CloseWindowCommand
        {
            get
            {
                return new RelayCommand<AddAirTransportWindow>((window) =>
                {

                    window?.Close();
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

                    _airTransportProvider.Insert(AirTransport);
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
                    AirTransportDetail airTransportDetail = new()
                    {
                        MemberId = AppData.Instance.CurrentUser.Id,
                        AirTransportId = AirTransport.Id
                    };
                    var count = _airTransportDetailProvider.Insert(airTransportDetail);
                    if (count > 0)
                    {
                        AirTransportDetails.Add(airTransportDetail);
                    }
                });
            }
        }

        /// <summary>
        /// 儲存空運單號
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _airTransportProvider.Save();
                    _airTransportDetailProvider.Save();
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
                    window?.Close();
                });
            }
        }

        /// <summary>
        /// 刪除一個項目
        /// </summary>
        public RelayCommand<AirTransportDetail> DeleteCustomerCommand
        {
            get
            {
                return new RelayCommand<AirTransportDetail>((airTransportDetail) =>
                {
                    if (airTransportDetail == null) return;

                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

                    if (MessageBox.Show($"Delete this [ {airTransportDetail.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _airTransportDetailProvider.Delete(airTransportDetail);
                        if (count > 0)
                        {
                            AirTransportDetails.Remove(airTransportDetail);
                        }
                    }

                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Collapsed;
                });
            }
        }



        #endregion


    }
}
