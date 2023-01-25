using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL;
using Logisticsm.DAL.Models;
using Logisticsm.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Logisticsm.ViewModels
{
	public class CustomerViewModel : ObservableObject
	{
		#region Fields

		private readonly CustomerProvider _customerProvider = new CustomerProvider();

		#endregion


		#region Properties

		private ObservableCollection<Customer> _customers = new();

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
					var customers = _customerProvider.GetAll();
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
                    var count = _customerProvider.Insert(customer);
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
						var count = _customerProvider.Delete(customer);
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

        public void Save()
        {
            _customerProvider.Save();
        }

        #endregion
    }
}
