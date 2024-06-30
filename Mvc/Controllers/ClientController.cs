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

    public ClientController(
        ApplicationDbContext context,
        ILogger<ClientController> logger,
        ClientRepository cl
        )
    {
        _logger = logger;
        _context = context;
        client = cl;
    }
    public IActionResult VoirLoyerv(DateTime date1,DateTime date2)
    {
        string? userId = HttpContext.Session.GetString("Id"); 
        ViewBag.list = client.paye(date1,date2,userId);
        return Ok($"{ViewBag.list }");
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
         Console.WriteLine("email:"+email);

         if (!string.IsNullOrEmpty(userId))
            {
                HttpContext.Session.SetString("Id", userId);
                return RedirectToAction("Acceuil", "Client"); 
            }
            else
            {
                var add = new Client
                {
                    Email = email,
                };
                client.Add(add);
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