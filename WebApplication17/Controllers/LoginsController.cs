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
            string passHashFromDB = _context.User.Where(item => item.Username == login.Username).Select(item => item.Password).FirstOrDefault();
            int userID = _context.User.Where(item => item.Username == login.Username).Select(item => item.Id).FirstOrDefault();
            Token thisToken = _context.Token.FirstOrDefault(item => item.UserId == userID);

            var passwordSalt = new Salt();
            string passwordHash = Hash.Create(login.Password, passwordSalt.ToString());
            if (passHashFromDB == passwordHash)
            {

                if (thisToken == null || thisToken.TokenGuid == login.Token && thisToken.EndDate >= DateTime.Now)
                {
                    var guid = Guid.NewGuid();
                    thisToken = new Token();
                    thisToken.UserId = userID;
                    thisToken.StartDate = DateTime.Now;
                    thisToken.EndDate = DateTime.Now.AddMinutes(60);
                    thisToken.TokenGuid = guid;
                    login.Token = guid;
                    _context.Token.Add(thisToken);
                    _context.SaveChanges();
                }
                else
                {
                    thisToken.EndDate = thisToken.EndDate.AddMinutes(60);
                    _context.SaveChanges();

                }
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
        public async Task<ActionResult<Login>> DeleteLogin(Guid token)
        {
            Token updateToken = _context.Token.FirstOrDefault(item => item.TokenGuid == token);
            _context.Remove(updateToken);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
