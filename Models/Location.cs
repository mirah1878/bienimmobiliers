using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("location")]
public class Location
{
    [Key]
    [Column("id")]
    public string? Id { get; set; }

    [ForeignKey("Bien")]
    [Column("id_bien")]
    public string? IdBien { get; set; }

    [ForeignKey("Client")]
    [Column("id_client")]
    public string? IdClient { get; set; }

    [Column("duree")]
    public int? Duree { get; set; }

    [Column("date_debut")]
    public DateTime DateDebut { get; set; }
}
