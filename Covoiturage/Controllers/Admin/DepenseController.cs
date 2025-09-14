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
    public class DepenseController : Controller
    {
        private readonly CovoiturageContext _context;

        public DepenseController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: Depense
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depenses.ToListAsync());
        }

        // GET: Depense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // GET: Depense/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule,Montant,Date")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depense);
        }

        // GET: Depense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depenses.FindAsync(id);
            if (depense == null)
            {
                return NotFound();
            }
            return View(depense);
        }

        // POST: Depense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Intitule,Montant,Date")] Depense depense)
        {
            if (id != depense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepenseExists(depense.Id))
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
            return View(depense);
        }

        // GET: Depense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // POST: Depense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depense = await _context.Depenses.FindAsync(id);
            if (depense != null)
            {
                _context.Depenses.Remove(depense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepenseExists(int id)
        {
            return _context.Depenses.Any(e => e.Id == id);
        }
    }
}
