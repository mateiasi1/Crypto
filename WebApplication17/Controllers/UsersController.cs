using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Email;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        readonly EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("D:/Didactical/back/ClassLibrary1/Email/EmailTemplate.html"));

        public UsersController(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        [Route("api/[controller]/addcurrency")]
        [HttpPost]
        public async Task<ActionResult<Currency>> PostUser(Currency currency, int UserId)
        {
            var currentUser = await _context.User.FindAsync(UserId);
            if (currentUser.Role == "admin")
            {
                _context.Currency.Add(currency);
                await _context.SaveChangesAsync();
                return Ok("Currency succesfull added");
            }
            else
            {
                return NotFound("Only andmin users can add a currency!");
            }
        }

        // GET: api/Users/confirmed
        [HttpGet]
        public IActionResult GetConfirmedUsers()
        {
            var users = _context.User.Where(u => u.Confirmed == true);
            return Ok(_mapper.Map<IEnumerable<UnconfirmedUsersDTO>>(users));
        }

        

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok() ;
        }


        // GET: api/Users/unconfirmed/id- return the list of unconfirmed users
        //[HttpGet("1")]
        //public async Task<List<User>> GetUnconfirmedUsers(bool confirmed)
        //{
        //    List<User> users = await _context.User.Where(u => u.Confirmed == confirmed).ToListAsync();
        //    return users;
        //}
        [HttpGet("1")]
        public  IActionResult GetUnconfirmedUsers(bool confirmed)
        {
            var users = _context.User.Where(u => u.Confirmed == confirmed);
            return Ok(_mapper.Map<IEnumerable<UnconfirmedUsersDTO>>(users));
        }


        // PUT: api/Users
        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody]int id)
        {
            var user = await _context.User.FindAsync(id);
            if (id != user.Id)
            {
                return BadRequest();
            }
            user.Password = null;
            await _context.SaveChangesAsync();
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            EmailModel model = new EmailModel();
            model.EmailTo = "dragosdm22@gmail.com";
            model.Subject = "test subject";
            model.Message = Body + "http://localhost:4200/validateAccount/" + user.Id + " " + user.Username + user.Token;
            model.UserId = user.Id;
            emailService.SendEmail(model);
            // de legat de serviciul de email si de trimis GUID + Id in link ul de email pentru resetare
            return Ok();
        }

        // PUT: api/Users/confirmed
        [HttpPut("{Confirmed}")]
        public async Task<IActionResult> ConfirmUser([FromBody]int id, bool confirmed)
        {
            var user = await _context.User.FindAsync(id);
            if (id != user.Id)
            {
                return BadRequest();
            }
            user.Confirmed = true;
            await _context.SaveChangesAsync();
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // de legat de serviciul de email si de trimis GUID + Id in link ul de email pentru resetare
            return Ok();
        }

        // POST: api/Users reset password
        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody]int id, string password, string token)
        {
            var user = await _context.User.FindAsync(id);
            if (UserExists(id) && user.Token == token)
            {
                var passwordSalt = new Salt();
                string passwordHash = Hash.Create(user.Password, passwordSalt.ToString());
                user.Password = passwordHash;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut ("suspend")]
        public async Task<ActionResult> SuspendUser([FromBody]int id)
        {
            var user = await _context.User.FindAsync(id);
            user.Confirmed = false;
            _context.SaveChanges();
            return Ok();

           

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
