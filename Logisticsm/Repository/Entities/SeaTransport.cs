using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logisticsm.Repository.Entities;

[Table("SeaTransport")]
public partial class SeaTransport
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("send_date", TypeName = "datetime")]
    public DateTime? SendDate { get; set; }

    [Column("box_model")]
    [StringLength(64)]
    public string? BoxModel { get; set; }

    [Column("box_number")]
    [StringLength(64)]
    public string? BoxNumber { get; set; }

    [Column("batch")]
    [StringLength(64)]
    public string? Batch { get; set; }

    [Column("count")]
    public int? Count { get; set; }

    [Column("volume")]
    public double? Volume { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("source_place")]
    [StringLength(50)]
    public string? SourcePlace { get; set; }

    [Column("target_place")]
    [StringLength(50)]
    public string? TargetPlace { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(128)]
    public string? Tag { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("SeaTransports")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("SeaTransport")]
    public virtual ICollection<SeaTransportDetail> SeaTransportDetails { get; set; } = new List<SeaTransportDetail>();
}
