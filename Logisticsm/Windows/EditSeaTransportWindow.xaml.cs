using Logisticsm.Repository.Providers;
using Logisticsm.ViewModels.SeaTransports;
using System.Windows;

namespace Logisticsm.Windows
{
    public partial class EditSeaTransportWindow : Window
    {
        public EditSeaTransportWindow(CustomerProvider customerProvider, SeaTransportProvider seaTransportProvider, SeaTransportDetailProvider seaTransportDetailProvider)
        {
            InitializeComponent();

            var vm = DataContext as EditSeaTransportViewModel;

            if (vm != null)
            {
                vm._customerProvider = customerProvider;
                vm._seaTransportProvider = seaTransportProvider;
                vm._seaTransportDetailProvider = seaTransportDetailProvider;
            }
        }
    }
}
