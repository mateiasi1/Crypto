using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly BanksManager _banksManager;

        public BankAccountsController(IMapper mapper, BanksManager banksManager)
        {
            _mapper = mapper;
            _banksManager = banksManager;
        }

        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccount()
        {
            var bankList = _banksManager.GetAllBankAccounts();

            return Ok(_mapper.Map<IEnumerable<BankAccountDTO>>(bankList));
        }

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
        {
           var bankAccount = _banksManager.GetBankAccountById(id);

            return bankAccount;
        }
        

        // PUT: api/BankAccounts/add  for Add sold
        [HttpPut("add")]
        public IActionResult AddBankAccount()
        
        {
            string body = this.InputBodyData;
            _banksManager.AddToBankAccount(body);
            return Ok();
        }

        // PUT: api/BankAccounts/withdraw for withdraw sold
        [HttpPut("withdraw")]
        public IActionResult WithdrawBankAccount()
        {
            string body = this.InputBodyData;
            _banksManager.WithdrawFromBankAccount(body);
            return Ok();
        }

        // POST: api/BankAccounts
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
        {
            _banksManager.AddBankAccount(bankAccount);
            return Ok();
        }

        // DELETE: api/BankAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccount>> DeleteBankAccount(int id)
        {
            _banksManager.DeleteBankAccount(id);
            return Ok();
        }

        
    }
}
