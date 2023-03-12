using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Entities;

[Table("Customer")]
public partial class Customer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("telphone")]
    [StringLength(50)]
    public string? Telphone { get; set; }

    [Column("nation")]
    [StringLength(50)]
    public string? Nation { get; set; }

    [Column("address")]
    [StringLength(128)]
    public string? Address { get; set; }

    [Column("description", TypeName = "ntext")]
    public string? Description { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(128)]
    public string? Tag { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<AirTransport> AirTransports { get; } = new List<AirTransport>();

    [InverseProperty("Customer")]
    public virtual ICollection<SeaTransport> SeaTransports { get; } = new List<SeaTransport>();
}
