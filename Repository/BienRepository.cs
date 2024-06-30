using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class BienRepository
{
    private readonly ApplicationDbContext _context;

    public BienRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Bien> GetByProprietaireId(string? proprietaireId)
    {
        return _context._bien.Where(v => v.IdProprietaire == proprietaireId).ToList();
    }


    public List<Bien> FindAll()
    {
        return _context._bien?.ToList()?? new List<Bien>();
    }

    public void Add(Bien bien)
    {
        if (bien == null)
        {
            throw new ArgumentNullException(nameof(Bien));
        }

        _context._bien.Add(bien);
        _context.SaveChanges();
    }
}