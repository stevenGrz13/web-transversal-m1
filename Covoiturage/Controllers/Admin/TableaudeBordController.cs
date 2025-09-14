using Covoiturage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Covoiturage.Controllers;

public class TableaudeBordController : Controller
{
    private readonly CovoiturageContext _context;

    public TableaudeBordController(CovoiturageContext context)
    {
        _context = context;
    }
    
    // GET
    public IActionResult VersTableauDeBord()
    {
        List<Commission> listeCommission = _context.Commission.ToList();
        double pourcentageCommission = listeCommission.Last().Pourcentage;

        var listeTrajet = _context.Trajets.ToList();
        var listeDepense = _context.Depenses.ToList();

        double chiffreAffaire = _context.Voyages
            .Where(v => v.EstPayee == true)
            .Join(
                _context.Trajets,
                v => v.IdTrajet,
                t => t.Id,
                (v, t) => t.PrixUniquePlace ?? 0
            ).Sum();

        double commission = (chiffreAffaire * pourcentageCommission) / 100.0;

        double depenseTotal = listeDepense.Sum(d => d.Montant);

        double benefice = commission - depenseTotal;

        int totalTrajets = listeTrajet.Count;
        int totalVoyagesPayes = _context.Voyages.Count(v => v.EstPayee == true);

        ViewData["ChiffreAffaire"] = chiffreAffaire;
        ViewData["Commission"] = commission;
        ViewData["PourcentageCommission"] = pourcentageCommission;
        ViewData["Benefice"] = benefice;
        ViewData["DepenseTotal"] = depenseTotal;
        ViewData["TotalTrajets"] = totalTrajets;
        ViewData["TotalVoyagesPayes"] = totalVoyagesPayes;
        ViewData["ListeDepense"] = listeDepense;

        return View("~/Views/Admin/TableauDeBord.cshtml");
    }
    
    public IActionResult Profil()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Home");
        }

        var utilisateur = _context.Utilisateurs
            .Include(a => a.Role)
            .FirstOrDefault(u => u.Id == userId.Value);

        if (utilisateur == null)
        {
            return NotFound();
        }

        ViewData["utilisateur"] = utilisateur;
        return View("~/Views/Admin/Profil.cshtml");
    }
}