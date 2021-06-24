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
    public class InstitutionCareerController : ControllerBase
    {
        private readonly DescubreContext _context;

        public InstitutionCareerController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionCareer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionCareer>>> GetInstitutionCareer()
        {
            return await _context.InstitutionCareer.ToListAsync();
        }

        // GET: api/InstitutionCareer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionCareer>> GetInstitutionCareer(int id)
        {
            var institutionCareer = await _context.InstitutionCareer.FindAsync(id);

            if (institutionCareer == null)
            {
                return NotFound();
            }

            return institutionCareer;
        }

        // PUT: api/InstitutionCareer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitutionCareer(int id, InstitutionCareer institutionCareer)
        {
            if (id != institutionCareer.Id)
            {
                return BadRequest();
            }

            _context.Entry(institutionCareer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionCareerExists(id))
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

        // POST: api/InstitutionCareer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InstitutionCareer>> PostInstitutionCareer(InstitutionCareer institutionCareer)
        {
            _context.InstitutionCareer.Add(institutionCareer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitutionCareer", new { id = institutionCareer.Id }, institutionCareer);
        }

        // DELETE: api/InstitutionCareer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstitutionCareer(int id)
        {
            var institutionCareer = await _context.InstitutionCareer.FindAsync(id);
            if (institutionCareer == null)
            {
                return NotFound();
            }

            _context.InstitutionCareer.Remove(institutionCareer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstitutionCareerExists(int id)
        {
            return _context.InstitutionCareer.Any(e => e.Id == id);
        }
    }
}
