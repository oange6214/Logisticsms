using System.Windows;

namespace Logisticsm.Windows
{
    public partial class AddExpressTransportWindow : Window
    {
        public AddExpressTransportWindow()
        {
            InitializeComponent();

            firstGrid.Visibility = Visibility.Visible;
            secondGrid.Visibility = Visibility.Collapsed;
        }
    }
}
