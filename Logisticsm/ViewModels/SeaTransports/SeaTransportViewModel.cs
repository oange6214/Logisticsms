using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.SeaTransports
{
    public class SeaTransportViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();
        private readonly SeaTransportProvider _seaTransportProvider = new();
        private readonly SeaTransportDetailProvider _seaTransportDetailProvider = new();

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

        private ObservableCollection<SeaTransport> _seaTransports = new();
        /// <summary>
        /// 空運單號群
        /// </summary>
        public ObservableCollection<SeaTransport> SeaTransports
        {
            get => _seaTransports;
            set
            {
                SetProperty(ref _seaTransports, value);
            }
        }

        private ObservableCollection<SeaTransportDetail> _seaTransportDetails = new();
        /// <summary>
        /// 空軍詳細群
        /// </summary>
        public ObservableCollection<SeaTransportDetail> SeaTransportDetails
        {
            get => _seaTransportDetails;
            set
            {
                SetProperty(ref _seaTransportDetails, value);
            }
        }


        private SeaTransport _seaTransport = new();
        /// <summary>
        /// DataGrid 空運單號
        /// </summary>
        public SeaTransport SeaTransport
        {
            get => _seaTransport;
            set
            {
                SetProperty(ref _seaTransport, value);
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
        public RelayCommand InsertSeaTransportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

                    AddSeaTransportWindow addSeaTransportWindow = new();
                    addSeaTransportWindow.ShowDialog();
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

                    EditSeaTransportWindow editSeaTransportWindow = new(_customerProvider, _seaTransportProvider, _seaTransportDetailProvider);
                    (editSeaTransportWindow.DataContext as EditSeaTransportViewModel).SeaTransport = SeaTransport;
                    (editSeaTransportWindow.DataContext as EditSeaTransportViewModel).SetProvider(_customerProvider, _seaTransportProvider, _seaTransportDetailProvider);
                    editSeaTransportWindow.ShowDialog();
                    Load((editSeaTransportWindow.DataContext as EditSeaTransportViewModel).SeaTransport);
                    SeaTransport.UpdateProperties();

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
            _seaTransportProvider.Save();
            _seaTransportDetailProvider.Save();
        }

        public Task Load(SeaTransport? entity)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    // 所有庫戶
                    Customers.Clear();
                    foreach (var customer in _customerProvider.GetAll())
                    {
                        Customers.Add(customer);
                    }

                    // 所有詳情單號
                    SeaTransportDetails.Clear();
                    foreach (var item in _seaTransportDetailProvider.GetAll())
                    {
                        SeaTransportDetails.Add(item);
                    }

                    // 所有單號
                    SeaTransports.Clear();
                    foreach (var item in _seaTransportProvider.GetAll())
                    {
                        item.SeaTransportDetails.Clear();
                        SeaTransports.Add(item);

                        List<SeaTransportDetail> children = SeaTransportDetails.Where(t => t.SeaTransportId == item.Id).ToList();
                        children.ForEach(child => item.SeaTransportDetails.Add(child));

                        //item.SumCount = (int)item.SeaTransportDetails.Sum(t => t.Count);
                        //item.SumWeight = (int)item.SeaTransportDetails.Sum(t => t.Weight);
                        //item.SumVolume = (int)item.SeaTransportDetails.Sum(t => t.Volume);
                        item.Customer = Customers.FirstOrDefault(t => t.Id == item.CustomerId);
                    }

                });
            });
        }

        #endregion
    }
}
