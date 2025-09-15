using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Covoiturage;
using Covoiturage.Models;

namespace Covoiturage.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonnementApiController : ControllerBase
    {
        private readonly CovoiturageContext _context;

        public AbonnementApiController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: api/AbonnementApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnements()
        {
            return await _context.Abonnements.ToListAsync();
        }

        // GET: api/AbonnementApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Abonnement>> GetAbonnement(int id)
        {
            var abonnement = await _context.Abonnements.FindAsync(id);

            if (abonnement == null)
            {
                return NotFound();
            }

            return abonnement;
        }

        // PUT: api/AbonnementApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbonnement(int id, Abonnement abonnement)
        {
            if (id != abonnement.Id)
            {
                return BadRequest();
            }

            _context.Entry(abonnement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonnementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AbonnementApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Abonnement>> PostAbonnement(Abonnement abonnement)
        {
            _context.Abonnements.Add(abonnement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbonnement", new { id = abonnement.Id }, abonnement);
        }

        // DELETE: api/AbonnementApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbonnement(int id)
        {
            var abonnement = await _context.Abonnements.FindAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }

            _context.Abonnements.Remove(abonnement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonnementExists(int id)
        {
            return _context.Abonnements.Any(e => e.Id == id);
        }
    }
}
