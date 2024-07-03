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

    [Column("idlocation")]
    public string? Idlocation { get; set; }

    [Column("loyer")]
    public double Loyer { get; set; }

    [Column("id_proprietaire")]
    public string? IdProprietaire { get; set; }

    [Column("id_region")]
    public string? IdRegion { get; set; }

    [Column("id_type_de_bien")]
    public string? IdTypeDeBien { get; set; }

    [Column("telephone")]
    public string? Telephone { get; set; }

    [Column("nom_region")]
    public string? Nomregion { get; set; }

    [Column("nom_type_bien")]
    public string? Nomtypebien { get; set; }

    [Column("commission")]
    public string? Commission { get; set; }

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
    

    [Column("gain_par_moi")]
    public double Gainparmoi { get; set; }

    [Column("gain_proprietaire")]
    public double Gainproprietaire { get; set; }
    
    
    [Column("mois")]
    public int Mois { get; set; }

    [Column("mois_loyer")]
    public DateTime MoisLoyer { get; set; }

    [Column("fin_du_mois")]
    public DateTime FinduMoi { get; set; }


}
