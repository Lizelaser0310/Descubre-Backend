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
    public class TestResultController : ControllerBase
    {
        private readonly DescubreContext _context;

        public TestResultController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/TestResult
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestResult>>> GetTestResult()
        {
            return await _context.TestResult.ToListAsync();
        }

        // GET: api/TestResult/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestResult>> GetTestResult(int id)
        {
            var testResult = await _context.TestResult.FindAsync(id);

            if (testResult == null)
            {
                return NotFound();
            }

            return testResult;
        }

        // PUT: api/TestResult/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestResult(int id, TestResult testResult)
        {
            if (id != testResult.Id)
            {
                return BadRequest(ErrorVm.Create("El id del resultado por test no coincide con el objeto enviado"));
            }

            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Entry(testResult).State = EntityState.Modified;
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

        // POST: api/TestResult
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestResult>> PostTestResult(TestResult testResult)
        {
            if (testResult==null)
            {
                return BadRequest();
            }
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                _context.TestResult.Add(testResult);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }

            return CreatedAtAction("GetTestResult", new { id = testResult.Id }, testResult);
        }

        // DELETE: api/TestResult/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestResult(int id)
        {
            var testResult = await _context.TestResult.FindAsync(id);
            if (testResult == null)
            {
                return NotFound();
            }

            _context.TestResult.Remove(testResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestResultExists(int id)
        {
            return _context.TestResult.Any(e => e.Id == id);
        }
    }
}
