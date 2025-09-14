using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covoiturage;
using Covoiturage.Models;

namespace Covoiturage.Controllers.Admin
{
    public class VoyageController : Controller
    {
        private readonly CovoiturageContext _context;

        public VoyageController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: Voyage
        public async Task<IActionResult> Index()
        {
            var covoiturageContext = _context.Voyages.Include(v => v.Trajet).Include(v => v.Utilisateur);
            return View(await covoiturageContext.ToListAsync());
        }

        // GET: Voyage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyages
                .Include(v => v.Trajet)
                .Include(v => v.Utilisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound();
            }

            return View(voyage);
        }

        // GET: Voyage/Create
        public IActionResult Create()
        {
            ViewData["IdTrajet"] = new SelectList(_context.Trajets, "Id", "Id");
            ViewData["IdPassager"] = new SelectList(_context.Utilisateurs, "Id", "Id");
            return View();
        }

        // POST: Voyage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPassager,IdTrajet,LieuRecuperation,Destination,EstPayee")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voyage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTrajet"] = new SelectList(_context.Trajets, "Id", "Id", voyage.IdTrajet);
            ViewData["IdPassager"] = new SelectList(_context.Utilisateurs, "Id", "Id", voyage.IdPassager);
            return View(voyage);
        }

        // GET: Voyage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyages.FindAsync(id);
            if (voyage == null)
            {
                return NotFound();
            }
            ViewData["IdTrajet"] = new SelectList(_context.Trajets, "Id", "Id", voyage.IdTrajet);
            ViewData["IdPassager"] = new SelectList(_context.Utilisateurs, "Id", "Id", voyage.IdPassager);
            return View(voyage);
        }

        // POST: Voyage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPassager,IdTrajet,LieuRecuperation,Destination,EstPayee")] Voyage voyage)
        {
            if (id != voyage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voyage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoyageExists(voyage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTrajet"] = new SelectList(_context.Trajets, "Id", "Id", voyage.IdTrajet);
            ViewData["IdPassager"] = new SelectList(_context.Utilisateurs, "Id", "Id", voyage.IdPassager);
            return View(voyage);
        }

        public IActionResult PayVoyage(int idvoyage)
        {
            Voyage voyage = _context.Voyages.Find(idvoyage);
            voyage.EstPayee = true;
            _context.SaveChanges();
            
            return RedirectToAction("MesVoyages", "Passager");
        }

        // GET: Voyage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyages
                .Include(v => v.Trajet)
                .Include(v => v.Utilisateur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voyage == null)
            {
                return NotFound();
            }

            return View(voyage);
        }

        // POST: Voyage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voyage = await _context.Voyages.FindAsync(id);
            if (voyage != null)
            {
                _context.Voyages.Remove(voyage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoyageExists(int id)
        {
            return _context.Voyages.Any(e => e.Id == id);
        }
    }
}
