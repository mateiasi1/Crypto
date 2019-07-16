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
    public class FlatRateFeesController : ControllerBase
    {
        private readonly Contexts _context;

        public FlatRateFeesController(Contexts context)
        {
            _context = context;
        }

        // GET: api/FlatRateFees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlatRateFee>>> GetFlatRateFee()
        {
            return await _context.FlatRateFee.ToListAsync();
        }

        // GET: api/FlatRateFees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlatRateFee>> GetFlatRateFee(int id)
        {
            var flatRateFee = await _context.FlatRateFee.FindAsync(id);

            if (flatRateFee == null)
            {
                return NotFound();
            }

            return flatRateFee;
        }

        // PUT: api/FlatRateFees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlatRateFee(int id, FlatRateFee flatRateFee)
        {
            if (id != flatRateFee.Id)
            {
                return BadRequest();
            }

            _context.Entry(flatRateFee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlatRateFeeExists(id))
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

        // POST: api/FlatRateFees
        [HttpPost]
        public async Task<ActionResult<FlatRateFee>> PostFlatRateFee(FlatRateFee flatRateFee)
        {
            _context.FlatRateFee.Add(flatRateFee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlatRateFee", new { id = flatRateFee.Id }, flatRateFee);
        }

        // DELETE: api/FlatRateFees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlatRateFee>> DeleteFlatRateFee(int id)
        {
            var flatRateFee = await _context.FlatRateFee.FindAsync(id);
            if (flatRateFee == null)
            {
                return NotFound();
            }

            _context.FlatRateFee.Remove(flatRateFee);
            await _context.SaveChangesAsync();

            return flatRateFee;
        }

        private bool FlatRateFeeExists(int id)
        {
            return _context.FlatRateFee.Any(e => e.Id == id);
        }
    }
}
