using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class ClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public int getnbmonths(DateTime startDate, DateTime endDate, string? id)
    {
        startDate = startDate.ToUniversalTime();
        endDate = endDate.ToUniversalTime();
        int months = GetNumberOfMonthsInclusive(startDate, endDate);

        return months;
    }
    public double paye(DateTime startDate, DateTime endDate, string? id)
    {
        startDate = startDate.ToUniversalTime();
        endDate = endDate.ToUniversalTime();

        double queryResult = _context._vchiffaffaire
            .Where(v => v.DateDebut >= startDate && v.DateDebut <= endDate && v.IdClient == id )
            .Sum(v => v.ChiffreAffaire);
        

        return queryResult;
    }

    static int GetNumberOfMonthsInclusive(DateTime startDate, DateTime endDate)
    {
        // Ensure startDate is less than or equal to endDate
        if (startDate > endDate)
        {
            throw new ArgumentException("La date de début doit être antérieure ou égale à la date de fin.");
        }

        // Calculate the number of months inclusively
        int startMonth = startDate.Month;
        int startYear = startDate.Year;
        int endMonth = endDate.Month;
        int endYear = endDate.Year;

        // Calculate the total number of months between the two dates, inclusive
        int numberOfMonths = (endYear - startYear) * 12 + endMonth - startMonth +1;
        
        return numberOfMonths;
    }
    private DateTime datemin(DateTime startDate, DateTime endDate, string? id){
        DateTime date = _context._vchiffaffaire
            .OrderBy(v => v.DateDebut)
            .Where(v => v.DateDebut >= startDate && v.DateDebut <= endDate && v.IdClient == id )
            .Select(v => v.DateDebut)
            .FirstOrDefault();
        return date;
    }

    public List<Client> FindAll()
    {
        return _context._client?.ToList()?? new List<Client>();
    }

    public void Add(Client client)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(Client));
        }

        _context._client.Add(client);
        _context.SaveChanges();
    }

    public void Update(Client client)
    {
        if (client == null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        _context._client.Update(client);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var varToDelete = _context._client.Find(id);
        if (varToDelete != null)
        {
            _context._client.Remove(varToDelete);
            _context.SaveChanges();
        }
    }

    public string? Authenticate(string email)
    {
        var selectAll = this.FindAll();
        foreach (var adm in selectAll)
        {
            if (adm.Email == email)
            {
                return adm.Id;
            }
        }
        return null; 
    }
    
}