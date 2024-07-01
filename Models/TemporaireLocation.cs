using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CsvHelper;

[Table("location_temporaire")]
public class TemporaireLocation
{
    [Column("reference")]
    public string? Reference { get; set; }

    [Column("date_debut")]
    public DateTime DateDebut { get; set; }

    [Column("duree_mois")]
    public int DureeMois { get; set; }

    [Column("client")]
    public string? Client { get; set; }

    public static TemporaireLocation MapLocationTemporaire(CsvReader csv)
    {
        return new TemporaireLocation
        {
            Reference = csv.GetField<string>("reference"),
            DateDebut = DateTime.ParseExact(csv.GetField<string>("Date debut"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
            DureeMois = csv.GetField<int>("duree mois"),
            Client = csv.GetField<string>("client")
        };
    }
}
