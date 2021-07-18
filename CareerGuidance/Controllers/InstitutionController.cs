using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerGuidance.Models;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerGuidance.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly DescubreContext _context;

        public InstitutionController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/Institution
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Institution>>> GetInstitution()
        {
            return await _context.Institution.ToListAsync();
        }

        // GET: api/Institution/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Institution>> GetInstitution(int id)
        {
            var institution = await _context.Institution.FindAsync(id);

            if (institution == null)
            {
                return NotFound();
            }

            return institution;
        }

        // PUT: api/Institution/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitution(int id, Institution institution)
        {
            var institutionDb = await _context.Institution.SingleOrDefaultAsync(r => r.Id == id);

            if (institutionDb==null)
            {
                return BadRequest(ErrorVm.Create("La institución no existe"));
            }
            
            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                institution.UpdatedAt = DateTime.Now;
                _context.Entry(institution).State = EntityState.Modified;
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

        // POST: api/Institution
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Institution>> PostInstitution(Institution institution)
        {
            if (institution==null)
            {
                return BadRequest();
            }
            
            var dbInstitution = await _context.Institution
                .Where(t => t.Denomination.Equals(institution.Denomination))
                .SingleOrDefaultAsync();
            
            if (dbInstitution != null) return BadRequest(new { error = "La institución ya existe" });
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                institution.CreatedAt = DateTime.Now;
                institution.Status = true;
                _context.Institution.Add(institution);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }


            return CreatedAtAction("GetInstitution", new { id = institution.Id }, institution);
        }

        // DELETE: api/Institution/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var institution = await _context.Institution.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }

            _context.Institution.Remove(institution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstitutionExists(int id)
        {
            return _context.Institution.Any(e => e.Id == id);
        }
    }
}
