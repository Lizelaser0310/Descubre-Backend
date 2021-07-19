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
    public class ResultController : ControllerBase
    {
        private readonly DescubreContext _context;

        public ResultController(DescubreContext context)
        {
            _context = context;
        }

        // GET: api/Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResult()
        {
            return await _context.Result.ToListAsync();
        }

        // GET: api/Result/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Result.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Result/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> SaveResult(TestResultDTO result)
        {
            if (User.Identity?.Name==null)
            {
                return BadRequest(ErrorVm.Create("El usuario no está registrado"));
            }
            var userid = int.Parse(User.Identity.Name);
            
            var currentResult = await _context.Result
                .Include(r => r.UserId == userid && r.Status.Denomination == $"{ResultStateEnum.Created}").SingleOrDefaultAsync();

            var statusEnd = await _context.Status.Where(s => s.Denomination == $"{ResultStateEnum.Finished}")
                .Select(s=>s.Id)
                .SingleOrDefaultAsync();
            
            
            if (currentResult==null)
            {
                return BadRequest(ErrorVm.Create("Debe iniciar un nueva prueba de orientación vocacional"));
            }

            List<int> modalitiesIds = new List<int>();

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var testResult = new TestResult()
                {
                    ResultId = currentResult.Id,
                    TestId = result.TestId,
                    ModalityId = result.ModalityId,
                    Total = result.Total,
                };
                _context.TestResult.Add(testResult);
                await _context.SaveChangesAsync();
                
                modalitiesIds.Add(result.ModalityId);
                

                if (result.IsLast)
                {
                    var career = await _context.CareerModality
                        .Include(cm => cm.Career)
                        .Where(cm =>
                            cm.ModalityId == modalitiesIds[0] || cm.ModalityId == modalitiesIds[1] ||
                            cm.ModalityId == modalitiesIds[2]).Select(cm=>cm.CareerId).SingleOrDefaultAsync();

                    currentResult.StatusId = statusEnd;
                    currentResult.EndDate=DateTime.Now;
                    _context.Entry(currentResult).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    

                    var recomendation = new Recomendation()
                    {
                        ResultId = currentResult.Id,
                        CareerId = career,
                        Comments = ""
                    };
                    
                    _context.Recomendation.Add(recomendation);
                    await _context.SaveChangesAsync();
                }
                
                await transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return NoContent();
        }

        // POST: api/Result
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> CreateResult(Result result)
        {
            if (result==null)
            {
                return BadRequest();
            }

            if (User.Identity?.Name==null)
            {
                return BadRequest(ErrorVm.Create("El usuario no está registrado"));
            }

            var userid = int.Parse(User.Identity.Name);

            var currentResult = await _context.Result.Include(r=>r.Status)
                .Where(r => r.UserId== userid || r.Status.Denomination==$"{ResultStateEnum.Created}" || r.Status.Denomination == $"{ResultStateEnum.OnProgress}")
                .AnyAsync();

            if (currentResult)
            {
                return BadRequest(ErrorVm.Create("No puedes iniciar un nuevo test sin haber terminado el anterior"));
            }
            
            var status = await _context.Status.FindAsync(result.StatusId);

            if (status==null)
            {
                return BadRequest(ErrorVm.Create("Debe asignar un estado al test"));
            }
            

            await using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                result.StartDate = DateTime.Now;
                _context.Result.Add(result);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }

            return CreatedAtAction("GetResult", new { id = result.Id }, result);
        }

        // DELETE: api/Result/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.Result.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultExists(int id)
        {
            return _context.Result.Any(e => e.Id == id);
        }
    }
}
