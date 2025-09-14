using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covoiturage;
using Covoiturage.Models;

namespace Covoiturage.Controllers
{
    public class TrajetController : Controller
    {
        private readonly CovoiturageContext _context;

        public TrajetController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: Trajet
        public async Task<IActionResult> Index()
        {
            var covoiturageContext = _context.Trajets.Include(t => t.Vehicule);
            return View(await covoiturageContext.ToListAsync());
        }

        // GET: Trajet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trajet = await _context.Trajets
                .Include(t => t.Vehicule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trajet == null)
            {
                return NotFound();
            }

            return View(trajet);
        }

        // GET: Trajet/Create
        public IActionResult Create()
        {
            ViewData["IdVehicule"] = new SelectList(_context.Vehicules, "Id", "Id");
            return View();
        }

        // POST: Trajet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdVehicule,Depart,Arrivee,DateDepart,PrixUniquePlace")] Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trajet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVehicule"] = new SelectList(_context.Vehicules, "Id", "Id", trajet.IdVehicule);
            return View(trajet);
        }

        // GET: Trajet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trajet = await _context.Trajets.FindAsync(id);
            if (trajet == null)
            {
                return NotFound();
            }
            ViewData["IdVehicule"] = new SelectList(_context.Vehicules, "Id", "Id", trajet.IdVehicule);
            return View(trajet);
        }

        // POST: Trajet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdVehicule,Depart,Arrivee,DateDepart,PrixUniquePlace")] Trajet trajet)
        {
            if (id != trajet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trajet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrajetExists(trajet.Id))
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
            ViewData["IdVehicule"] = new SelectList(_context.Vehicules, "Id", "Id", trajet.IdVehicule);
            return View(trajet);
        }

        // GET: Trajet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trajet = await _context.Trajets
                .Include(t => t.Vehicule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trajet == null)
            {
                return NotFound();
            }

            return View(trajet);
        }

        // POST: Trajet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trajet = await _context.Trajets.FindAsync(id);
            if (trajet != null)
            {
                _context.Trajets.Remove(trajet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrajetExists(int id)
        {
            return _context.Trajets.Any(e => e.Id == id);
        }
    }
}
