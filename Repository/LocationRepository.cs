using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class LocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Location> FindAll()
    {
        return _context._location?.ToList()?? new List<Location>();
    }

    public void Add(Location location)
    {
        if (location == null)
        {
            throw new ArgumentNullException(nameof(Location));
        }

        _context._location.Add(location);
        _context.SaveChanges();
    }
}