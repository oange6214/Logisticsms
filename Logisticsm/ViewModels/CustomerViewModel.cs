using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.Repository.Entities;
using Logisticsm.Repository.Providers;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logisticsm.ViewModels
{
	public class CustomerViewModel : ObservableRecipient
    {
		#region Fields

		private readonly CustomerProvider _CustomerProvider = new();

		#endregion


		#region Properties

		private ObservableCollection<Customer> _customers = new();
		/// <summary>
		/// 客戶
		/// </summary>
		public ObservableCollection<Customer> Customers
		{
			get => _customers;
			set
			{
				SetProperty(ref _customers, value);
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
					var customers = _CustomerProvider.GetAll();
					Customers.Clear();
					customers.ForEach(item => Customers.Add(item));
				});
			}
		}

		/// <summary>
		/// 新增一位客戶
		/// </summary>
		public RelayCommand InsertCustomerCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					Customer customer = new() { Name = "新客戶", MemberId = AppData.Instance.CurrentUser.Id };
					var count = _CustomerProvider.Insert(customer);
					if (count > 0)
					{
						Customers.Add(customer);
					}
					else
					{
						MessageBox.Show("插入失敗");
					}
				});
			}
		}

		/// <summary>
		/// 刪除一位客戶
		/// </summary>
		public RelayCommand<Customer> DeleteCustomerCommand
		{
			get
			{
				return new RelayCommand<Customer>((customer) =>
				{
					if (customer == null) return;

					App.ServiceProvider.GetRequiredService<MainViewModel>().MamkerVisible = Visibility.Visible;

					if (MessageBox.Show($"Delete this [ {customer.Name} ]?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						var count = _CustomerProvider.Delete(customer);
						if (count > 0)
						{
							Customers.Remove(customer);
						}
					}

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
			_CustomerProvider.Save();
		}

		#endregion
	}
}
