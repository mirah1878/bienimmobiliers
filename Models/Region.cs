using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("region")]
public class Region
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }
}
