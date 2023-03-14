using Logisticsm.Repository.Providers;
using Logisticsm.ViewModels.AirTransports;
using System.Windows;

namespace Logisticsm.Windows
{
    public partial class EditAirTransportWindow : Window
    {
        public EditAirTransportWindow(CustomerProvider customerProvider, AirTransportProvider airTransportProvider, AirTransportDetailProvider airTransportDetailProvider)
        {
            InitializeComponent();

            var vm = DataContext as EditAirTransportViewModel;
            if (vm == null)
            {
                vm._customerProvider = customerProvider;
                vm._airTransportProvider = airTransportProvider;
                vm._airTransportDetailProvider = airTransportDetailProvider;
            }
        }
    }
}
