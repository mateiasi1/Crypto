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
    public class ConversionTransactionsController : ControllerBase
    {
        private readonly Contexts _context;

        public ConversionTransactionsController(Contexts context)
        {
            _context = context;
        }

        // GET: api/ConversionTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversionTransaction>>> GetConversionTransaction()
        {
            return await _context.ConversionTransaction.ToListAsync();
        }

        // GET: api/ConversionTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConversionTransaction>> GetConversionTransaction(int id)
        {
            var conversionTransaction = await _context.ConversionTransaction.FindAsync(id);

            if (conversionTransaction == null)
            {
                return NotFound();
            }

            return conversionTransaction;
        }

        // PUT: api/ConversionTransactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversionTransaction(int id, ConversionTransaction conversionTransaction)
        {
            if (id != conversionTransaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(conversionTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversionTransactionExists(id))
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

        // POST: api/ConversionTransactions
        [HttpPost]
        public async Task<ActionResult<ConversionTransaction>> PostConversionTransaction(ConversionTransaction conversionTransaction,string percentage)
        {
            conversionTransaction.Ammount-=((Convert.ToDouble(percentage)/100)*conversionTransaction.Ammount);
            _context.ConversionTransaction.Add(conversionTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConversionTransaction", new { id = conversionTransaction.Id }, conversionTransaction);
        }

        // DELETE: api/ConversionTransactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConversionTransaction>> DeleteConversionTransaction(int id)
        {
            var conversionTransaction = await _context.ConversionTransaction.FindAsync(id);
            if (conversionTransaction == null)
            {
                return NotFound();
            }

            _context.ConversionTransaction.Remove(conversionTransaction);
            await _context.SaveChangesAsync();

            return conversionTransaction;
        }

        private bool ConversionTransactionExists(int id)
        {
            return _context.ConversionTransaction.Any(e => e.Id == id);
        }
    }
}
