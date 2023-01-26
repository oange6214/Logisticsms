namespace Logisticsm.Models
{
    public class AirTransportModel
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? SendDate { get; set; }

        public string? OrderNumber { get; set; }

        public string? Batch { get; set; }
        public int SumCount { get; set; }

        public string? SourcePlace { get; set; }

        public string? TargetPlace { get; set; }

        public DateTime? InsertDate { get; set; }

        public string? Tag { get; set; }
    }
}
