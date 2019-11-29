using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.Email;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly Contexts _context;
        EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("Email/EmailTemplate.html"));
        

        public RegisterUsersController(Contexts context)
        {
            _context = context;
        }

        // GET: api/RegisterUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterUser>>> GetRegisterUser()
        {
            return await _context.RegisterUser.ToListAsync();
        }

        // GET: api/RegisterUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterUser>> GetRegisterUser(int id)
        {
            var registerUser = await _context.RegisterUser.FindAsync(id);

            if (registerUser == null)
            {
                return NotFound();
            }

            return registerUser;
        }

        // PUT: api/RegisterUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegisterUser(int id, RegisterUser registerUser)
        {
            if (id != registerUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(registerUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterUserExists(id))
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

        // POST: api/RegisterUsers
        [HttpPost]
        public async Task<ActionResult<RegisterUser>> PostRegisterUser([FromBody]RegisterUser registerUser)
        {
            Random rnd = new Random();
            int referralRandom = rnd.Next(1000000, 9999999);
            registerUser.ReferralId = "CRYPTOAPP" + registerUser.Id.ToString() + registerUser.Username + referralRandom.ToString();
            Salt salt = new Salt();
            var passwordSalt = salt.ReturnSalt();
            string passwordHash = Hash.Create(registerUser.Password, passwordSalt.ToString());
            registerUser.PasswordSalt = passwordSalt.ToString();
            registerUser.PasswordHash = passwordHash;
            registerUser.Password = "admin";
            registerUser.PhoneNumber = " ";
            User user = new User();
            user.Password = registerUser.PasswordHash;
            user.Username = registerUser.Username;
            user.ReferralId = registerUser.ReferralId;
            user.PasswordSalt = passwordSalt.ToString();
            user.Confirmed = false;
            user.Role = registerUser.Role;
            //_context.RegisterUser.Add(registerUser);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            EmailModel model = new EmailModel();
            model.EmailTo = "dragosdm22@gmail.com";
            model.Subject = "test subject";
            model.Message = Body + "http://localhost:4200/validateAccount/" + user.Id + " " + user.Username + user.Token;
            model.UserId = user.Id;
            emailService.SendEmail(model);

            return CreatedAtAction("GetRegisterUser", new { id = registerUser.Id }, registerUser);
        }

        // DELETE: api/RegisterUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RegisterUser>> DeleteRegisterUser(int id)
        {
            var registerUser = await _context.RegisterUser.FindAsync(id);
            if (registerUser == null)
            {
                return NotFound();
            }

            _context.RegisterUser.Remove(registerUser);
            await _context.SaveChangesAsync();

            return registerUser;
        }

        private bool RegisterUserExists(int id)
        {
            return _context.RegisterUser.Any(e => e.Id == id);
        }
    }
}
