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
    public class CryptoAccountController : CustomBaseController
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        
        public CryptoAccountController(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/CryptoAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoAccount>>> GetCryptoAccount()
        {
            var cryptoList = _context.CryptoAccount.ToList();

            return Ok(_mapper.Map<IEnumerable<CryptoAccountDTO>>(cryptoList));
        }

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoAccount>> GetCryptoAccount(int id)
        {
            var cryptoAccount = await _context.CryptoAccount.FindAsync(id);

            if (cryptoAccount == null)
            {
                return NotFound();
            }

            return cryptoAccount;
        }


        // PUT: api/AddCryptoAccount/add  for Add sold
        [HttpPut("add")]
        public IActionResult AddCryptoAccount()
        {
            CryptoAccountTransaction cryptoAccountTransaction = new CryptoAccountTransaction();

            string body = this.InputBodyData;
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold += amount;
            _context.SaveChanges();

            cryptoAccountTransaction.Ammount = amount;
            cryptoAccountTransaction.IdCryptoAccount = id;
            cryptoAccountTransaction.IdFee = 0;
            cryptoAccountTransaction.Status = "Done";
            _context.CryptoAccountTransaction.Add(cryptoAccountTransaction);
            _context.SaveChanges();

            var cryptoList = _context.CryptoAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<CryptoAccountDTO>>(cryptoList));
        }

        // PUT: api/WithdrawCryptoAccount/withdraw for withdraw sold
        [HttpPut("withdraw")]
        public IActionResult WithdrawCryptoAccount()
        {
            string body = this.InputBodyData;
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold -= amount;
            _context.SaveChanges();

            var cryptoList = _context.CryptoAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<CryptoAccountDTO>>(cryptoList));
        }

        // POST: api/PostCryptoAccount
        [HttpPost]
        public async Task<ActionResult<CryptoAccount>> PostCryptoAccount(CryptoAccount cryptoAccount)
        {
            var crypto = _context.Crypto.Find(cryptoAccount.IdCrypto);
            var cryptoCurrency = _context.Crypto.Where(b => b.CryptoCurrencyName == cryptoAccount.CryptoCurrencyName).FirstOrDefault();
            var bankAccount = _context.Bank.Where(b => b.Id == cryptoAccount.IdBankAccount).FirstOrDefault();

            cryptoAccount.Refference = bankAccount.Id + bankAccount.BankName + cryptoAccount.IdUser;
            cryptoAccount.IdCrypto = crypto.Id;
            cryptoAccount.IdCryptoCurrency = cryptoCurrency.Id;
            cryptoAccount.Sold = 0;
            _context.CryptoAccount.Add(cryptoAccount);
            _context.SaveChanges();
            var cryptoList = _context.CryptoAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<CryptoAccountDTO>>(cryptoList));
        }

        // DELETE: api/DeleteCryptoAccount/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CryptoAccount>> DeleteCryptoAccount(int id)
        {
            var cryptoAccount = await _context.CryptoAccount.FindAsync(id);
            if (cryptoAccount == null)
            {
                return NotFound();
            }

            _context.CryptoAccount.Remove(cryptoAccount);
            await _context.SaveChangesAsync();

            var cryptoList = _context.CryptoAccount.ToList();
            return Ok(_mapper.Map<IEnumerable<CryptoAccountDTO>>(cryptoList));
        }

        private bool CryptoAccountExists(int id)
        {
            return _context.CryptoAccount.Any(e => e.Id == id);
        }
    }
}
