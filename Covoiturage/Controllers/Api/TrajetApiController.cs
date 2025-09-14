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
    public class TrajetApiController : ControllerBase
    {
        private readonly CovoiturageContext _context;

        public TrajetApiController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: api/TrajetApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trajet>>> GetTrajets()
        {
            return await _context.Trajets.ToListAsync();
        }

        // GET: api/TrajetApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trajet>> GetTrajet(int id)
        {
            var trajet = await _context.Trajets.FindAsync(id);

            if (trajet == null)
            {
                return NotFound();
            }

            return trajet;
        }

        // PUT: api/TrajetApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrajet(int id, Trajet trajet)
        {
            if (id != trajet.Id)
            {
                return BadRequest();
            }

            _context.Entry(trajet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrajetExists(id))
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

        // POST: api/TrajetApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Trajet>> PostTrajet(Trajet trajet)
        // {
        //     _context.Trajets.Add(trajet);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetTrajet", new { id = trajet.Id }, trajet);
        // }
        
        [HttpPost]
        public async Task<ActionResult<Trajet>> PostTrajet(Trajet trajet)
        {
            // Convertir la date en UTC si elle a une valeur
            if (trajet.DateDepart.HasValue)
            {
                // Si la date n'a pas de timezone spécifié, assumez qu'elle est en heure locale et convertissez en UTC
                if (trajet.DateDepart.Value.Kind == DateTimeKind.Unspecified)
                {
                    trajet.DateDepart = DateTime.SpecifyKind(trajet.DateDepart.Value, DateTimeKind.Utc);
                }
                else if (trajet.DateDepart.Value.Kind == DateTimeKind.Local)
                {
                    trajet.DateDepart = trajet.DateDepart.Value.ToUniversalTime();
                }
            }

            _context.Trajets.Add(trajet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrajet", new { id = trajet.Id }, trajet);
        }

        // DELETE: api/TrajetApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrajet(int id)
        {
            var trajet = await _context.Trajets.FindAsync(id);
            if (trajet == null)
            {
                return NotFound();
            }

            _context.Trajets.Remove(trajet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrajetExists(int id)
        {
            return _context.Trajets.Any(e => e.Id == id);
        }
    }
}
