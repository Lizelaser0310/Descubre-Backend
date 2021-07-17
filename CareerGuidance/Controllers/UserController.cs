using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerGuidance.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Lizelaser0310.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace CareerGuidance.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DescubreContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IKeys _keys;

        public UserController(DescubreContext context, IKeys keys, IWebHostEnvironment env)
        {
            _context = context;
            _keys = keys;
            _env = env;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest(ErrorVm.Create("El id del usuario no coincide con el objeto enviado"));
            }

            var userDb = await _context.User.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb==null)
            {
                return BadRequest(ErrorVm.Create("El usuario no existe"));
            }
            
            _context.ChangeTracker.Clear();
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.UpdatedAt = DateTime.Now;
                user.Password = user.Password!=null? AuthUtility.HashPassword(user.Password,_keys.EncryptionKey):userDb.Password;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user==null)
            {
                return BadRequest();
            }
            var dbUser = await _context.User
                .Where(u => u.Username.Equals(user.Username) && u.Email.Equals(user.Email) && u.Dni.Equals(u.Dni))
                .SingleOrDefaultAsync();
            
            if (dbUser != null) return BadRequest(new { error = "El usuario ya existe" });
            
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                user.Password = AuthUtility.HashPassword(user.Password, _keys.EncryptionKey);
                user.CreatedAt = DateTime.Now;
                user.Status = true;
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.CommitAsync();
                throw;
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
