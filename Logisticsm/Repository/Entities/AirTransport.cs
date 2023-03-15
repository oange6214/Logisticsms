using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Entities;

[Table("AirTransport")]
[Index("CustomerId", Name = "IX_AirTransport_customer_id")]
public partial class AirTransport
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("member_id")]
    public int? MemberId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("send_date", TypeName = "datetime")]
    public DateTime? SendDate { get; set; }

    [Column("order_number")]
    [StringLength(64)]
    public string? OrderNumber { get; set; }

    [Column("batch")]
    [StringLength(64)]
    public string? Batch { get; set; }

    [Column("source_place")]
    [StringLength(50)]
    public string? SourcePlace { get; set; }

    [Column("target_place")]
    [StringLength(50)]
    public string? TargetPlace { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(512)]
    public string? Tag { get; set; }

    [InverseProperty("AirTransport")]
    public virtual ICollection<AirTransportDetail> AirTransportDetails { get; set; } = new List<AirTransportDetail>();

    [ForeignKey("CustomerId")]
    [InverseProperty("AirTransports")]
    public virtual Customer? Customer { get; set; }
}
