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
    public class CommissionController : Controller
    {
        private readonly CovoiturageContext _context;

        public CommissionController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: Commission
        public async Task<IActionResult> Index()
        {
            return View(await _context.Commission.ToListAsync());
        }

        // GET: Commission/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commission == null)
            {
                return NotFound();
            }

            return View(commission);
        }

        // GET: Commission/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Commission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pourcentage,DateDecision")] Commission commission)
        {
            if (ModelState.IsValid)
            {
                commission.DateDecision = DateTime.SpecifyKind(commission.DateDecision, DateTimeKind.Utc);
                _context.Add(commission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commission);
        }

        // GET: Commission/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commission.FindAsync(id);
            if (commission == null)
            {
                return NotFound();
            }
            return View(commission);
        }

        // POST: Commission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pourcentage,DateDecision")] Commission commission)
        {
            if (id != commission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommissionExists(commission.Id))
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
            return View(commission);
        }

        // GET: Commission/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commission = await _context.Commission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commission == null)
            {
                return NotFound();
            }

            return View(commission);
        }

        // POST: Commission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commission = await _context.Commission.FindAsync(id);
            if (commission != null)
            {
                _context.Commission.Remove(commission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommissionExists(int id)
        {
            return _context.Commission.Any(e => e.Id == id);
        }
    }
}
