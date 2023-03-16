using Logisticsm.Repository.Providers;
using Logisticsm.ViewModels.ExpressTransports;
using System.Windows;

namespace Logisticsm.Windows
{
    public partial class EditExpressTransportWindow : Window
    {
        public EditExpressTransportWindow(CustomerProvider customerProvider, ExpressTransportProvider expressTransportProvider, ExpressTransportDetailProvider expressTransportDetailProvider)
        {
            InitializeComponent();

            var vm = DataContext as EditExpressTransportViewModel;
            if (vm != null)
            {
                vm._customerProvider = customerProvider;
                vm._expressTransportProvider = expressTransportProvider;
                vm._expressTransportDetailProvider = expressTransportDetailProvider;
            }
        }
    }
}
