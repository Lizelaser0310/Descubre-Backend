using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CareerGuidance.Models;
using Domain.Models;
using Lizelaser0310.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CareerGuidance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly DescubreContext _context;
        private readonly IWebHostEnvironment _env;

        private readonly IKeys _keys;

        public PublicController(DescubreContext context, IWebHostEnvironment env, IKeys keys)
        {
            _context = context;
            _env = env;
            _keys = keys;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<bool>> Check(string input)
        {
            var inputModified = input?.Trim().ToLower();
            var result = await _context.User.AnyAsync(u =>
                (u.Email != null && u.Email.Trim().ToLower() == inputModified) ||
                (u.Username != null && u.Username.Trim().ToLower() == inputModified));

            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<String>> Login(LoginVm json)
        {
            var input = json.Username?.Trim().ToLower();
            var user = await _context.User.Include(u => u.Rol)
                .Where(x =>
                    x.Username.Trim().ToLower() == input || x.Email.Trim().ToLower() == input)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest("El usuario no existe o se encuentra desactivado");
            }

            var verifyPassword =
                AuthUtility.VerifyPassword(json.Password, user.Password, _keys.EncryptionKey);

            if (!verifyPassword)
            {
                return BadRequest("El usuario y/o contraseña son incorrectos");
            }

            // We create the claims (belongings, characteristics) of the user
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Id.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, user.Rol.Denomination)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // Token lifetime
                Expires = DateTime.UtcNow.AddDays(28),
                // Credentials to generate the token using our secret key and the 256 hash algorithm
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_keys.TokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            tokenHandler.WriteToken(createdToken);

            return Ok(tokenHandler.WriteToken(createdToken));
        }
    }
}