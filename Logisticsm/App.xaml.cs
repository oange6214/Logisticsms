using Logisticsm.Repository.Providers;
using Logisticsm.ViewModels;
using Logisticsm.ViewModels.AirTransports;
using Logisticsm.ViewModels.ExpressTransports;
using Logisticsm.ViewModels.SeaTransports;
using Logisticsm.Views;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Logisticsm;

public partial class App : Application
{
	public static IServiceProvider ServiceProvider { get; private set; } = null!;
    private readonly CustomerProvider _customerProvider = new();
    private readonly AirTransportProvider _airTransportProvider = new();
    private readonly AirTransportDetailProvider _airTransportDetailProvider = new();

    public App()
	{
		var host = Host.CreateDefaultBuilder()
            .ConfigureServices(ViewModelServices)
            .ConfigureServices(WindowServices)
			.Build();

		ServiceProvider = host.Services;
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		var window = ServiceProvider.GetRequiredService<LoginWindow>();
		window.Show();

		base.OnStartup(e);
	}

	private void WindowServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
	{
		services.AddSingleton<MainWindow>();
		services.AddSingleton<LoginWindow>();
        services.AddSingleton<CustomerView>();
        services.AddSingleton<AirTransportView>();
		services.AddTransient<AddAirTransportWindow>();
		services.AddTransient<EditAirTransportWindow>();
        services.AddSingleton<SeaTransportView>();
		//services.AddTransient<AddSeaTransportWindow>();
		//services.AddTransient<EditSeaTransportWindow>();
		services.AddSingleton<ExpressTransportView>();
		//services.AddTransient<AddExpressTransportWindow>();
		//services.AddTransient<EditExpressTransportWindow>();
	}

	private void ViewModelServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
	{
		services.AddSingleton<MainViewModel>();
		services.AddSingleton<LoginViewModel>();
        services.AddSingleton<CustomerViewModel>();
        services.AddSingleton<AirTransportViewModel>();
        services.AddTransient<AddAirTransportViewModel>(); 
		services.AddTransient<EditAirTransportViewModel>();
        services.AddSingleton<SeaTransportViewModel>();
        services.AddTransient<AddSeaTransportViewModel>();
        services.AddTransient<EditSeaTransportViewModel>();
		services.AddSingleton<ExpressTransportViewModel>();
		services.AddTransient<AddExpressTransportViewModel>();
		services.AddTransient<EditExpressTransportViewModel>();
	}
}