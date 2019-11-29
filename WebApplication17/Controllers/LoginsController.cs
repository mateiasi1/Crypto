using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly Contexts _context;

        public LoginsController(Contexts context)
        {
            _context = context;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
            return await _context.Login.ToListAsync();
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            var login = await _context.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Banks/token
        [HttpPut("{token}")]
        public async Task<IActionResult> PutLogin(Token token)
        {
            Token updateToken = await _context.Token.FindAsync(token);
            updateToken.EndDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Logins
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin([FromBody]Login login)
        {
            int userID = _context.User.Where(item => item.Username == login.Username).Select(item => item.Id).FirstOrDefault();

            var user = _context.User.Find(userID);

            var passwordSalt = user.PasswordSalt;
            string passwordHash = Hash.Create(login.Password, passwordSalt.ToString());

            if (user.Password == passwordHash)
            {
                var thisToken = new Token();

                thisToken.UserId = user.Id;
                thisToken.StartDate = DateTime.Now;
                thisToken.EndDate = DateTime.Now.AddMinutes(60);
                thisToken.TokenGuid = " ";

                login.Token = thisToken;
                _context.Token.Add(thisToken);
                _context.Login.Add(login);
              await  _context.SaveChangesAsync();

                return Ok(login.Token);
            } 
            // de facut request in baza pe baza de username  si de retrive password + verify password + verify consirmed state + return status
            else
            {
                return Unauthorized();
            }
        }

        // DELETE: api/Login/5
        [HttpDelete("{token}")]
        public async Task<ActionResult<Login>> DeleteLogin(string token)
        {
            Token deleteToken = _context.Token.FirstOrDefault(item => item.TokenGuid == token);
            _context.Remove(deleteToken);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
