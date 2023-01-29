using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logisticsm.Repository.Entities
{
    public partial class AirTransport : ObservableObject
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
            }
        }

        private double _sumDensity = 0;
        [NotMapped]
        public double SumDensity
        {
            get => _sumDensity;
            set
            {
                SetProperty(ref _sumDensity, value);
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
                Tag = _tagEx;
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
                SourcePlace = _sourcePlaceEx;
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
                TargetPlace = _targetPlaceEx;
            }
        }


        public void UpdateProperties()
        {
            if (AirTransportDetails != null)
            {
                SumCount = (int)AirTransportDetails.Sum(t => t.Count);
                SumWeight = (int)AirTransportDetails.Sum(t => t.Weight);
                SumVolume = (int)AirTransportDetails.Sum(t => t.Volume);
                SumDensity = SumWeight / SumVolume;

                TagEx = string.Empty;
                foreach (var item in AirTransportDetails)
                {
                    TagEx += $"{item.Length} * {item.Width} * {item.Height} / {item.Count} \r";
                }
            }
        }
    }
}
