using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Logisticsm.ViewModels.SeaTransports
{
    public class EditSeaTransportViewModel : ObservableObject
    {
        #region Fields

        private CustomerProvider _customerProvider = new();
        private SeaTransportProvider _seaTransportProvider = new();
        private SeaTransportDetailProvider _seaTransportDetailProvider = new();

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
        /// 空運單號
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
        /// 全部詳細空運單號
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
                    // 載入當前單號
                    SeaTransport = _seaTransportProvider.GetItemById(SeaTransport.Id);

                    // 載入全部客戶
                    var customers = _customerProvider.GetAll();
                    Customers.Clear();
                    foreach (var item in customers)
                    {
                        Customers.Add(item);
                    }

                    // 當前客戶
                    Customer = Customers.FirstOrDefault(t => t.Id == SeaTransport.CustomerId);

                    // 載入當前單號的詳細記錄
                    var SeaTransportDetails = _seaTransportDetailProvider.GetAll().FindAll(t => t.SeaTransportId == SeaTransport.Id);
                    SeaTransportDetails.Clear();
                    foreach (var item in SeaTransportDetails)
                    {
                        SeaTransportDetails.Add(item);
                        //item.SeaTransport = SeaTransport;
                    }

                    //SeaTransport.SeaTransportDetails = SeaTransportDetails;
                    SeaTransport.UpdateProperties();
                });
            }
        }

        /// <summary>
        /// 增加空運詳細記錄
        /// </summary>
        public RelayCommand<SeaTransportDetail> AddDetailCommand
        {
            get
            {
                return new RelayCommand<SeaTransportDetail>((e) =>
                {
                    SeaTransportDetail entity = new()
                    {
                        MemberId = AppData.Instance.CurrentUser.Id,
                        SeaTransportId = SeaTransport.Id,
                        ReceiveDate = DateTime.Now,
                        //SeaTransport = SeaTransport
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
        /// 儲存空運單號
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

                    //SeaTransport.UpdateProperties();

                    string message = count > 0 ? "操作成功" : "操作失敗";

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
                return new RelayCommand<SeaTransportDetail>((entity) =>
                {
                    if (entity == null) return;

                    if (MessageBox.Show($"Delete this [ {entity.Id} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var count = _seaTransportDetailProvider.Delete(entity);
                        if (count > 0)
                        {
                            SeaTransportDetails.Remove(entity);
                        }
                        SeaTransport.UpdateProperties();
                    }
                });
            }
        }

        #endregion

        #region Public Methods

        public void SetProvider(CustomerProvider customerProvider, SeaTransportProvider SeaTransportProvider, SeaTransportDetailProvider SeaTransportDetailProvider)
        {
            _customerProvider = customerProvider;
            _seaTransportProvider = SeaTransportProvider;
            _seaTransportDetailProvider = SeaTransportDetailProvider;
        }

        #endregion
    }
}
