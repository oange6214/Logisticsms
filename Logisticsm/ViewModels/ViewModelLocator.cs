using Logisticsm.ViewModels.AirTransports;
using Logisticsm.ViewModels.SeaTransports;
using Microsoft.Extensions.DependencyInjection;

namespace Logisticsm.ViewModels
{
    public class ViewModelLocator
	{
		public MainViewModel Main => App.ServiceProvider.GetRequiredService<MainViewModel>();
		public LoginViewModel Login => App.ServiceProvider.GetRequiredService<LoginViewModel>();
		public CustomerViewModel Customer => App.ServiceProvider.GetRequiredService<CustomerViewModel>();
		public AirTransportViewModel AirTransport => App.ServiceProvider.GetRequiredService<AirTransportViewModel>();
		public AddAirTransportViewModel AddAirTransport => App.ServiceProvider.GetRequiredService<AddAirTransportViewModel>();
		public EditAirTransportViewModel EditAirTransport => App.ServiceProvider.GetRequiredService<EditAirTransportViewModel>();
        public SeaTransportViewModel SeaTransport => App.ServiceProvider.GetRequiredService<SeaTransportViewModel>();
		public AddSeaTransportViewModel AddSeaTransport => App.ServiceProvider.GetRequiredService<AddSeaTransportViewModel>();
		public EditSeaTransportViewModel EditSeaTransport => App.ServiceProvider.GetRequiredService<EditSeaTransportViewModel>();
	}
}
