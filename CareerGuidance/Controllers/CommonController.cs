using System;
using System.Linq;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using CareerGuidance.Models;
using Domain.Models;
using Lizelaser0310.Utilities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace CareerGuidance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly DescubreContext _context;
        private readonly IWebHostEnvironment _env;

        private readonly IKeys _keys;

        public CommonController(DescubreContext db, SearchClient algolia,
            IKeys keys, IWebHostEnvironment env)
        {
            _context = db;
            _keys = keys;
            _env = env;
        }

        // Authentication methods

        [HttpGet]
        [Route("get-user")]
        public async Task<ActionResult<User>> GetUser()
        {
            if (User.Identity?.Name==null)
            {
                return BadRequest();
            }
            var user = await _context.User
                .Where(x => x.Id == int.Parse(User.Identity.Name))
                .SingleOrDefaultAsync();

            ImageUtility.CreateImageUrl(user, Request, "Foto");

            if (user == null) return BadRequest("El usuario no existe");

            return Ok(user);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> EncryptPassword()
        {
            /*if (User.Identity?.Name==null)
            {
                return BadRequest();
            }*/
            var user = await _context.User.FindAsync(1);
            var passwordEncrypt = AuthUtility.HashPassword(user.Password,_keys.EncryptionKey);

            user.Password = passwordEncrypt;
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<ActionResult<User>> ChangePassword(string newPassword, string currentPassword,
            byte[] encryptionKey)
        {
            if (User.Identity?.Name != null)
            {
                int userid = int.Parse(User.Identity?.Name);
                var user = await (from u in _context.User where u.Id.Equals(userid) select u).FirstOrDefaultAsync();
                if (user != null && AuthUtility.VerifyPassword(currentPassword, user.Password, encryptionKey))
                {
                    user.Password = AuthUtility.HashPassword(newPassword, _keys.EncryptionKey);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }

            return BadRequest(
                "Hubo un error al actualizar su contraseña, verifique que la contraseña actual sea la correcta");
        }

        [HttpGet]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword(string email)
        {
            var user =
                await (from u in _context.User
                        where u.Email.Equals(email) || u.Username.Equals(email)
                        select u)
                    .FirstOrDefaultAsync();
            if (user != null)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Lizeth La Serna", "lizssdhdd@zohomail.com"));
                message.To.Add(new MailboxAddress(user.Username, user.Email));
                message.Subject = "Solicitud de cambio de contraseña";

                var builder = new BodyBuilder();
                builder.HtmlBody =
                    $"<div>Hola, {user.Username}:</div><div>Ingrese a este <a href=\"https://google.com\">enlace</a> para recuperar su contraseña.</div>";


                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.zoho.com", 465, true);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("lizssdhdd@zohomail.com", "OtLcCtkbOyK8iye");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }

                return Ok();
            }

            return BadRequest("El usuario no existe");
        }
    }
}