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

    public List<Viewca> FindAll()
    {
        return _context._viewca?.ToList()?? new List<Viewca>();
    }


}