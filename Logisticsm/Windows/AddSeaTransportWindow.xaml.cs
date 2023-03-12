using System.Windows;

namespace Logisticsm.Windows
{
    public partial class AddSeaTransportWindow : Window
    {
        public AddSeaTransportWindow()
        {
            InitializeComponent();

            firstGrid.Visibility = Visibility.Visible;
            secondGrid.Visibility = Visibility.Collapsed;
        }
    }
}
