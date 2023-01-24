using Logisticsm.ViewModels;
using Logisticsm.Views;
using Logisticsm.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Logisticsm;

public partial class App : Application
{
	public static IServiceProvider ServiceProvider { get; private set; }

	public App()
	{
		var host = Host.CreateDefaultBuilder()
			.ConfigureServices(WindowServices)
			.ConfigureServices(ViewModelServices)
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
		services.AddSingleton<AirTransportView>();
		services.AddSingleton<CustomerView>();
	}

	private void ViewModelServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
	{
		services.AddSingleton<MainViewModel>();
		services.AddSingleton<LoginViewModel>();
		services.AddSingleton<AirTransportViewModel>();
		services.AddSingleton<CustomerViewModel>();
	}
}