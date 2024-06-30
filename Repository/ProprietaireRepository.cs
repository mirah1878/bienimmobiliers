using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ProprietaireRepository
{
    private readonly ApplicationDbContext _context;

    public ProprietaireRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public List<Proprietaire> FindAll()
    {
        return _context._proprietaire?.ToList()?? new List<Proprietaire>();
    }

    public void Add(Proprietaire pro)
    {
        if (pro == null)
        {
            throw new ArgumentNullException(nameof(Proprietaire));
        }

        _context._proprietaire.Add(pro);
        _context.SaveChanges();
    }

    public void Update(Proprietaire pro)
    {
        if (pro == null)
        {
            throw new ArgumentNullException(nameof(pro));
        }

        _context._proprietaire.Update(pro);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var varToDelete = _context._proprietaire.Find(id);
        if (varToDelete != null)
        {
            _context._proprietaire.Remove(varToDelete);
            _context.SaveChanges();
        }
    }

    public string? Authenticate(string tel)
    {
        var selectAll = this.FindAll();
        foreach (var adm in selectAll)
        {
            if (adm.Tel == tel)
            {
                return adm.Id;
            }
        }
        return null; 
    }
    
}