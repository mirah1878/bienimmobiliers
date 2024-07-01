using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class VChiffreAffaireRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ClientRepository _chiffreaffaire;

    public VChiffreAffaireRepository(ApplicationDbContext context,ClientRepository _chiffr)
    {
        _context = context;
        _chiffreaffaire = _chiffr;
    }

    public double? ChiffreAffaireAdminFiltre(DateTime dateDebut, DateTime dateFin)
    {
    
        dateDebut = dateDebut.ToUniversalTime();
        dateFin = dateFin.ToUniversalTime();
        double? queryResult = _context._viewca
            .Where(v => v.DateDebut >= dateDebut && v.DateDebut <= dateFin)
            .Sum(v => v.LoyerPayer);
        
        return queryResult;
    }

    public double? GainAdminFiltre(DateTime dateDebut, DateTime dateFin)
    {
    
        dateDebut = dateDebut.ToUniversalTime();
        dateFin = dateFin.ToUniversalTime();
        double? queryResult = _context._viewca
            .Where(v => v.DateDebut >= dateDebut && v.DateDebut <= dateFin)
            .Sum(v => v.ValeurCommission);
        
        return queryResult;
    }

        public double? ChiffreAffaireProFiltre(DateTime dateDebut, DateTime dateFin,string? userId)
    {
    
        dateDebut = dateDebut.ToUniversalTime();
        dateFin = dateFin.ToUniversalTime();
        double? queryResult = _context._viewca
            .Where(v => v.DateDebut >= dateDebut && v.DateDebut <= dateFin  && v.IdProprietaire == userId)
            .Sum(v => v.LoyerPayer);
        
        return queryResult;
    }

    public List<ViewChiffreAffaire> FindAll()
    {
        return _context._vchiffaffaire.ToList()?? new List<ViewChiffreAffaire>();
    }


    // ---------------------------------------------------------ADMIN--------------------------------------------------------------------------
    // -----------------------------------------------------------------------------------------------------------------------------------
  /*  public double ChiffreAffaireAdmin(){
        var total = _context._view_chiffre_affaire.Sum(v => v.Loyer);
        #pragma warning disable CS8629 // Nullable value type may be null.
        return (double)total;
    }
    public double ChiffreAffaireAdminFiltre(DateTime dateDebut,DateTime dateFin){
        double sommeLoyer = 0;
        DateTime d1 = dateDebut;
        DateTime d2 = dateFin;
        if(d1 == DateTime.MinValue){ d1 = dateMin(); }
        if(d2 == DateTime.MinValue){ d2 = dateMax(); }
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionPath.Connection))
        {
            string sqlQuery = @"SELECT SUM(loyer) AS somme_loyer 
                                FROM view_chiffre_affaire 
                                WHERE date_trunc('month', fin_du_mois) >= date_trunc('month', @d1::DATE)
                                AND date_trunc('month', fin_du_mois) <= date_trunc('month', @d2::DATE)";

            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@d1", d1);
            command.Parameters.AddWithValue("@d2", d2);
            connection.Open();
            var result = command.ExecuteScalar();
            if (result != DBNull.Value)
            {
                sommeLoyer = (double)Convert.ToDecimal(result);
            }
            connection.Close();
        }
        return sommeLoyer;
    }

    // -----------------------------------------------------------------------------------------------------------------------------------

    public double Gain(){
        var total = _context._view_chiffre_affaire.Sum(v => v.GainParMoi);
        #pragma warning disable CS8629 // Nullable value type may be null.
        return (double)total;
    }
    public double GainFiltre(DateTime dateDebut,DateTime dateFin){
        double gain = 0;
        DateTime d1 = dateDebut;
        DateTime d2 = dateFin;
        if(d1 == DateTime.MinValue){ d1 = dateMin(); }
        if(d2 == DateTime.MinValue){ d2 = dateMax(); }
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionPath.Connection))
        {
            string sqlQuery = @"SELECT SUM(gain_par_moi) AS somme_loyer 
                                FROM view_chiffre_affaire 
                                WHERE date_trunc('month', fin_du_mois) >= date_trunc('month', @d1::DATE)
                                AND date_trunc('month', fin_du_mois) <= date_trunc('month', @d2::DATE)";

            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@d1", d1);
            command.Parameters.AddWithValue("@d2", d2);
            connection.Open();
            var result = command.ExecuteScalar();
            if (result != DBNull.Value)
            {
                gain = (double)Convert.ToDecimal(result);
            }
            connection.Close();
        }
        return gain;
    }
    // ---------------------------------------------------------PROPRIETAIRE--------------------------------------------------------------------------
    // -----------------------------------------------------------------------------------------------------------------------------------
    public double ChiffreAffaireProprietaire(string? id_proprietaire){
        var total = _context._view_chiffre_affaire.Where(v => v.IdProprietaire == id_proprietaire)
        .Sum(v => v.Loyer);
        #pragma warning disable CS8629 // Nullable value type may be null.
        return (double)total;
    }
    public double ChiffreAffaireProprietaireFiltre(string? id_proprietaire,DateTime dateDebut,DateTime dateFin){
        DateTime d1 = dateDebut;
        DateTime d2 = dateFin;
        if(d1 == DateTime.MinValue){ d1 = dateMin(); }
        if(d2 == DateTime.MinValue){ d2 = dateMax(); }
        var total = _context._view_chiffre_affaire.Where(v => v.IdProprietaire == id_proprietaire && v.MoisLoyer >= d1 && v.FinDuMois <= d2)
        .Sum(v => v.Loyer);
        return (double)total;
    }

    // -----------------------------------------------------------------------------------------------------------------------------------
    public DateTime dateMin(){
        DateTime date = (DateTime)_context._view_chiffre_affaire.Min(v => v.MoisLoyer);
        return date;   
    }
    public DateTime dateMax(){
        DateTime date = (DateTime)_context._view_chiffre_affaire.Max(v => v.FinDuMois);
        return date;
    }
    public static DateTime GetStartOfMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }
    public static DateTime GetEndOfMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
    }*/

}