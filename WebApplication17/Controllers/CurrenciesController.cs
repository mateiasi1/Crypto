using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebApplication17.Models;
using DataLayer.DTO;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.DTO;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            ResponseDTO<CurrencyDTO> response = new ResponseDTO<CurrencyDTO>();
            ListDTO<CurrencyDTO> list = new ListDTO<CurrencyDTO>();

            list = _currenciesManager.GetAllCurrencies();
            response.Data = new ListDTO<CurrencyDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return NotFound(response);
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
            ResponseDTO<CurrencyDTO> response = new ResponseDTO<CurrencyDTO>();
            ListDTO<CurrencyDTO> list = new ListDTO<CurrencyDTO>();

            list = _currenciesManager.AddCurrency(currency);
            response.Data = new ListDTO<CurrencyDTO>();

            if (list != null)
            {
                response.Data = list;
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return NotFound(response);
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
