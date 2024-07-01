using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ViewcaRepository
{
    private readonly ApplicationDbContext _context;

    public ViewcaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Photo? GetById(string? idbien)
    {
        return _context._photo.FirstOrDefault(v => v.IdBien== idbien);
    }
    public List<Photo> FindAll()
    {
        return _context._photo?.ToList()?? new List<Photo>();
    }

    public void Add(Photo photo)
    {
        if (photo == null)
        {
            throw new ArgumentNullException(nameof(photo));
        }

        _context._photo.Add(photo);
        _context.SaveChanges();
    }

}