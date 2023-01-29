using System.ComponentModel.DataAnnotations.Schema;

namespace Logisticsm.Repository.Entities
{
    public partial class AirTransportDetail
    {
        /// <summary>
        /// 件數
        /// </summary>
        [NotMapped]
        public int? CountEx
        {
            get => Count;
            set
            {
                Count = value;
                RefreshAirTransport();
            }
        }

        /// <summary>
        /// 重量
        /// </summary>
        [NotMapped]
        public double? WeightEx
        {
            get => Weight;
            set
            {
                Weight = value;
                RefreshAirTransport();

            }
        }

        /// <summary>
        /// 體積
        /// </summary>
        [NotMapped]
        public double? VolumeEx
        {
            get => Volume;
            set
            {
                Volume = value;
                RefreshAirTransport();
            }
        }

        /// <summary>
        /// 長
        /// </summary>
        [NotMapped]
        public double? LengthEx
        {
            get => Length;
            set
            {
                Length = value;
                RefreshAirTransport();
            }
        }

        /// <summary>
        /// 寬
        /// </summary>
        [NotMapped]
        public double? WidthEx
        {
            get => Width;
            set
            {
                Width = value;
                RefreshAirTransport();
            }
        }

        /// <summary>
        /// 高
        /// </summary>
        [NotMapped]
        public double? HeightEx
        {
            get => Height;
            set
            {
                Height = value;
                RefreshAirTransport();
            }
        }

        #region Methods

        private void RefreshAirTransport()
        {
            AirTransport.UpdateProperties();
        }

        #endregion

    }
}
