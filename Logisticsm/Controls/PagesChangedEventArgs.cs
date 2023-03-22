using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logisticsm.Controls
{
    public class PagesChangedEventArgs : EventArgs
    {
        public int Page { get; private set; }

        public PagesChangedEventArgs(int page)
        {
            Page = page;
        }
    }
}
