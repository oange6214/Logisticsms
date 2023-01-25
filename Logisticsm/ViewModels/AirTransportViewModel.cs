using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL;
using Logisticsm.DAL.Models;
using System.Collections.ObjectModel;

namespace Logisticsm.ViewModels
{
    public class AirTransportViewModel : ObservableObject
    {
        #region Fields

        private readonly AirTransportProvider _airTransportProvider = new();

        #endregion




        #region Properties

        private ObservableCollection<AirTransport> _airTransports = new();

        public ObservableCollection<AirTransport> AirTransports
        {
            get => _airTransports;
            set
            {
                SetProperty(ref _airTransports, value);
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
                    var airTransports = _airTransportProvider.GetAll();
                    AirTransports.Clear();
                    airTransports.ForEach(item => AirTransports.Add(item));
                });
            }
        }



        ///// <summary>
        ///// 新增一位客戶
        ///// </summary>
        //public RelayCommand InsertCustomerCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(() =>
        //        {
        //            Customer customer = new() { Name = "新客戶" };
        //            var count = _customerProvider.Insert(customer);
        //            if (count > 0)
        //            {
        //                Customers.Add(customer);
        //            }
        //            else
        //            {
        //                MessageBox.Show("插入失敗");
        //            }
        //        });
        //    }
        //}

        #endregion



        #region Public Methods

        public void Save()
        {
            _airTransportProvider.Save();
        } 

        #endregion
    }
}
