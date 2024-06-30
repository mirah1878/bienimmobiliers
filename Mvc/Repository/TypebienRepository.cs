using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class TypebienRepository
{
    private readonly ApplicationDbContext _context;

    public TypebienRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<TypeDeBien> FindAll()
    {
        return _context._typesDeBien?.ToList()?? new List<TypeDeBien>();
    }
}