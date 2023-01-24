using Microsoft.Extensions.DependencyInjection;

namespace Logisticsm.ViewModels
{
	public class ViewModelLocator
	{
		public MainViewModel Main => App.ServiceProvider.GetRequiredService<MainViewModel>();
		public LoginViewModel Login => App.ServiceProvider.GetRequiredService<LoginViewModel>();
		public CustomerViewModel Customer => App.ServiceProvider.GetRequiredService<CustomerViewModel>();
		public AirTransportViewModel AirTransport => App.ServiceProvider.GetRequiredService<AirTransportViewModel>();
	}
}
