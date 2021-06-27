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

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<String>> Login(LoginVm json)
        {
            User user = await _context.User.Include(u=>u.Rol)
                .Where(x => x.Username.Equals(json.Username) || x.Email.Equals(json.Username))
                .FirstOrDefaultAsync();

            if (user != null)
            {
                bool verifyPassword = AuthUtility.VerifyPassword(json.Password, user.Password, _keys.EncryptionKey);
                if (verifyPassword)
                {
                    // We create the claims (belongings, characteristics) of the user
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, user.Rol.Denomination));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        // Token lifetime
                        Expires = DateTime.UtcNow.AddDays(28),
                        // Credentials to generate the token using our secret key and the 256 hash algorithm
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_keys.TokenKey),
                            SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(createdToken);

                    return Ok(tokenHandler.WriteToken(createdToken));
                }

                return BadRequest("El usuario y/o contraseña son incorrectos");
            }

            return BadRequest("El usuario no existe o se encuentra desactivado");
        }
    }
}