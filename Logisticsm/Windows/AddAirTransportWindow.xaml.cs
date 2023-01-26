using System.Windows;

namespace Logisticsm.Windows
{
    public partial class AddAirTransportWindow : Window
    {
        public AddAirTransportWindow()
        {
            InitializeComponent();

            firstGrid.Visibility= Visibility.Visible;
            secondGrid.Visibility= Visibility.Collapsed;
        }
    }
}
