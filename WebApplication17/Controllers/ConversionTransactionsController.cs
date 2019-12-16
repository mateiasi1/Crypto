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
            _conversionsManager.GetAllConversionTransactions();
            return Ok(_mapper.Map<IEnumerable<ConversionTransactionDTO>>(_conversionsManager));
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
