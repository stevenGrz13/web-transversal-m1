using Covoiturage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Covoiturage.Controllers.Utilisateurs;

public class PassagerController : Controller
{
    private readonly CovoiturageContext _context;

    public PassagerController(CovoiturageContext context)
    {
        _context = context;
    }

    private int? UserId => HttpContext.Session.GetInt32("UserId");

    // GET
    public IActionResult VersAcceuilPassager()
    {
        var derniereCommission = _context.Commission
            .OrderByDescending(c => c.DateDecision)
            .FirstOrDefault();

        ViewData["PourcentageCommission"] = derniereCommission;
        return View("~/Views/Home/AcceuilUtilisateur.cshtml");
    }
    
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
        return View("~/Views/Passager/Index.cshtml");
    }
    
    public IActionResult MesVoyages()
    {
        if (UserId == null)
            return RedirectToAction("Login", "Home");

        var voyages = _context.Voyages
            .Where(v => v.IdPassager == UserId.Value)
            .Include(v => v.Trajet)
            .ToList();

        ViewData["voyages"] = voyages;

        double totalDepense = voyages
            .Where(v => v.Trajet?.PrixUniquePlace != null)
            .Sum(v => v.Trajet.PrixUniquePlace.Value);

        ViewData["TotalDepense"] = totalDepense;

        return View("~/Views/Passager/Voyages.cshtml");
    }

    public class TrajetDisponibleDto
    {
        public Trajet Trajet { get; set; } = null!;
        public int PlacesLibres { get; set; }
    }

    public IActionResult TrajetsDisponibles()
    {
        var trajets = _context.Trajets
            .Include(t => t.Vehicule)
            .ToList();

        var trajetsDisponibles = trajets.Select(t => new TrajetDisponibleDto
        {
            Trajet = t,
            PlacesLibres = (t.Vehicule?.NombrePlace ?? 0) - 
                           _context.Voyages.Count(v => v.IdTrajet == t.Id && v.EstPayee == true)
        }).ToList();

        ViewData["TrajetsDisponibles"] = trajetsDisponibles;
        return View("~/Views/Passager/Trajets.cshtml");
    }

    public IActionResult Reserver(int trajetid, string lieuderecuperation, string destination, int nombreplace)
    {
        Voyage voyage = new Voyage();
        voyage.IdPassager = UserId.Value;
        voyage.IdTrajet = trajetid;
        voyage.Destination = destination;
        voyage.LieuRecuperation = lieuderecuperation;

        if (nombreplace == 1)
        {
            _context.Add(voyage);
            _context.SaveChanges();
        }

        if (nombreplace > 1)
        {
            for (int i = 0; i < nombreplace; i++)
            {
                _context.Add(voyage);
                _context.SaveChanges();
            }
        }

        return Ok();
    }
}