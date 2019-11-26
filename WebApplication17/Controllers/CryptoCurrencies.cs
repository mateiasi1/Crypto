using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.Models;
using WebApplication17.DTO;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCurrencies : ControllerBase
    {
            private readonly Contexts _context;
            private readonly IMapper _mapper;

            public CryptoCurrencies(Contexts context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            // GET: api/Currencies
            [HttpGet]
            public async Task<ActionResult<IEnumerable<CryptoCurrency>>> GetCurrency()
            {
                return await _context.CryptoCurrency.ToListAsync();
            }

            // GET: api/Currencies/5
            [HttpGet("{id}")]
            public async Task<ActionResult<CryptoCurrency>> GetCurrency(int id)
            {
                var cryptoCurrency = await _context.CryptoCurrency.FindAsync(id);

                if (cryptoCurrency == null)
                {
                    return NotFound();
                }

                return cryptoCurrency;
            }

            // PUT: api/Currencies/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCryptoCurrency(int id, CryptoCurrency cryptoCurrency)
            {
                if (id != cryptoCurrency.Id)
                {
                    return BadRequest();
                }

                _context.Entry(cryptoCurrency).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(id))
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

            // POST: api/Currencies
            [HttpPost]
            public async Task<ActionResult<Currency>> PostCurrency(CryptoCurrency cryptoCurrency)
            {
                _context.CryptoCurrency.Add(cryptoCurrency);
                _context.SaveChanges();
                var cryptoCurrencyList = _context.CryptoCurrency.ToList();
                return Ok(_mapper.Map<IEnumerable<CryptoCurrency>>(cryptoCurrencyList));
            }

            // DELETE: api/Currencies/5
            [HttpDelete("{id}")]
            public async Task<ActionResult<CryptoCurrency>> DeleteCurrency(int id)
            {
                var cryptoCurrency = await _context.CryptoCurrency.FindAsync(id);
                if (cryptoCurrency == null)
                {
                    return NotFound();
                }

                _context.CryptoCurrency.Remove(cryptoCurrency);
                await _context.SaveChangesAsync();

                return cryptoCurrency;
            }

            private bool CurrencyExists(int id)
            {
                return _context.CryptoCurrency.Any(e => e.Id == id);
            }
    }
}
