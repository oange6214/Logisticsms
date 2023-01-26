using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Entities;

[Keyless]
[Table("ExpressTransportDetail")]
public partial class ExpressTransportDetail
{
    [Column("id")]
    public int Id { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("express_transport_id")]
    public int ExpressTransportId { get; set; }

    [Column("productor")]
    [StringLength(128)]
    public string Productor { get; set; } = null!;

    [Column("receive_date", TypeName = "datetime")]
    public DateTime ReceiveDate { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(512)]
    public string? Tag { get; set; }
}
