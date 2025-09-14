using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Covoiturage.Models;

namespace Covoiturage.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CovoiturageContext _context;

    public HomeController(ILogger<HomeController> logger, CovoiturageContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Login()
    {
        if (TempData["messageerreur"] != null)
        {
            ViewBag.message = TempData["messageerreur"];
        }
        List<Role> listeRole = _context.Roles.ToList();
        ViewData["roles"] = listeRole;
        return View();
    }
    
    [HttpPost]
    public IActionResult LoginAction(int idrole, string username, string password)
    {
        var utilisateur = _context.Utilisateurs.FirstOrDefault(x =>
            x.IdRole == idrole &&
            x.Email.ToLower() == username.ToLower() &&
            x.MotDePasse.ToLower() == password.ToLower()
        );

        if (utilisateur == null)
        {
            // Identifiants invalides
            ViewBag.Error = "Email, mot de passe ou rôle incorrect.";
            return View("~/Views/Home/Login.cshtml"); // page de login
        }

        HttpContext.Session.SetInt32("UserId", utilisateur.Id);
        HttpContext.Session.SetInt32("UserRole", utilisateur.IdRole);

        if (utilisateur.IdRole == 1) // chauffeur
        {
            return View("~/Views/Home/AcceuilChauffeur.cshtml"); 
        }
        if (utilisateur.IdRole == 2) // passsager
        {
            return RedirectToAction("VersAcceuilPassager", "Passager");
        }
        if (utilisateur.IdRole == 3) // 3 administrateur
        {
            return View("~/Views/Home/AcceuilAdmin.cshtml"); 
        }

        else
        {
            TempData["messageerreur"] = "no";
            return RedirectToAction("Login");
        }
    }
    public IActionResult LogOutAction()
    {
        HttpContext.Session.Remove("UserId");
        HttpContext.Session.Remove("UserRole");
        return RedirectToAction("Login");
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