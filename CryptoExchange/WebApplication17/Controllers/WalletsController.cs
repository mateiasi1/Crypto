using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly Contexts _context;

        public WalletsController(Contexts context)
        {
            _context = context;
        }

        // GET: api/Wallets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet.Wallet>>> GetWallet()
        {
            return await _context.Wallet.ToListAsync();
        }

        // GET: api/Wallets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet.Wallet>> GetWallet(int id)
        {
            var wallet = await _context.Wallet.FindAsync(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }

        // PUT: api/Wallets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWallet(int id, Wallet.Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
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

        // POST: api/Wallets
        [HttpPost]
        public async Task<ActionResult<Wallet.Wallet>> PostWallet(Wallet.Wallet wallet)
        {
            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWallet", new { id = wallet.Id }, wallet);
        }

        // DELETE: api/Wallets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wallet.Wallet>> DeleteWallet(int id)
        {
            var wallet = await _context.Wallet.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            _context.Wallet.Remove(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        private bool WalletExists(int id)
        {
            return _context.Wallet.Any(e => e.Id == id);
        }
    }
}
