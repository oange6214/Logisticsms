using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logisticsm.DAL;
using Logisticsm.DAL.Models;
using System.Collections.ObjectModel;

namespace Logisticsm.ViewModels
{
	public class CustomerViewModel : ObservableObject
	{
		#region Fields

		private CustomerProvider _customerProvider = new CustomerProvider();

		#endregion


		#region Properties

		private ObservableCollection<Customer> _customers;

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

		public RelayCommand LoadedCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					var list = _customerProvider.GetAll();
				});
			}
		}

		public RelayCommand InsertCustomerCommand
		{
			get
			{
				return new RelayCommand(() =>
				{
					Customer customer = new Customer();

				});
			}
		}

		#endregion
	}
}
