using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerGuidance.Controllers
{
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
            if (id != institution.Id)
            {
                return BadRequest();
            }

            _context.Entry(institution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionExists(id))
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

        // POST: api/Institution
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Institution>> PostInstitution(Institution institution)
        {
            _context.Institution.Add(institution);
            await _context.SaveChangesAsync();

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
