using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Microsoft.AspNetCore.Http;

namespace Mvc.Controllers;

//[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class ProprietaireController : Controller
{
    private readonly ILogger<ProprietaireController> _logger;
    private readonly ApplicationDbContext _context;
     private readonly ProprietaireRepository proprietaire;
     private readonly PhotoRepository photoRepository;
     private readonly BienRepository bien;
     private readonly RegionRepository region;
     private readonly TypebienRepository typebien;
     private readonly VChiffreAffaireRepository viewChiffreAffaire;

    public ProprietaireController(
        ILogger<ProprietaireController> logger,
        ApplicationDbContext context,
        ProprietaireRepository pro,
        PhotoRepository photoR,
        BienRepository bn,
        RegionRepository reg,
        TypebienRepository ty,
        VChiffreAffaireRepository vha
        )
    {
        _logger = logger;
        _context = context;
        proprietaire = pro;
        photoRepository = photoR;
        bien = bn;
        region = reg;
        typebien = ty;
        viewChiffreAffaire = vha;
    }

    public IActionResult ListeChiffrev(DateTime date1, DateTime date2)
    {   
        string? userId = HttpContext.Session.GetString("Id"); 
        Console.WriteLine(userId);
        ViewBag.date1 = date1.ToString("dd/MM/yyyy");
        ViewBag.date2 = date2.ToString("dd/MM/yyyy");
        
        ViewBag.affaire = viewChiffreAffaire.ChiffreAffaireProFiltre(date1,date2,userId);
            return View();
    }
    public IActionResult ListeChiffre()
    {   
        return View();
    }

    public IActionResult ListeBien()
    {
        string? userId = HttpContext.Session.GetString("Id");   
        Console.WriteLine("user : "+userId);
        ViewBag.liste = bien.GetByProprietaireId(userId);
        ViewBag.photo = photoRepository.FindAll();
        ViewBag.region = region.FindAll();
        ViewBag.type = typebien.FindAll();
        return View();
    }
    public IActionResult Acceuil()
    {
        return View();
    }
    
     public IActionResult Login(string tel)
    {
         string? userId = proprietaire.Authenticate(tel);
         Console.WriteLine("tel:"+tel);

         if (!string.IsNullOrEmpty(userId))
            {
                HttpContext.Session.SetString("Id", userId);
                return RedirectToAction("Acceuil", "Proprietaire"); 
            }
            else
            {
                var add = new Proprietaire
                {
                    Tel = tel,
                };

                proprietaire.Add(add);
                return RedirectToAction("Acceuil", "Proprietaire"); 
            }
    }
    public IActionResult Index()
    {
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