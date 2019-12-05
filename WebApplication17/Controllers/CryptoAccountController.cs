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
    public class CryptoAccountController : CustomBaseController
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        private readonly CryptoManager _cryptoManager;

        public CryptoAccountController(Contexts context, IMapper mapper, CryptoManager cryptoManager)
        {
            _mapper = mapper;
            _context = context;
            _cryptoManager = cryptoManager;
        }

        // GET: api/CryptoAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoAccount>>> GetCryptoAccount()
        {
            var cryptoList = _cryptoManager.GetAllCryptoAccounts();

            return Ok(_mapper.Map<IEnumerable<CryptoAccountDTO>>(cryptoList));
        }

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoAccount>> GetCryptoAccount(int id)
        {
            var cryptoAccount = _cryptoManager.GetCryptoAccountById(id);

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
            _cryptoManager.AddToCryptoAccount(body);
            return Ok();
        }

        // PUT: api/WithdrawCryptoAccount/withdraw for withdraw sold
        [HttpPut("withdraw")]
        public IActionResult WithdrawCryptoAccount()
        {
            string body = this.InputBodyData;
            _cryptoManager.WithdrawFromCryptoAccount(body);
            return Ok();
        }

        // POST: api/PostCryptoAccount
        [HttpPost]
        public async Task<ActionResult<CryptoAccount>> PostCryptoAccount(CryptoAccount cryptoAccount)
        {
            _cryptoManager.AddCryptoAccount(cryptoAccount);
            return Ok();
        }

        // DELETE: api/DeleteCryptoAccount/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CryptoAccount>> DeleteCryptoAccount(int id)
        {
            _cryptoManager.DeleteCryptoAccount(id);
            return Ok();
        }

        
    }
}
