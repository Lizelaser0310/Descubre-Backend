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
            if (User.Identity?.Name == null)
            {
                return BadRequest(ErrorVm.Create("El usuario no está registrado"));
            }

            var userid = int.Parse(User.Identity.Name);

            return Ok(await _context.Recomendation
                .Include(r => r.Career)
                .Include(r => r.Result)
                .ThenInclude(r => r.Status)
                .Where(r =>
                    r.Result.UserId == userid &&
                    r.Result.Status.Denomination == $"{ResultStateEnum.Finished}")
                .Select(r => new
                {
                    Id = r.Id,
                    Status = r.Result.Status.Denomination,
                    Career = r.Career.Denomination,
                    EndDate = r.Result.EndDate,
                    Comments = r.Comments
                })
                .ToListAsync());
        }

        // GET: api/Result/5
        [HttpGet("{id:int}")]
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> SaveResult(TestResultDTO result, int id)
        {
            if (User.Identity?.Name == null)
            {
                return BadRequest(ErrorVm.Create("El usuario no está registrado"));
            }

            var userid = int.Parse(User.Identity.Name);

            var currentResult = await _context.Result
                .Include(r => r.Status)
                .Include(r => r.TestResult)
                .FirstOrDefaultAsync(r =>
                    r.UserId == userid && (r.Status.Denomination == $"{ResultStateEnum.Created}" ||
                                           r.Status.Denomination ==
                                           $"{ResultStateEnum.OnProgress}"));

            if (currentResult == null)
            {
                return BadRequest(
                    ErrorVm.Create("Debe iniciar un nueva prueba de orientación vocacional"));
            }

            var statusEnd = await _context.Status
                .Where(s => s.Denomination == $"{ResultStateEnum.Finished}")
                .Select(s => s.Id)
                .FirstOrDefaultAsync();

            int? resId = null;

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                currentResult.StatusId = result.IsLast ? 4 : 2;
                _context.Entry(currentResult).State = EntityState.Modified;

                var testResult = new TestResult()
                {
                    ResultId = currentResult.Id,
                    TestId = result.TestId,
                    ModalityId = result.ModalityId,
                    Total = result.Total,
                };
                _context.TestResult.Add(testResult);
                await _context.SaveChangesAsync();

                if (result.IsLast)
                {
                    var careersModalities = await _context.CareerModality
                        .Include(cm => cm.Career)
                        .ToListAsync();

                    var careerHelper = new Dictionary<int, int>();
                    foreach (var cms in careersModalities.GroupBy(cm => cm.CareerId))
                    {
                        var total = 0;
                        foreach (var cm in cms)
                        {
                            var userTestResult = currentResult.TestResult.SingleOrDefault(tr =>
                                tr.ModalityId == cm.ModalityId);
                            if (userTestResult != null)
                            {
                                total += cm.Weight;
                            }
                        }

                        careerHelper.Add(cms.Key, total);
                    }

                    var careerId = careerHelper
                        .Aggregate((x, y) => x.Value > y.Value ? x : y)
                        .Key;

                    currentResult.StatusId = statusEnd;
                    currentResult.EndDate = DateTime.Now;
                    _context.Entry(currentResult).State = EntityState.Modified;
                    await _context.SaveChangesAsync();


                    var recomendation = new Recomendation()
                    {
                        ResultId = currentResult.Id,
                        CareerId = careerId,
                        Comments = "¡Bien hecho!"
                    };

                    _context.Recomendation.Add(recomendation);
                    await _context.SaveChangesAsync();

                    resId = recomendation.Id;
                }

                await transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                throw;
            }

            if (resId != null)
            {
                _context.ChangeTracker.Clear();
                var recomendation = await _context.Recomendation
                    .Include(r => r.Career)
                    .Include(r => r.Result)
                    .FirstAsync(r => r.Id == resId);
                return Ok(new
                {
                    Career = recomendation.Career.Denomination, Comments = "¡Bien hecho!"
                });
            }

            return NoContent();
        }

        // POST: api/Result
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> CreateResult()
        {
            if (User.Identity?.Name == null)
            {
                return BadRequest(ErrorVm.Create("El usuario no está registrado"));
            }

            var userid = int.Parse(User.Identity.Name);

            var currentResult = await _context.Result.Include(r => r.Status)
                .Where(r =>
                    r.UserId == userid && (r.Status.Denomination == $"{ResultStateEnum.Created}" ||
                                           r.Status.Denomination ==
                                           $"{ResultStateEnum.OnProgress}"))
                .FirstOrDefaultAsync();

            if (currentResult != null)
            {
                if (currentResult.Status.Denomination == $"{ResultStateEnum.Created}")
                {
                    return Ok(currentResult.Id);
                }

                currentResult.StatusId = 3;
                _context.Entry(currentResult).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            var status =
                await _context.Status.FirstOrDefaultAsync(s =>
                    s.Denomination == $"{ResultStateEnum.Created}");

            var result = new Result
            {
                UserId = userid, StatusId = status?.Id ?? 1, StartDate = DateTime.Now
            };

            _context.Result.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResult", new {id = result.Id}, result);
        }

        // DELETE: api/Result/5
        [HttpDelete("{id:int}")]
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
    }
}