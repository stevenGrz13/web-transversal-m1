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
    public class VehiculeApiController : ControllerBase
    {
        private readonly CovoiturageContext _context;

        public VehiculeApiController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: api/VehiculeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicule>>> GetVehicules()
        {
            return await _context.Vehicules.ToListAsync();
        }

        // GET: api/VehiculeApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicule>> GetVehicule(int id)
        {
            var vehicule = await _context.Vehicules.FindAsync(id);

            if (vehicule == null)
            {
                return NotFound();
            }

            return vehicule;
        }

        // PUT: api/VehiculeApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicule(int id, Vehicule vehicule)
        {
            if (id != vehicule.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculeExists(id))
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

        // POST: api/VehiculeApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicule>> PostVehicule(Vehicule vehicule)
        {
            _context.Vehicules.Add(vehicule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicule", new { id = vehicule.Id }, vehicule);
        }

        // DELETE: api/VehiculeApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicule(int id)
        {
            var vehicule = await _context.Vehicules.FindAsync(id);
            if (vehicule == null)
            {
                return NotFound();
            }

            _context.Vehicules.Remove(vehicule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculeExists(int id)
        {
            return _context.Vehicules.Any(e => e.Id == id);
        }
    }
}
