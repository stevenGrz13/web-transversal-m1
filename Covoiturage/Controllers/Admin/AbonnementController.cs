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
    public class AbonnementController : Controller
    {
        private readonly CovoiturageContext _context;

        public AbonnementController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: Abonnement
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abonnements.ToListAsync());
        }

        // GET: Abonnement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }

        // GET: Abonnement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abonnement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MontantPassager,MontantChauffeur,DateChangement")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                abonnement.DateChangement = DateTime.UtcNow;
                _context.Add(abonnement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abonnement);
        }

        // GET: Abonnement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnements.FindAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }
            return View(abonnement);
        }

        // POST: Abonnement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MontantPassager,MontantChauffeur,DateChangement")] Abonnement abonnement)
        {
            if (id != abonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonnement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonnementExists(abonnement.Id))
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
            return View(abonnement);
        }

        // GET: Abonnement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }

        // POST: Abonnement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonnement = await _context.Abonnements.FindAsync(id);
            if (abonnement != null)
            {
                _context.Abonnements.Remove(abonnement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonnementExists(int id)
        {
            return _context.Abonnements.Any(e => e.Id == id);
        }
    }
}
