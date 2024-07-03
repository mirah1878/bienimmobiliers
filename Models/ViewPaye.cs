using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("view_paye", Schema = "public")] 
    public class ViewPaye
    {
        [Column("id_bien")]
        public string? IdBien { get; set; }

        [Column("id_client")]
        public string? IdClient { get; set; }

        [Column("date_debut")]
        public DateTime DateDebut { get; set; }

        [Column("duree")]
        public int Duree { get; set; }

        [Column("mois")]
        public int Mois { get; set; }

        [Column("fin_du_mois")]
        public DateTime FinDuMois { get; set; }

        [Column("loyer")]
        public int Loyer { get; set; }
        [Column("paye")]
        public int Paye { get; set; }
    }

