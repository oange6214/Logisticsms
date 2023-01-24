using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.DAL.Models;

[Keyless]
[Table("ExpressTransport")]
public partial class ExpressTransport
{
    [Column("id")]
    public int Id { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("send_date", TypeName = "datetime")]
    public DateTime SendDate { get; set; }

    [Column("order_number")]
    [StringLength(64)]
    public string OrderNumber { get; set; } = null!;

    [Column("channel")]
    [StringLength(64)]
    public string Channel { get; set; } = null!;

    [Column("count")]
    public int Count { get; set; }

    [Column("volume")]
    public double Volume { get; set; }

    [Column("weight")]
    public double Weight { get; set; }

    [Column("cost_weight")]
    public double CostWeight { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("source_place")]
    [StringLength(50)]
    public string SourcePlace { get; set; } = null!;

    [Column("target_place")]
    [StringLength(50)]
    public string TargetPlace { get; set; } = null!;

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(512)]
    public string? Tag { get; set; }
}
