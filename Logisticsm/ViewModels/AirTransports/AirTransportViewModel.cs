using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.AirTransports
{
    public class AirTransportViewModel : ObservableRecipient
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

        private ObservableCollection<AirTransport> _airTransports = new();
        /// <summary>
        /// 空運單號群
        /// </summary>
        public ObservableCollection<AirTransport> AirTransports
        {
            get => _airTransports;
            set
            {
                SetProperty(ref _airTransports, value);
            }
        }

        private ObservableCollection<AirTransportDetail> _airTransportDetails = new();
        /// <summary>
        /// 空軍詳細群
        /// </summary>
        public ObservableCollection<AirTransportDetail> AirTransportDetails
        {
            get => _airTransportDetails;
            set
            {
                SetProperty(ref _airTransportDetails, value);
            }
        }


        private AirTransport _airTransport = new();
        /// <summary>
        /// DataGrid 空運單號
        /// </summary>
        public AirTransport AirTransport
        {
            get => _airTransport;
            set
            {
                SetProperty(ref _airTransport, value);
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
                    Load(null);
                });
            }
        }

        /// <summary>
        /// 新增空運單號
        /// </summary>
        public RelayCommand InsertAirTransportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

                    AddAirTransportWindow addAirTransportWindow = new();
                    addAirTransportWindow.ShowDialog();
                    Load(null);

                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Collapsed;
                });
            }
        }

        /// <summary>
        /// 修改空運單號
        /// </summary>
        public RelayCommand DataGridMouseDoubleClickCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

                    EditAirTransportWindow editAirTransportWindow = new(_customerProvider, _airTransportProvider, _airTransportDetailProvider);
                    (editAirTransportWindow.DataContext as EditAirTransportViewModel).AirTransport = AirTransport;
                    (editAirTransportWindow.DataContext as EditAirTransportViewModel).SetProvider(_customerProvider, _airTransportProvider, _airTransportDetailProvider);
                    editAirTransportWindow.ShowDialog();
                    Load((editAirTransportWindow.DataContext as EditAirTransportViewModel).AirTransport);
                    AirTransport.UpdateProperties();

                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Collapsed;
                });
            }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 儲存當前修改的資料至資料庫
        /// </summary>
        public void Save()
        {
            _airTransportProvider.Save();
            _airTransportDetailProvider.Save();
        }

        public Task Load(AirTransport entity)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // 所有庫戶
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    foreach (var customer in customers)
                    {
                        Customers.Add(customer);
                    }

                    // 所有詳情單號
                    var airTransportDetails = _airTransportDetailProvider.GetAll();
                    AirTransportDetails.Clear();
                    foreach (var item in airTransportDetails)
                    {
                        AirTransportDetails.Add(item);
                    }

                    // 所有單號
                    var airTransports = _airTransportProvider.GetAll();
                    AirTransports.Clear();
                    foreach (var item in airTransports)
                    {
                        item.AirTransportDetails.Clear();
                        AirTransports.Add(item);

                        var children = AirTransportDetails.Where(t => t.AirTransportId == item.Id).ToList();
                        children.ForEach(child => item.AirTransportDetails.Add(child));

                        item.SumCount = (int)item.AirTransportDetails.Sum(t => t.Count);
                        item.SumWeight = (int)item.AirTransportDetails.Sum(t => t.Weight);
                        item.SumVolume = (int)item.AirTransportDetails.Sum(t => t.Volume);
                        item.Customer = Customers.FirstOrDefault(t => t.Id == item.CustomerId);
                    }

                });
            });
        }

        #endregion
    }
}
