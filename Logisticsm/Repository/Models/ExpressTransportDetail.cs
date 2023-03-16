using System.ComponentModel.DataAnnotations.Schema;

namespace Logisticsm.Repository.Entities
{
    public partial class ExpressTransportDetail
    {
        [NotMapped]
        public DateTime ReceiveDateEx
        {
            get => ReceiveDate;
            set
            {
                ReceiveDate = value;
            }
        }
    }
}
