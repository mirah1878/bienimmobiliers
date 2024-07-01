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

    public double Affaire(DateTime startDate, DateTime endDate)
    {
        startDate = startDate.ToUniversalTime();
        endDate = endDate.ToUniversalTime();

        int nbmois = _chiffreaffaire.GetNumberOfMonthsInclusive(startDate,endDate);

        double queryResult = _context._vchiffaffaire
            .Where(v => v.DateDebut >= startDate && v.DateDebut <= endDate)
            .Sum(v => v.ChiffreAffaire);

        List<double> Result = _context._vchiffaffaire
            .Where(v => v.DateDebut >= startDate && v.DateDebut <= endDate)
            .Select(v => v.Loyer).ToList();
            double somme = 0;
        foreach (var item in Result)
        {
            somme +=item;
            Console.WriteLine(item);
        }

        return queryResult;
    }

    public double Gain(DateTime startDate, DateTime endDate)
    {
        startDate = startDate.ToUniversalTime();
        endDate = endDate.ToUniversalTime();

        double queryResult = _context._vchiffaffaire
            .Where(v => v.DateDebut >= startDate && v.DateDebut <= endDate)
            .Sum(v => v.Gain);

        return queryResult;
    }


   public double[] ChiffreAffairePro(DateTime startDate, DateTime endDate, string? idpro)
    {
        startDate = startDate.ToUniversalTime();
        endDate = endDate.ToUniversalTime();

        double[] result = new double[2];

        var queryResult = _context._vchiffaffaire
            .Where(v => v.DateDebut >= startDate && v.DateDebut <= endDate && v.IdProprietaire == idpro) 
            .GroupBy(v => 1)
            .Select(g => new
            {
                ChiffreAffaireTotal = g.Sum(v => v.ChiffreAffaire),
                GainTotal = g.Sum(v => v.Gain)
            })
            .FirstOrDefault();

        if (queryResult != null)
        {
            result[0] = queryResult.ChiffreAffaireTotal;
            result[1] = queryResult.GainTotal;
        }

        return result;
    }

    public List<ViewChiffreAffaire> FindAll()
    {
        return _context._vchiffaffaire.ToList()?? new List<ViewChiffreAffaire>();
    }
}