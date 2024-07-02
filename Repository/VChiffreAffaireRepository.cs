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
}