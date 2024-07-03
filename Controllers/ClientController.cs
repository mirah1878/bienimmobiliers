using System.Data;
using System.Diagnostics;
using iText.Layout.Hyphenation;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Microsoft.EntityFrameworkCore;


namespace Mvc.Controllers;
//[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class ClientController : Controller
{
    private readonly ILogger<ClientController> _logger;
    private readonly ApplicationDbContext _context;
     private readonly ClientRepository client;
     private readonly ViewpayeRepository viewpaye;
     private readonly BienRepository bien;

    public ClientController(
        ApplicationDbContext context,
        ILogger<ClientController> logger,
        ClientRepository cl,
        ViewpayeRepository vp,
        BienRepository bn
        )
    {
        _logger = logger;
        _context = context;
        client = cl;
        viewpaye = vp;
        bien = bn;
    }
    public IActionResult VoirLoyerv(DateTime date1,DateTime date2)
    {
        string? userId = HttpContext.Session.GetString("Id"); 
        date1 = date1.ToUniversalTime();
        date2 = date2.ToUniversalTime();
        ViewBag.list = viewpaye.ListLoyer(date1,date2,userId);
        ViewBag.paye = viewpaye.GetSommeLoyersPayes(date1,date2,userId);
        ViewBag.npaye = viewpaye.GetSommeLoyersnonPayes(date1,date2,userId);
        ViewBag.bien = bien.FindAll();
        return View();
    }
    

    public IActionResult VoirLoyer()
    {
        return View();
    }

    

    //public IActionResult Restore()
    //{
    //    _context.Database.ExecuteSqlRaw(@"TRUNCATE TABLE genre,cat,
    //        courreur,etape,etapecoureur,courseetape,
    //        points,pointcourreur,penalite,tempetape,tempresultat cascade");
    //    return RedirectToAction("Index", "Admin");
    //}

    public IActionResult Acceuil()
    {
        return View();
    }

    public IActionResult Login(string email)
    {
         string? userId = client.Authenticate(email);
         Console.WriteLine("user:"+userId);

         if (!string.IsNullOrEmpty(userId))
            {
                HttpContext.Session.SetString("Id", userId);
                return RedirectToAction("Acceuil", "Client"); 
            }
            else
            {
                return RedirectToAction("Acceuil", "Client"); 
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