using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logisticsm.Repository.Entities
{
    public partial class ExpressTransport : ObservableObject
    {
        private int _sumCount = 0;
        [NotMapped]
        public int SumCount
        {
            get => _sumCount;
            set
            {
                SetProperty(ref _sumCount, value);
            }
        }

        private double _sumWeight = 0;
        [NotMapped]
        public double SumWeight
        {
            get => _sumWeight;
            set
            {
                SetProperty(ref _sumWeight, value);
                Weight = value;
            }
        }

        private double _costWeightEx = 0;
        [NotMapped]
        public double CostWeightEx
        {
            get => _costWeightEx;
            set
            {
                SetProperty(ref _costWeightEx, value);
                Weight = value;
            }
        }

        private double _sumVolume = 0;
        [NotMapped]
        public double SumVolume
        {
            get => _sumVolume;
            set
            {
                SetProperty(ref _sumVolume, value);
                Volume = value;
            }
        }

        private string _tagEx = string.Empty;
        [NotMapped]
        public string TagEx
        {
            get => _tagEx;
            set
            {
                SetProperty(ref _tagEx, value);
                Tag = value;
            }
        }

        private string _sourcePlaceEx = string.Empty;
        [NotMapped]
        public string SourcePlaceEx
        {
            get => _sourcePlaceEx;
            set
            {
                SetProperty(ref _sourcePlaceEx, value);
                SourcePlace = value;
            }
        }

        private string _targetPlaceEx = string.Empty;
        [NotMapped]
        public string TargetPlaceEx
        {
            get => _targetPlaceEx;
            set
            {
                SetProperty(ref _targetPlaceEx, value);
                TargetPlace = value;
            }
        }

        public void UpdateProperties()
        {
            if (ExpressTransportDetails != null)
            {
                //SumCount = (int)ExpressTransportDetails.Sum(t => t.Count);
                //SumWeight = (int)ExpressTransportDetails.Sum(t => t.Weight);
                //SumVolume = (int)ExpressTransportDetails.Sum(t => t.Volume);
            }
        }

        /// <summary>
        /// 渠道選項集合: DHL、FeDex、TNT、UPS、專線
        /// </summary>
        public List<string> Channels { get; } = new List<string> { "DHL", "FeDex", "TNT", "UPS", "專線" };

    }
}
