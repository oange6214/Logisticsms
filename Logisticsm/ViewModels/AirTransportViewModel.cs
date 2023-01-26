using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL;
using Logisticsm.DAL.Models;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

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

        /// <summary>
        /// 新增一個空運單號
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
        } 

        #endregion
    }
}
