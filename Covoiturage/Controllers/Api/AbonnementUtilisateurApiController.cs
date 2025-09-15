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
    public class AbonnementUtilisateurApiController : ControllerBase
    {
        private readonly CovoiturageContext _context;

        public AbonnementUtilisateurApiController(CovoiturageContext context)
        {
            _context = context;
        }

        // GET: api/AbonnementUtilisateurApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbonnementUtilisateur>>> GetAbonnementUtilisateurs()
        {
            return await _context.AbonnementUtilisateurs.ToListAsync();
        }

        // GET: api/AbonnementUtilisateurApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AbonnementUtilisateur>> GetAbonnementUtilisateur(int id)
        {
            var abonnementUtilisateur = await _context.AbonnementUtilisateurs.FindAsync(id);

            if (abonnementUtilisateur == null)
            {
                return NotFound();
            }

            return abonnementUtilisateur;
        }

        // PUT: api/AbonnementUtilisateurApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbonnementUtilisateur(int id, AbonnementUtilisateur abonnementUtilisateur)
        {
            if (id != abonnementUtilisateur.Id)
            {
                return BadRequest();
            }

            _context.Entry(abonnementUtilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonnementUtilisateurExists(id))
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

        // POST: api/AbonnementUtilisateurApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAbonnementUtilisateur(
            AbonnementUtilisateur abonnementUtilisateur, 
            int nombremois)
        {
            var moisDepart = DateTime.Now.Month;   
            var anneeDepart = DateTime.Now.Year;   

            for (int i = 0; i < nombremois; i++)
            {
                int mois = ((moisDepart - 1 + i) % 12) + 1;
                int annee = anneeDepart + ((moisDepart - 1 + i) / 12);

                var nouvelAbonnement = new AbonnementUtilisateur
                {
                    IdUtilisateur = abonnementUtilisateur.IdUtilisateur,
                    IdMois = mois,
                    DatePaiement = DateTime.UtcNow,   
                    Annee = annee
                };

                _context.AbonnementUtilisateurs.Add(nouvelAbonnement);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"{nombremois} abonnements créés." });
        }

        [HttpGet("Utilisateur/{idUtilisateur}")]
        public async Task<ActionResult<IEnumerable<AbonnementUtilisateur>>> 
            GetAbonnementsByUtilisateur(int idUtilisateur)
        {
            return await _context.AbonnementUtilisateurs
                .Where(a => a.IdUtilisateur == idUtilisateur)
                .ToListAsync();
        }

        // DELETE: api/AbonnementUtilisateurApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbonnementUtilisateur(int id)
        {
            var abonnementUtilisateur = await _context.AbonnementUtilisateurs.FindAsync(id);
            if (abonnementUtilisateur == null)
            {
                return NotFound();
            }

            _context.AbonnementUtilisateurs.Remove(abonnementUtilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonnementUtilisateurExists(int id)
        {
            return _context.AbonnementUtilisateurs.Any(e => e.Id == id);
        }
    }
}
