using Covoiturage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Covoiturage.Controllers.Utilisateurs;

public class ChauffeurController : Controller
{
    private readonly CovoiturageContext _context;

    public ChauffeurController(CovoiturageContext context)
    {
        _context = context;
    }
    
    private int? UserId => HttpContext.Session.GetInt32("UserId");
    
    // GET
    public IActionResult Profil()
    {
        if (UserId == null)
            return RedirectToAction("Login", "Home");

        var utilisateur = _context.Utilisateurs
            .Include(a => a.Role)
            .FirstOrDefault(u => u.Id == UserId.Value);

        if (utilisateur == null)
            return NotFound();

        ViewData["utilisateur"] = utilisateur;
        return View("~/Views/Chauffeur/Index.cshtml");
    }

    public IActionResult MesVehicules()
    {
        List<Vehicule> listevehicule = _context
            .Vehicules
            .Where(a => a.IdUtilisateur == UserId.Value)
            .ToList();
        
        ViewData["listevehicule"] = listevehicule;
        return View("~/Views/Chauffeur/Vehicules.cshtml");
    }
    
    public IActionResult TrajetsParVehicule(int idVehicule)
    {
        var trajets = _context.Trajets
            .Where(t => t.IdVehicule == idVehicule)
            .ToList();

        ViewData["trajets"] = trajets;
        return View("~/Views/Chauffeur/TrajetsParVehicule.cshtml");
    }
    
    public IActionResult TousLesTrajets()
    {
        var vehicules = _context.Vehicules
            .Where(a => a.IdUtilisateur == UserId.Value)
            .ToList();
        List<Trajet> listetrajet = new List<Trajet>();
        foreach (var vehicule in vehicules)
        {
            List<Trajet> trajet = _context.Trajets
                .Include(a => a.Vehicule)
                .Where(a => a.IdVehicule == vehicule.Id)
                .ToList();
            listetrajet.AddRange(trajet);
        }

        ViewData["trajets"] = listetrajet;
        return View("~/Views/Chauffeur/AllTrajet.cshtml");
    }
    
    public IActionResult MesAbonnements()
    {
        var abonnements = _context.AbonnementUtilisateurs
            .Where(a => a.IdUtilisateur == UserId.Value)
            .ToList();
        ViewData["abonnements"] = abonnements;
        return View("~/Views/Chauffeur/Abonnements.cshtml");
    }
}