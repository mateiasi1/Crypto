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
    public class ClientCurrenciesController : ControllerBase
    {
        private readonly Contexts _context;

        public ClientCurrenciesController(Contexts context)
        {
            _context = context;
        }

        // GET: api/ClientCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientCurrency>>> GetClientCurrency()
        {
            return await _context.ClientCurrency.ToListAsync();
        }

        // GET: api/ClientCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCurrency>> GetClientCurrency(int id)
        {
            var clientCurrency = await _context.ClientCurrency.FindAsync(id);

            if (clientCurrency == null)
            {
                return NotFound();
            }

            return clientCurrency;
        }

        // PUT: api/ClientCurrencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientCurrency(int id, ClientCurrency clientCurrency)
        {
            if (id != clientCurrency.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientCurrencyExists(id))
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

        // POST: api/ClientCurrencies
        [HttpPost]
        public async Task<ActionResult<ClientCurrency>> PostClientCurrency(ClientCurrency clientCurrency)
        {
            _context.ClientCurrency.Add(clientCurrency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientCurrency", new { id = clientCurrency.Id }, clientCurrency);
        }

        // DELETE: api/ClientCurrencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientCurrency>> DeleteClientCurrency(int id)
        {
            var clientCurrency = await _context.ClientCurrency.FindAsync(id);
            if (clientCurrency == null)
            {
                return NotFound();
            }

            _context.ClientCurrency.Remove(clientCurrency);
            await _context.SaveChangesAsync();

            return clientCurrency;
        }

        private bool ClientCurrencyExists(int id)
        {
            return _context.ClientCurrency.Any(e => e.Id == id);
        }
    }
}
