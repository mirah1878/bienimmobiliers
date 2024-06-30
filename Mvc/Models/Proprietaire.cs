using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("proprietaire")]
public class Proprietaire
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("tel")]
    public string? Tel { get; set; }
}
