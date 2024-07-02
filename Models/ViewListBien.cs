using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 [Table("view_list_bien")] 
    public class ViewListBien
    {
        [Column("nom_bien")]
        public string? NomBien { get; set; }

        [Column("id_proprietaire")]
        public string? IdProprietaire { get; set; }

        [Column("description_bien")]
        public string? DescriptionBien { get; set; }

        [Column("loyer")]
        public double? Loyer { get; set; }

        [Column("date_fin")]
        public DateTime DateFin { get; set; }
    }
