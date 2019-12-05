using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Contexts _context;
        private readonly CurrenciesManager _currenciesManager;

        public CryptoController(Contexts context, IMapper mapper, CurrenciesManager currenciesManager)
        {
            _context = context;
            _mapper = mapper;
            _currenciesManager = currenciesManager;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCrypto()
        {
            return _currenciesManager.GetAllCryptoCurrencies();
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoCurrency>> GetCrypto(int id)
        {
            var crypto = _currenciesManager.GetCryptoCurrencyById(id);

            if (crypto == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CryptoCurrencyDTO>>(_currenciesManager));
        }

       
        // POST: api/Banks
        [HttpPost]
        public async Task<ActionResult<Crypto>> PostCrypto(Crypto crypto)
        {

            var cryptoList = _currenciesManager.AddCryptoCurrency(crypto);
            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(cryptoList));
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crypto>> DeleteCrypto(int id)
        {
            var cryptoList = _currenciesManager.DeleteCryptoCurrency(id);
            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(cryptoList));
        }

        private bool CryptoExists(int id)
        {
            return _context.Crypto.Any(e => e.Id == id);
        }
    }
}
