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
    public IActionResult VoirLoyerv(DateTime date1, DateTime date2, string userId)
    {
        Console.WriteLine("tonga");
        Console.WriteLine("use :"+userId);
        date1 = date1.ToUniversalTime();
        date2 = date2.ToUniversalTime();
        Console.WriteLine("date1"+date1);
        Console.WriteLine("date2"+date2);
        
        ViewBag.list = viewpaye.ListLoyer(date1, date2, userId);
        ViewBag.paye = viewpaye.GetSommeLoyersPayes(date1, date2, userId);
        ViewBag.npaye = viewpaye.GetSommeLoyersnonPayes(date1, date2, userId);
        ViewBag.bien = bien.FindAll();
        
        return View(); 
    }

  
    

    public IActionResult VoirLoyer()
    {
        return View();
    }

     public IActionResult Acceuil()
    {
        ViewBag.UserId = TempData["UserId"] as string;
        return View();
    }

    public IActionResult Login(string email)
    {
        string? userId = client.Authenticate(email);
        if (!string.IsNullOrEmpty(userId))
        {
            TempData["UserId"] = userId; 
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