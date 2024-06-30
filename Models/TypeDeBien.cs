using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("type_de_bien")]
public class TypeDeBien
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("commission")]
    public double? Commission { get; set; }
}
