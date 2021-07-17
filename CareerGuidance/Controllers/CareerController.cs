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
    public class CareerController : ControllerBase
    {
        private readonly DescubreContext _context;

        public CareerController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/Career
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Career>>> GetCareer()
        {
            return await _context.Career.ToListAsync();
        }

        // GET: api/Career/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Career>> GetCareer(int id)
        {
            var career = await _context.Career.FindAsync(id);

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }

        // PUT: api/Career/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCareer(int id, Career career)
        {
            if (id != career.Id)
            {
                return BadRequest(ErrorVm.Create("El id de la carrera no coincide con el objeto enviado"));
            }
            
            var careerDb = await _context.Career.SingleOrDefaultAsync(r => r.Id == id);

            if (careerDb==null)
            {
                return BadRequest(ErrorVm.Create("La carrera no existe"));
            }
            
            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                career.UpdatedAt = DateTime.Now;
                _context.Entry(career).State = EntityState.Modified;
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

        // POST: api/Career
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Career>> PostCareer(Career career)
        {
            if (career==null)
            {
                return BadRequest();
            }
            var dbCareer = await _context.Career
                .Where(c => c.Denomination.Equals(career.Denomination))
                .SingleOrDefaultAsync();
            
            if (dbCareer != null) return BadRequest(new { error = "La carrera ya existe" });
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                career.CreatedAt = DateTime.Now;
                career.Status = true;
                _context.Career.Add(career);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }
            return CreatedAtAction("GetCareer", new { id = career.Id }, career);
        }

        // DELETE: api/Career/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareer(int id)
        {
            var career = await _context.Career.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }

            _context.Career.Remove(career);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CareerExists(int id)
        {
            return _context.Career.Any(e => e.Id == id);
        }
    }
}
