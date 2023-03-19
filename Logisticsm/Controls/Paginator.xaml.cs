using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logisticsm.Controls
{
    /// <summary>
    /// Paginator.xaml 的互動邏輯
    /// </summary>
    public partial class Paginator : UserControl
    {
        public Paginator()
        {
            InitializeComponent();
        }

        public void MethodB()
        {
            try
            {
                // 執行一些操作
                MethodC();
            }
            catch (Exception ex)
            {
                // 處理例外
                Console.WriteLine($"MethodB caught exception: {ex.Message}");
                throw new ApplicationException("An error occurred in MethodB.", ex);
            }
        }

        public void MethodC()
        {
            try
            {
                // 執行一些操作
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                // 處理例外
                Console.WriteLine($"MethodC caught exception: {ex.Message}");
                throw new InvalidOperationException("An error occurred in MethodC.", ex);
            }
        }
    }
}
