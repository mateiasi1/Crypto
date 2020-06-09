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
            ResponseDTO<CryptoCurrencyDTO> response = new ResponseDTO<CryptoCurrencyDTO>();
            ListDTO<CryptoCurrencyDTO> list = new ListDTO<CryptoCurrencyDTO>();

            list = _currenciesManager.GetAllCryptoCurrencies();
            response.Data = new ListDTO<CryptoCurrencyDTO>();
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
        public async Task<ActionResult<CryptoCurrency>> GetCryptoCurrency(int id)
        {
            ResponseDTO<CryptoCurrencyDTO> response = new ResponseDTO<CryptoCurrencyDTO>();
            ListDTO<CryptoCurrencyDTO> list = new ListDTO<CryptoCurrencyDTO>();

            list = _currenciesManager.GetCryptoCurrencyById(id);
            response.Data = new ListDTO<CryptoCurrencyDTO>();
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

        // POST: api/Currencies
        [HttpPost]
        public async Task<ActionResult<CryptoCurrency>> PostCurrency(CryptoCurrency cryptoCurrency)
        {
            

            ResponseDTO<CryptoCurrencyDTO> response = new ResponseDTO<CryptoCurrencyDTO>();
            ListDTO<CryptoCurrencyDTO> list = new ListDTO<CryptoCurrencyDTO>();

            list = _currenciesManager.AddCryptoCurrency(cryptoCurrency);
            response.Data = new ListDTO<CryptoCurrencyDTO>();
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
        public async Task<ActionResult<CryptoCurrency>> DeleteCurrency(int id)
        {

            ResponseDTO<CryptoCurrencyDTO> response = new ResponseDTO<CryptoCurrencyDTO>();
            ListDTO<CryptoCurrencyDTO> list = new ListDTO<CryptoCurrencyDTO>();

            list = _currenciesManager.DeleteCryptoCurrency(id);
            response.Data = new ListDTO<CryptoCurrencyDTO>();
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

    }
}
