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
                return BadRequest();
            }

            _context.Entry(career).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CareerExists(id))
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

        // POST: api/Career
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Career>> PostCareer(Career career)
        {
            _context.Career.Add(career);
            await _context.SaveChangesAsync();

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
