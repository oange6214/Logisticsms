using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Logisticsm.Repository.Entities
{
    public partial class SeaTransportDetail : ObservableObject
    {
        ///// <summary>
        ///// 件數
        ///// </summary>
        //[NotMapped]
        //public int? CountEx
        //{
        //    get => Count;
        //    set
        //    {
        //        Count = value;
        //        RefreshAirTransport();
        //    }
        //}

        ///// <summary>
        ///// 重量
        ///// </summary>
        //[NotMapped]
        //public double? WeightEx
        //{
        //    get => Weight;
        //    set
        //    {
        //        Weight = value;
        //        RefreshAirTransport();

        //    }
        //}

        ///// <summary>
        ///// 體積
        ///// </summary>
        //[NotMapped]
        //public double? VolumeEx
        //{
        //    get => Volume;
        //    set
        //    {
        //        Volume = value;
        //        RefreshAirTransport();
        //    }
        //}

        #region Methods

        private void RefreshAirTransport()
        {
            SeaTransport.UpdateProperties();
        }

        #endregion
    }
}
