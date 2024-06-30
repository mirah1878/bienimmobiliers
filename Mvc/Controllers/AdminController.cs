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
     private readonly VChiffreAffaireRepository viewChiffreAffaire;

    public AdminController(
        ApplicationDbContext context,
        ILogger<AdminController> logger,
        AdminRepository ad,
        VChiffreAffaireRepository vha
        )
    {
        _logger = logger;
        _context = context;
        admin = ad;
        viewChiffreAffaire = vha;
    }

    //public IActionResult Restore()
    //{
    //    _context.Database.ExecuteSqlRaw(@"TRUNCATE TABLE genre,cat,
    //        courreur,etape,etapecoureur,courseetape,
    //        points,pointcourreur,penalite,tempetape,tempresultat cascade");
    //    return RedirectToAction("Index", "Admin");
    //}

    public IActionResult ListeChiffrev(DateTime date1, DateTime date2)
    {   
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