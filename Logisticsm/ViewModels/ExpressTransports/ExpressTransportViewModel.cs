using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels.ExpressTransports
{
    public class ExpressTransportViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();
        private readonly ExpressTransportProvider _expressTransportProvider = new();
        private readonly ExpressTransportDetailProvider _expressTransportDetailProvider = new();

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

        private ObservableCollection<ExpressTransport> _expressTransports = new();
        /// <summary>
        /// 快遞單號群
        /// </summary>
        public ObservableCollection<ExpressTransport> ExpressTransports
        {
            get => _expressTransports;
            set
            {
                SetProperty(ref _expressTransports, value);
            }
        }

        private ObservableCollection<ExpressTransportDetail> _expressTransportDetails = new();
        /// <summary>
        /// 空軍詳細群
        /// </summary>
        public ObservableCollection<ExpressTransportDetail> ExpressTransportDetails
        {
            get => _expressTransportDetails;
            set
            {
                SetProperty(ref _expressTransportDetails, value);
            }
        }


        private ExpressTransport _expressTransport = new();
        /// <summary>
        /// DataGrid 快遞單號
        /// </summary>
        public ExpressTransport ExpressTransport
        {
            get => _expressTransport;
            set
            {
                SetProperty(ref _expressTransport, value);
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
        /// 新增快遞單號
        /// </summary>
        public RelayCommand InsertExpressTransportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

                    AddExpressTransportWindow addExpressTransportWindow = new();
                    addExpressTransportWindow.ShowDialog();
                    Load(null);

                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Collapsed;
                });
            }
        }

        /// <summary>
        /// 修改快遞單號
        /// </summary>
        public RelayCommand DataGridMouseDoubleClickCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

                    EditExpressTransportWindow editExpressTransportWindow = new EditExpressTransportWindow(_customerProvider, _expressTransportProvider, _expressTransportDetailProvider);
                    (editExpressTransportWindow.DataContext as EditExpressTransportViewModel).ExpressTransport = ExpressTransport;
                    (editExpressTransportWindow.DataContext as EditExpressTransportViewModel).SetProvider(_customerProvider, _expressTransportProvider, _expressTransportDetailProvider);
                    editExpressTransportWindow.ShowDialog();
                    Load((editExpressTransportWindow.DataContext as EditExpressTransportViewModel).ExpressTransport);
                    //ExpressTransport.UpdateProperties();

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
            _expressTransportProvider.Save();
            _expressTransportDetailProvider.Save();
        }

        public Task Load(ExpressTransport? entity)
        {
            return Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    //// 所有庫戶
                    //Customers.Clear();
                    //_customerProvider.GetAll().ForEach(c => Customers.Add(c));

                    //// 所有詳情單號
                    //ExpressTransportDetails.Clear();
                    //_expressTransportDetailProvider.GetAll().ForEach(e => ExpressTransportDetails.Add(e));

                    // 所有庫戶
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    foreach (var customer in customers)
                    {
                        Customers.Add(customer);
                    }

                    // 所有詳情單號
                    var airTransportDetails = _expressTransportDetailProvider.GetAll();
                    ExpressTransportDetails.Clear();
                    foreach (var item in airTransportDetails)
                    {
                        ExpressTransportDetails.Add(item);
                    }


                    // 所有單號
                    ExpressTransports.Clear();
                    foreach (var item in _expressTransportProvider.GetAll())
                    {
                        item.ExpressTransportDetails.Clear();
                        ExpressTransports.Add(item);

                        List<ExpressTransportDetail> children = ExpressTransportDetails.Where(t => t.ExpressTransportId == item.Id).ToList();
                        children.ForEach(child => item.ExpressTransportDetails.Add(child));

                        //item.SumCount = (int)item.ExpressTransportDetails.Sum(t => t.Count);
                        //item.SumWeight = (int)item.ExpressTransportDetails.Sum(t => t.Weight);
                        //item.SumVolume = (int)item.ExpressTransportDetails.Sum(t => t.Volume);
                        item.Customer = Customers.FirstOrDefault(t => t.Id == item.CustomerId);
                    }

                });
            });
        }

        #endregion
    }
}
