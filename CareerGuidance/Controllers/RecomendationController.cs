using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace CareerGuidance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendationController : ControllerBase
    {
        private readonly DescubreContext _context;

        public RecomendationController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/Recomendation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recomendation>>> GetRecomendation()
        {
            return await _context.Recomendation.ToListAsync();
        }

        // GET: api/Recomendation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recomendation>> GetRecomendation(int id)
        {
            var recomendation = await _context.Recomendation.FindAsync(id);

            if (recomendation == null)
            {
                return NotFound();
            }

            return recomendation;
        }

        // PUT: api/Recomendation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecomendation(int id, Recomendation recomendation)
        {
            if (id != recomendation.Id)
            {
                return BadRequest();
            }

            _context.Entry(recomendation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecomendationExists(id))
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

        // POST: api/Recomendation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recomendation>> PostRecomendation(Recomendation recomendation)
        {
            _context.Recomendation.Add(recomendation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecomendation", new { id = recomendation.Id }, recomendation);
        }

        // DELETE: api/Recomendation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomendation(int id)
        {
            var recomendation = await _context.Recomendation.FindAsync(id);
            if (recomendation == null)
            {
                return NotFound();
            }

            _context.Recomendation.Remove(recomendation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecomendationExists(int id)
        {
            return _context.Recomendation.Any(e => e.Id == id);
        }
    }
}
