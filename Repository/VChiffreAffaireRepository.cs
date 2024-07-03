using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

public class VChiffreAffaireRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ClientRepository _chiffreaffaire;
    private readonly ConnectionPath _connectionPath;

    public VChiffreAffaireRepository(ApplicationDbContext context,ClientRepository _chiffr,ConnectionPath _connectio)
    {
        _context = context;
        _chiffreaffaire = _chiffr;
        _connectionPath = _connectio;
    }

    public double ChiffreAffaireAdminFiltre(DateTime dateDebut,DateTime dateFin)
    {
        double sommeLoyer = 0;
        DateTime d1 = dateDebut;
        DateTime d2 = dateFin;
        if(d1 == DateTime.MinValue){ d1 = dateMin(); }
        if(d2 == DateTime.MinValue){ d2 = dateMax(); }
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionPath.Connection))
        {
            string sqlQuery = @"SELECT SUM(loyer_payer) AS somme_loyer 
                                FROM view_ca 
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
    public DateTime dateMin(){
        DateTime date = (DateTime)_context._vchiffaffaire.Min(v => v.MoisLoyer);
        return date;   
    }
    public DateTime dateMax(){
        DateTime date = (DateTime)_context._vchiffaffaire.Max(v => v.FinduMoi);
        return date;
    }

    public double? GainAdminFiltre(DateTime dateDebut,DateTime dateFin)
    {
        double sommeLoyer = 0;
        DateTime d1 = dateDebut;
        DateTime d2 = dateFin;
        if(d1 == DateTime.MinValue){ d1 = dateMin(); }
        if(d2 == DateTime.MinValue){ d2 = dateMax(); }
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionPath.Connection))
        {
            string sqlQuery = @"SELECT SUM(valeur_commission) AS somme_loyer 
                                FROM view_ca 
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

        public double? ChiffreAffaireProFiltre(DateTime dateDebut, DateTime dateFin,string? userId)
    {
        double sommeLoyer = 0;
        DateTime d1 = dateDebut;
        DateTime d2 = dateFin;
        if(d1 == DateTime.MinValue){ d1 = dateMin(); }
        if(d2 == DateTime.MinValue){ d2 = dateMax(); }
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionPath.Connection))
        {
            string sqlQuery = @"SELECT SUM(gain_proprietaire) AS somme_loyer 
                                FROM view_ca 
                                WHERE date_trunc('month', fin_du_mois) >= date_trunc('month', @d1::DATE)
                                AND date_trunc('month', fin_du_mois) <= date_trunc('month', @d2::DATE)
                                and id_proprietaire = @idpro";

            NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@d1", d1);
            command.Parameters.AddWithValue("@d2", d2);
            command.Parameters.AddWithValue("@idpro", userId);
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
    

    public List<ViewChiffreAffaire> FindAll()
    {
        return _context._vchiffaffaire.ToList()?? new List<ViewChiffreAffaire>();
    }
}