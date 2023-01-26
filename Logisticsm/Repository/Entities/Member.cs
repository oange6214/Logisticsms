using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Entities;

[Table("Member")]
public partial class Member
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(32)]
    public string Name { get; set; } = null!;

    [Column("password")]
    [StringLength(32)]
    public string Password { get; set; } = null!;

    [Column("level")]
    public int Level { get; set; }

    [Column("insert_date", TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [Column("tag")]
    [StringLength(128)]
    public string? Tag { get; set; }
}
