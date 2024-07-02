using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ViewpayeRepository
{
    private readonly ApplicationDbContext _context;

    public ViewpayeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ViewPaye> ListLoyer(DateTime dateDebut, DateTime dateFin, string? idclient)
    {
        dateDebut = dateDebut.ToUniversalTime();
        dateFin = dateFin.ToUniversalTime();

        var queryResult = _context._vloyer
            .Where(v => v.DateDebut >= dateDebut && v.DateDebut <= dateFin && v.IdClient == idclient)
            .ToList();

        return queryResult;
    }



    public List<ViewPaye> FindAll()
    {
        return _context._vloyer?.ToList()?? new List<ViewPaye>();
    }


}