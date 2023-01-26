using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Entities;

[Table("AirTransportDetail")]
public partial class AirTransportDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("air_transport_id")]
    public int? AirTransportId { get; set; }

    [Column("member_id")]
    public int? MemberId { get; set; }

    [Column("receive_date", TypeName = "datetime")]
    public DateTime? ReceiveDate { get; set; }

    [Column("count")]
    public int? Count { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("volume")]
    public double? Volume { get; set; }

    [Column("length")]
    public double? Length { get; set; }

    [Column("width")]
    public double? Width { get; set; }

    [Column("height")]
    public double? Height { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(128)]
    public string? Tag { get; set; }
}
