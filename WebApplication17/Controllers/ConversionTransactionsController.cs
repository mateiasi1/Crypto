using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DataLayer.DTO;
using WebApplication17.Models;
using BusinessLayer.DTO;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionTransactionsController : ControllerBase
    {
        private readonly ConversionsManager _conversionsManager;
        private readonly IMapper _mapper;

        public ConversionTransactionsController(ConversionsManager conversionsManager, IMapper mapper)
        {
            _conversionsManager = conversionsManager;
            _mapper = mapper;
        }

        // GET: api/ConversionTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversionTransaction>>> GetConversionTransaction()
        {
            ResponseDTO<ConversionTransactionDTO> response = new ResponseDTO<ConversionTransactionDTO>();
            ListDTO<ConversionTransactionDTO> list = new ListDTO<ConversionTransactionDTO>();
            var conversions = _conversionsManager.GetAllConversionTransactions();

            if (conversions != null)
            {
                response.Data = conversions;
                response.Message = "Transactions are retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "Transactions are not retrieved successfully";
            response.Success = false;

            return Ok(response);
        }
        // POST: api/ConversionTransactions
        [HttpPost]
        public async Task<ActionResult<ConversionTransaction>> PostConversionTransaction(ConversionTransaction conversionTransaction,string percentage)
        {
            _conversionsManager.AddConversionTransaction(conversionTransaction);
            return Ok(_mapper.Map<IEnumerable<ConversionTransactionDTO>>(_conversionsManager));
        }
    }
}
