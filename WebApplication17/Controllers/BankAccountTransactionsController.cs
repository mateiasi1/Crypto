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
    public class BankAccountTransactionsController : ControllerBase
    {
        private readonly BanksManager _banksManager;
        private readonly IMapper _mapper;
        public BankAccountTransactionsController(IMapper mapper, BanksManager banksManager)
        {
            _mapper = mapper;
            _banksManager = banksManager;
        }

        // GET: api/BankAccountTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccountTransaction>>> GetBankAccountTransaction()
        {
            var transactionList = _banksManager.GetAllTransactions();

            return Ok(_mapper.Map<IEnumerable<BankAccountTransactionDTO>>(transactionList));
        }

        // GET: api/BankAccountTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountTransaction>> GetBankAccountTransaction(int id)
        {
            var transactionList = _banksManager.GetTransactionById(id);

            return Ok(_mapper.Map<IEnumerable<BankAccountTransactionDTO>>(transactionList));
        }

       
    }
}
