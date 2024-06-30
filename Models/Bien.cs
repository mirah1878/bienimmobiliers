using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("bien")]
public class Bien
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("loyer")]
    public double? Loyer { get; set; }

    [ForeignKey("Proprietaire")]
    [Column("id_proprietaire")]
    public string? IdProprietaire { get; set; }

    [ForeignKey("Region")]
    [Column("id_region")]
    public string? IdRegion { get; set; }

    [ForeignKey("TypeDeBien")]
    [Column("id_type_de_bien")]
    public string? IdTypeDeBien { get; set; }
}
