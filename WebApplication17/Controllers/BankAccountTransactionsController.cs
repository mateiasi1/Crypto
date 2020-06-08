﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using DataLayer.DTO;
using WebApplication17.Models;
using BusinessLayer.DTO;

namespace DataLayer.Controllers
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
            ResponseDTO<BankAccountTransactionDTO> response = new ResponseDTO<BankAccountTransactionDTO>();
            ListDTO<BankAccountTransactionDTO> list = new ListDTO<BankAccountTransactionDTO>();
            var bankList = _banksManager.GetAllTransactions();

            if (bankList != null)
            {
                response.Data = bankList;
                response.Message = "Transactions are retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "Transactions are not retrieved successfully";
            response.Success = false;

            return Ok(response);
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
