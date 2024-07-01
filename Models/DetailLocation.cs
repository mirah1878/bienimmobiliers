using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("view_detail_location")]
public class DetailLocation
{
    [Column("nom_bien")]
    public string? NomBien { get; set; }

    [Column("commission")]
    public double? Commission { get; set; }

    [Column("mois")]
    public int Mois { get; set; }

    [Column("date_debut")]
    public DateTime DateDebut { get; set; }

    [Column("fin_du_mois")]
    public DateTime FinDuMois { get; set; }

    [Column("loyer")]
    public double? Loyer { get; set; }

    [Column("gain")]
    public double? Gain { get; set; }

    [Column("chiffre_affaire")]
    public double? ChiffreAffaire { get; set; }

    [Column("email")]
    public string? Email { get; set; }
}
