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
using BusinessLayer;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        

        public CurrenciesController(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrency()
        {
            CurrenciesManager currencies = new CurrenciesManager(_context);
            currencies.GetAllCurrencies();
            return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(currencies));
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(int id, Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

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
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            CurrenciesManager currencies = new CurrenciesManager(_context);
            currencies.AddCurrency(currency);
            var currencyList = currencies.GetAllCurrencies();
            return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(currencyList));
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Currency>> DeleteCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();

            return currency;
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
