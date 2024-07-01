using System.Data;
using System.Diagnostics;
using iText.Layout.Hyphenation;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Microsoft.EntityFrameworkCore;


namespace Mvc.Controllers;
//[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly ApplicationDbContext _context;
     private readonly AdminRepository admin;
     private readonly ClientRepository client;
     private readonly BienRepository bien;
     private readonly Import csv;
     private readonly LocationRepository location;
     private readonly VChiffreAffaireRepository viewChiffreAffaire;

    public AdminController(
        ApplicationDbContext context,
        ILogger<AdminController> logger,
        AdminRepository ad,
        ClientRepository cl,
        BienRepository bi,
        LocationRepository loc,
        Import _csv,
        VChiffreAffaireRepository vha
        )
    {
        _logger = logger;
        _context = context;
        admin = ad;
        viewChiffreAffaire = vha;
        client = cl;
        bien = bi;
        location = loc;
        csv = _csv;
    }

    public IActionResult Restore()
    {
        _context.Database.ExecuteSqlRaw(@"TRUNCATE TABLE client,proprietaire,
            type_de_bien,region,bien,photo,location cascade");
        return RedirectToAction("Index", "Admin");
    }

    public IActionResult ImportBien(IFormFile type)
    {   
        if(type!= null){
            csv.ImportCsvToDatabase("bien_temporaire",type,TemporaireBien.MapBienTemporaire);
            csv.InsertDataFromBien();
            return RedirectToAction("Acceuil", "Admin"); 
        }else{
            TempData["ErrorMessage"] = "le fichier ne doit pas etre null";
            return RedirectToAction("PageImportType","");
        }
    }

    public IActionResult ImportLocation(IFormFile type)
    {   
        if(type!= null){
           csv.ImportCsvToDatabase("location_temporaire",type,TemporaireLocation.MapLocationTemporaire);
            csv.InsertDataFromLocation();
            return RedirectToAction("Acceuil", "Admin"); 
        }else{
            TempData["ErrorMessage"] = "le fichier ne doit pas etre null";
            return RedirectToAction("PageImportType","");
        }
        
    }
    
    public IActionResult ImportType(IFormFile type)
    {   
        if(type!= null){
            csv.ImportCsvToDatabase("commission_temporaire",type,TemporaireCommission.MapCommission);
            csv.InsertDataCommission();

            return RedirectToAction("Acceuil", "Admin"); 
        }else{
            TempData["ErrorMessage"] = "le fichier ne doit pas etre null";
            return RedirectToAction("PageImportType","");
        }
        
    }

    public IActionResult PageImportLocation()
    {   
        ViewBag.ErrorMessage = TempData["ErrorMessage"];
        return View();
    }
    
    public IActionResult PageImportBien()
    {   
        ViewBag.ErrorMessage = TempData["ErrorMessage"];
        return View();
    }
    public IActionResult PageImportType()
    {   
        ViewBag.ErrorMessage = TempData["ErrorMessage"];
        return View();
    }

    public IActionResult AjoutLocation(string bien,string client,int duree,DateTime date)
    {
        date = date.ToUniversalTime();
        var addloc = new Location{
            IdBien = bien,
            IdClient = client,
            Duree = duree,
            DateDebut = date,
        };

        location.Add(addloc);
        return RedirectToAction("Acceuil", "Admin"); 
    }
    
    public IActionResult PageAjoutLocation()
    {   
        ViewBag.client = client.FindAll();
        ViewBag.bien = bien.FindAll();
        return View();
    }
    public IActionResult ListeChiffrev(DateTime date1, DateTime date2)
    {   
        if(date2 <= date1){
            throw new Exception();
        }
        ViewBag.date1 = date1.ToString("dd/MM/yyyy");
        ViewBag.date2 = date2.ToString("dd/MM/yyyy");
        ViewBag.Gain = viewChiffreAffaire.Gain(date1,date2);
        ViewBag.Affaire = viewChiffreAffaire.Affaire(date1,date2);
            return View();
    }
    public IActionResult ListeChiffre()
    {   
        return View();
    }
    public IActionResult Acceuil()
    {
        return View();
    }

    public IActionResult Login(string email, string pass)
    {
         string? userId = admin.Authenticate(email, pass);

         if (!string.IsNullOrEmpty(userId))
            {
                HttpContext.Session.SetString("Id", userId);
                return RedirectToAction("Acceuil", "Admin"); 
            }
            else
            {
                TempData["ErrorMessage"] = "Adresse email ou mot de passe incorrect.";
                return RedirectToAction("Index","Admin");
            }
    }

     public IActionResult Index()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"];
        return View();
    }

        

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}