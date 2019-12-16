﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;
        private readonly CryptoManager _cryptoManager;

        public CryptoAccountController(IMapper mapper, CryptoManager cryptoManager)
        {
            _mapper = mapper;
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
        [Authorize]
        [HttpPut("add")]
        public IActionResult AddCryptoAccount()
        {
            CryptoAccountTransaction cryptoAccountTransaction = new CryptoAccountTransaction();

            string body = this.InputBodyData;
            _cryptoManager.AddToCryptoAccount(body);
            return Ok();
        }

        // PUT: api/WithdrawCryptoAccount/withdraw for withdraw sold
        [Authorize]
        [HttpPut("withdraw")]
        public IActionResult WithdrawCryptoAccount()
        {
            string body = this.InputBodyData;
            _cryptoManager.WithdrawFromCryptoAccount(body);
            return Ok();
        }

        // POST: api/PostCryptoAccount
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CryptoAccount>> PostCryptoAccount(CryptoAccount cryptoAccount)
        {
            _cryptoManager.AddCryptoAccount(cryptoAccount);
            return Ok();
        }

        // DELETE: api/DeleteCryptoAccount/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CryptoAccount>> DeleteCryptoAccount(int id)
        {
            _cryptoManager.DeleteCryptoAccount(id);
            return Ok();
        }

        
    }
}
