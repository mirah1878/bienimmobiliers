using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class DetailLocationRepository
{
    private readonly ApplicationDbContext _context;

    public DetailLocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<DetailLocation> GetById(string? idlocation)
    {
        return _context._detail.Where(v => v.Idlocation == idlocation).ToList();
    }

    public List<DetailLocation> FindAll()
    {
        return _context._detail?.ToList()?? new List<DetailLocation>();
    }
}