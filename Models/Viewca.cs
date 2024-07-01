using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("view_ca")]
public class Viewca
{
    [Column("id_bien")]
    public string? IdBien { get; set; }

    [Column("nom_bien")]
    public string? NomBien { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("loyer")]
    public double? Loyer { get; set; }

    [Column("id_proprietaire")]
    public string? IdProprietaire { get; set; }

    [Column("id_region")]
    public string? IdRegion { get; set; }

    [Column("id_type_de_bien")]
    public string? IdTypeDeBien { get; set; }

    [Column("telephone")]
    public string? Telephone { get; set; }

    [Column("nom_region")]
    public string? NomRegion { get; set; }

    [Column("nom_type_bien")]
    public string? NomTypeBien { get; set; }

    [Column("commission")]
    public double? Commission { get; set; }

    [Column("duree")]
    public int Duree { get; set; }

    [Column("date_debut")]
    public DateTime DateDebut { get; set; }

    [Column("id_client")]
    public string? IdClient { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("chiffre_affaire")]
    public double? ChiffreAffaireTotal { get; set; }

    [Column("gain")]
    public double? Gain { get; set; }

    [Column("gain_par_mois")]
    public double? GainParMois { get; set; }

    [Column("mois")]
    public int Mois { get; set; }

    [Column("fin_du_mois")]
    public DateTime FinDuMois { get; set; }

    [Column("mois_loyer")]
    public DateTime MoisLoyer { get; set; }

    [Column("loyer_payer")]
    public double? LoyerPayer { get; set; }

    [Column("commission_pourcentage")]
    public double? CommissionPourcentage { get; set; }

    [Column("valeur_commission")]
    public double? ValeurCommission { get; set; }
}
