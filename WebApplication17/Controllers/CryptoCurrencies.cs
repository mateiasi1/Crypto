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
    public class CryptoCurrencies : ControllerBase
    {
            private readonly IMapper _mapper;
        private readonly CurrenciesManager _currenciesManager;

        public CryptoCurrencies(IMapper mapper, CurrenciesManager currenciesManager)
            {
                _mapper = mapper;
            _currenciesManager = currenciesManager;
            }

            // GET: api/Currencies
            [HttpGet]
            public async Task<ActionResult<IEnumerable<CryptoCurrency>>> GetCurrency()
            {
            var cryptoCurrencyList = _currenciesManager.GetAllCryptoCurrencies();
            return Ok(_mapper.Map<IEnumerable<CryptoCurrencyDTO>>(cryptoCurrencyList));
        }

            // GET: api/Currencies/5
            [HttpGet("{id}")]
            public async Task<ActionResult<CryptoCurrency>> GetCryptoCurrency(int id)
            {
                var cryptoCurrency = _currenciesManager.GetCryptoCurrencyById(id);

            return Ok(_mapper.Map<IEnumerable<CryptoCurrencyDTO>>(cryptoCurrency));
            }

        // POST: api/Currencies
        [Authorize(Roles = "Admin")]
        [HttpPost]
            public async Task<ActionResult<Currency>> PostCurrency(CryptoCurrency cryptoCurrency)
            {
            _currenciesManager.AddCryptoCurrency(cryptoCurrency);
            return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(cryptoCurrency));
        }

        // DELETE: api/Currencies/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
            public async Task<ActionResult<CryptoCurrency>> DeleteCurrency(int id)
            {
            _currenciesManager.DeleteCryptoCurrency(id);

            return Ok();
        }

    }
}
