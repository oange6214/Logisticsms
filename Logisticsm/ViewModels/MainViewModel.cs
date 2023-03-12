using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.ViewModels.AirTransports;
using Logisticsm.ViewModels.SeaTransports;
using Logisticsm.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace Logisticsm.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        #region Properties

        public Member? CurrentUser { get; set; } = null!;


        private ContentControl _currentPage = null!;
        /// <summary>
        /// 目前頁面
        /// </summary>
        public ContentControl CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }

        private Visibility _mamkerVisible = Visibility.Collapsed;
        /// <summary>
        /// 主頁面遮罩
        /// </summary>
        public Visibility MamkerVisible
        {
            get => _mamkerVisible;
            set
            {
                SetProperty(ref _mamkerVisible, value);
            }
        }

        private DateTime _systemTime = DateTime.Now;
        /// <summary>
        /// 系統時間
        /// </summary>
        public DateTime SystemTime
        {
            get => _systemTime;
            set
            {
                SetProperty(ref _systemTime, value);
            }
        }

        #endregion


        #region Ctors

        public MainViewModel()
        {
            CurrentUser = AppData.Instance.CurrentUser;
            CurrentPage = App.ServiceProvider.GetRequiredService<AirTransportView>();

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    SystemTime = DateTime.Now;
                }
            });
        }

        #endregion


        #region Commands

        public RelayCommand<object> MenuCommand
        {
            get
            {
                var command = new RelayCommand<object>((sender) =>
                {
                    if (sender is RadioButton button)
                    {
                        if (string.IsNullOrEmpty(button.Name)) return;

                        if (button.Name == CurrentPage.GetType().Name) return;

                        Save(CurrentPage);

                        SetCurrentPage(button.Name);
                    }
                });
                return command;
            }
        }

        /// <summary>
        /// 關閉頁面時
        /// </summary>
        public RelayCommand ClosingCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Save(CurrentPage);
                });
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// 設定現在頁面
        /// </summary>
        /// <param name="pageType">頁面類型</param>
        private void SetCurrentPage(string pageType)
        {
            switch (pageType)
            {
                case "AirTransportView":
                    CurrentPage = App.ServiceProvider.GetRequiredService<AirTransportView>();

                    break;
                case "CustomerView":
                    CurrentPage = App.ServiceProvider.GetRequiredService<CustomerView>();
                    break;
                case "SeaTransportView":
                    CurrentPage = App.ServiceProvider.GetRequiredService<SeaTransportView>();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 儲存頁面資料
        /// </summary>
        /// <param name="currentPage">現在頁面物件</param>
		private void Save(ContentControl currentPage)
        {
            string pageType = currentPage.GetType().Name;

            switch (pageType)
            {
                case "AirTransportView":
                    if (currentPage is not AirTransportView) return;
                    if (currentPage.DataContext is not AirTransportViewModel airTransportViewModel) return;
                    airTransportViewModel.Save();
                    break;
                case "CustomerView":
                    if (currentPage is not CustomerView) return;
                    if (currentPage.DataContext is not CustomerViewModel customerViewModel) return;
                    customerViewModel.Save();
                    break;
                case "SeaTransportView":
                    if (currentPage is not SeaTransportView) return;
                    if (currentPage.DataContext is not SeaTransportViewModel seaTransportViewModel) return;
                    seaTransportViewModel.Save();
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
