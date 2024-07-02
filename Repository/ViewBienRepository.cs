using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ViewBienRepository
{
    private readonly ApplicationDbContext _context;

    public ViewBienRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ViewListBien>  GetById(string? IdPro)
    {
        return _context._vlist.Where(v => v.IdProprietaire == IdPro).ToList();
    }
    public List<ViewListBien> FindAll()
    {
        return _context._vlist?.ToList()?? new List<ViewListBien>();
    }
}