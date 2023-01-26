using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL;
using Logisticsm.DAL.Models;
using Logisticsm.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Logisticsm.ViewModels
{
    public class AddAirTransportViewModel : ObservableObject
    {
        #region Fields

        private readonly CustomerProvider _customerProvider = new();

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

        private AirTransport _airTransport = new();
        /// <summary>
        /// 客戶
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
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    customers.ForEach(item => Customers.Add(item));
                });
            }
        }

        /// <summary>
        /// 按下關閉視窗
        /// </summary>
        public RelayCommand<AddAirTransportWindow> ClosedCommand
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
                });
            }
        }


        #endregion


    }
}
