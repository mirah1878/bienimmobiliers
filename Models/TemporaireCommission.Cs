using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper;

[Table("commission_temporaire")]
public class TemporaireCommission
{
    [Column("type")]
    public string? Type { get; set; }

    [Column("commission", TypeName = "date")]
    public double? Commission { get; set; }

    public static TemporaireCommission MapCommission(CsvReader csv)
    {
        return new TemporaireCommission
        {
            Type = csv.GetField<string>("Type"),
            Commission = Double.Parse(Contrainte.ApplyConstraints(csv.GetField<string>("Commission")))    
        };
    }    
}

