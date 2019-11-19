using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly Contexts _context;
        private readonly IMapper _mapper;

        public BankAccountsController(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccount()
        {
            var bankList = _context.BankAccount.ToList();

            return Ok(_mapper.Map<IEnumerable<BankAccountDTO>>(bankList));
        }

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
        {
            var bankAccount = await _context.BankAccount.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return bankAccount;
        }
        

        // PUT: api/BankAccounts/5  for Add sold
        [HttpPut("add")]
        public IActionResult PutBankAccount()
        {
            string body = this.InputBodyData;
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var bankAccount = _context.BankAccount.Find(id);
            bankAccount.Sold += amount;
            _context.SaveChanges();

            var bankList = _context.BankAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<BankAccountDTO>>(bankList));
        }

        // POST: api/BankAccounts
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
        {
            var bank = _context.Bank.Find(bankAccount.IdBank);
            var currency = _context.Currency.Where(b => b.CurrencyName == bankAccount.CurrencyName).FirstOrDefault();

            bankAccount.IdBank = bank.Id;
            bankAccount.IdCurrency = currency.Id;
            bankAccount.Sold = 0;
            _context.BankAccount.Add(bankAccount);
            _context.SaveChanges();
            var bankList = _context.BankAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<BankAccountDTO>>(bankList));
        }

        // DELETE: api/BankAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccount>> DeleteBankAccount(int id)
        {
            var bankAccount = await _context.BankAccount.FindAsync(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            _context.BankAccount.Remove(bankAccount);
            await _context.SaveChangesAsync();

            var bankList = _context.BankAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<BankAccountDTO>>(bankList));
        }

        private bool BankAccountExists(int id)
        {
            return _context.BankAccount.Any(e => e.Id == id);
        }
    }
}
