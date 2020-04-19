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
using Microsoft.AspNetCore.Authorization;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CurrenciesManager _currenciesManager;

        public CurrenciesController(IMapper mapper, CurrenciesManager currenciesManager)
        {
            _mapper = mapper;
            _currenciesManager = currenciesManager;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrency()
        {
           var currencyList = _currenciesManager.GetAllCurrencies();
            return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(currencyList));
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency =  _currenciesManager.GetCurrencyById(id);

            if (currency == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(currency));
        }

        // POST: api/Currencies
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            _currenciesManager.AddCurrency(currency);
            return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(currency));
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Currency>> DeleteCurrency(int id)
        {
            _currenciesManager.DeleteCurrency(id);

            return Ok();
        }
    }
}
