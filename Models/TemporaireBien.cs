using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper;

[Table("bien_temporaire")]
public class TemporaireBien
{
    [Key]
    [Column("reference")]
    public string? Reference { get; set; }

    [Column("nom")]
    public string? Nom { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("type")]
    public string? Type { get; set; }

    [Column("region")]
    public string? Region { get; set; }

    [Column("loyer_mensuel")]
    public double? LoyerMensuel { get; set; }

    [Column("proprietaire")]
    public string? Proprietaire { get; set; }

    public static TemporaireBien MapBienTemporaire(CsvReader csv)
    {
        return new TemporaireBien
        {
            Reference = csv.GetField<string>("reference"),
            Nom = csv.GetField<string>("nom"),
            Description = csv.GetField<string>("Description"),
            Type = csv.GetField<string>("Type"),
            Region = csv.GetField<string>("region"),
            LoyerMensuel = csv.GetField<double>("loyer mensuel"),
            Proprietaire = csv.GetField<string>("Proprietaire")
        };
    }
}
