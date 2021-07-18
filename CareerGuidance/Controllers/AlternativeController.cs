using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerGuidance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace CareerGuidance.Controllers
{
    [Authorize(Roles="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlternativeController : ControllerBase
    {
        private readonly DescubreContext _context;

        public AlternativeController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/Alternative
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alternative>>> GetAlternative()
        {
            return await _context.Alternative.ToListAsync();
        }

        // GET: api/Alternative/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alternative>> GetAlternative(int id)
        {
            var alternative = await _context.Alternative.FindAsync(id);

            if (alternative == null)
            {
                return NotFound();
            }

            return alternative;
        }

        // PUT: api/Alternative/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlternative(int id, Alternative alternative)
        {
            if (id != alternative.Id)
            {
                return BadRequest(ErrorVm.Create("El id de la pregunta no coincide con el objeto enviado"));
            }
            
            var alternativeDb = await _context.Alternative.SingleOrDefaultAsync(r => r.Id == id);

            if (alternativeDb==null)
            {
                return BadRequest(ErrorVm.Create("La alternativa no existe"));
            }
            
            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Entry(alternative).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            return NoContent();
        }

        // POST: api/Alternative
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alternative>> PostAlternative(Alternative alternative)
        {
            if (alternative==null)
            {
                return BadRequest();
            }
            var dbAlternative = await _context.Alternative
                .Where(t => t.ModalityId==alternative.ModalityId && t.Denomination.Equals(alternative.Denomination))
                .SingleOrDefaultAsync();
            
            if (dbAlternative != null) return BadRequest(new { error = "La alternativa ya existe" });
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                _context.Alternative.Add(alternative);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }

            return CreatedAtAction("GetALternative", new { id = alternative.Id }, alternative);
        }

        // DELETE: api/Alternative/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteALternative(int id)
        {
            var alternative = await _context.Alternative.FindAsync(id);
            if (alternative == null)
            {
                return NotFound();
            }

            _context.Alternative.Remove(alternative);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlternativeExists(int id)
        {
            return _context.Alternative.Any(e => e.Id == id);
        }
    }
}
