using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.Data.Registration;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly Contexts _context;

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
            var passwordSalt = new Salt();
            string passwordHash = Hash.Create(registerUser.Password, passwordSalt.ToString());
            registerUser.PasswordSalt = passwordSalt.ToString();
            registerUser.PasswordHash = passwordHash;
            _context.RegisterUser.Add(registerUser);

            await _context.SaveChangesAsync();

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
