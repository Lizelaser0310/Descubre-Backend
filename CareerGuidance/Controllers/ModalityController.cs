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
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ModalityController : ControllerBase
    {
        private readonly DescubreContext _context;

        public ModalityController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/Modality
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modality>>> GetModality()
        {
            return await _context.Modality.ToListAsync();
        }

        // GET: api/Modality/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modality>> GetModality(int id)
        {
            var modality = await _context.Modality.FindAsync(id);

            if (modality == null)
            {
                return NotFound();
            }

            return modality;
        }

        // PUT: api/Modality/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModality(int id, Modality modality)
        {
            if (id != modality.Id)
            {
                return BadRequest(ErrorVm.Create("El id de la modalidad no coincide con el objeto enviado"));
            }
            
            var modalityDb = await _context.Modality.SingleOrDefaultAsync(r => r.Id == id);

            if (modalityDb==null)
            {
                return BadRequest(ErrorVm.Create("La modalidad no existe"));
            }
            
            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                modality.UpdatedAt = DateTime.Now;
                _context.Entry(modality).State = EntityState.Modified;
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

        // POST: api/Modality
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Modality>> PostModality(Modality modality)
        {
            if (modality==null)
            {
                return BadRequest();
            }
            var dbModality = await _context.Modality
                .Where(t => t.Denomination.Equals(modality.Denomination))
                .SingleOrDefaultAsync();
            
            if (dbModality != null) return BadRequest(new { error = "La modalidad ya existe" });
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                modality.CreatedAt = DateTime.Now;
                modality.Status = true;
                _context.Modality.Add(modality);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }
            return CreatedAtAction("GetModality", new { id = modality.Id }, modality);
        }

        // DELETE: api/Modality/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModality(int id)
        {
            var modality = await _context.Modality.FindAsync(id);
            if (modality == null)
            {
                return NotFound();
            }

            _context.Modality.Remove(modality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModalityExists(int id)
        {
            return _context.Modality.Any(e => e.Id == id);
        }
    }
}
