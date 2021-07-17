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
    public class CareerModalityController : ControllerBase
    {
        private readonly DescubreContext _context;

        public CareerModalityController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/CareerModality
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerModality>>> GetCareerModality()
        {
            return await _context.CareerModality.ToListAsync();
        }

        // GET: api/CareerModality/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CareerModality>> GetCareerModality(int id)
        {
            var careerModality = await _context.CareerModality.FindAsync(id);

            if (careerModality == null)
            {
                return NotFound();
            }

            return careerModality;
        }

        // PUT: api/CareerModality/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCareerModality(int id, CareerModality careerModality)
        {
            if (id != careerModality.Id)
            {
                return BadRequest(ErrorVm.Create("El id de la modalidad por carrera no coincide con el objeto enviado"));
            }
            
            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Entry(careerModality).State = EntityState.Modified;
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

        // POST: api/CareerModality
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CareerModality>> PostCareerModality(CareerModality careerModality)
        {
            if (careerModality==null)
            {
                return BadRequest();
            }
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                _context.CareerModality.Add(careerModality);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }

            return CreatedAtAction("GetCareerModality", new { id = careerModality.Id }, careerModality);
        }

        // DELETE: api/CareerModality/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCareerModality(int id)
        {
            var careerModality = await _context.CareerModality.FindAsync(id);
            if (careerModality == null)
            {
                return NotFound();
            }

            _context.CareerModality.Remove(careerModality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CareerModalityExists(int id)
        {
            return _context.CareerModality.Any(e => e.Id == id);
        }
    }
}
