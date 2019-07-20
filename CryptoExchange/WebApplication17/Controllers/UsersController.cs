﻿using System;
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
    public class UsersController : ControllerBase
    {
        private readonly Contexts _context;

        public UsersController(Contexts context)
        {
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

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUses()
        {
            return await _context.User.ToListAsync();
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

            return user;
        }


        // GET: api/Users/Confirmed - return the list of unconfirmed users
        [HttpGet("{Confirmed}")]
        public async Task<ActionResult<User>> GetUnconfirmedUsers(bool confirmed)
        {
            List<User> users = await _context.User.Where(u=>u.Confirmed==confirmed).ToListAsync();

            if (users == null)
            {
                return NotFound();
            }
            foreach (var user in users)
            {

            }
            return Ok();
        }
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            user.Confirmed = false;
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

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
