using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.DAL.Models;

[Keyless]
[Table("SeaTransportDetail")]
public partial class SeaTransportDetail
{
    [Column("id")]
    public int Id { get; set; }

    [Column("sea_transport_id")]
    public int SeaTransportId { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("receive_date", TypeName = "datetime")]
    public DateTime ReceiveDate { get; set; }

    [Column("productor")]
    [StringLength(64)]
    public string Productor { get; set; } = null!;

    [Column("count")]
    public int Count { get; set; }

    [Column("volume")]
    public double Volume { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(512)]
    public string? Tag { get; set; }
}
