using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL.Models;
using Logisticsm.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Logisticsm.ViewModels
{
	public class MainViewModel : ObservableObject
	{
		#region Properties

		public Member? CurrentUser { get; set; } = null!;

		//public ContentControl CurrentPage { get; set; } = null!;

		private ContentControl _currentPage;

		public ContentControl CurrentPage
		{
			get => _currentPage;
			set
			{
				SetProperty(ref _currentPage, value);
			}
		}


		#endregion

		public MainViewModel()
		{
			CurrentUser = AppData.Instance.CurrentUser;
			CurrentPage = App.ServiceProvider.GetRequiredService<AirTransportView>();
		}

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

						switch (button.Name)
						{
							case "AirTransportView":
								CurrentPage = App.ServiceProvider.GetRequiredService<AirTransportView>();
								break;
							case "CustomerView":
								CurrentPage = App.ServiceProvider.GetRequiredService<CustomerView>();
								break;
							default:
								break;
						}
					}
				});
				return command;
			}
		}

		#endregion
	}
}
