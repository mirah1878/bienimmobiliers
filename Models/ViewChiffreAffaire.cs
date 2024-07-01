using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("view_chiffre_affaire")]
public class ViewChiffreAffaire
{
    [Column("id_bien")]
    public string? IdBien { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("loyer")]
    public double Loyer { get; set; }

    [Column("id_proprietaire")]
    public string? IdProprietaire { get; set; }

    [Column("id_region")]
    public string? IdRegion { get; set; }

    [Column("id_type_de_bien")]
    public string? IdTypeDeBien { get; set; }

    [Column("duree")]
    public int? Duree { get; set; }

    [Column("date_debut")]
    public DateTime DateDebut { get; set; }

    [Column("id_client")]
    public string? IdClient { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("chiffre_affaire")]
    public double ChiffreAffaire { get; set; }

    [Column("gain")]
    public double Gain { get; set; }

}
