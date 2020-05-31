using System;
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

using DataLayer.DTO;
using WebApplication17.Models;
using BusinessLayer.DTO;
using DataLayer.Models;

namespace DataLayer.Controllers
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

            ResponseDTO<CryptoAccountDTO> response = new ResponseDTO<CryptoAccountDTO>();
            ListDTO<CryptoAccountDTO> list = new ListDTO<CryptoAccountDTO>();

            list = _cryptoManager.GetAllCryptoAccounts();
            response.Data = new ListDTO<CryptoAccountDTO>();
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

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoAccount>> GetCryptoAccount(int id)
        {

            ResponseDTO<CryptoAccountDTO> response = new ResponseDTO<CryptoAccountDTO>();
            ListDTO<CryptoAccountDTO> list = new ListDTO<CryptoAccountDTO>();
            list = _cryptoManager.GetCryptoAccountById(id);
            response.Data = new ListDTO<CryptoAccountDTO>();
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


        // PUT: api/AddCryptoAccount/add  for Add sold
        [HttpPut("add")]
        public IActionResult AddCryptoAccount([FromBody]Deposit deposit)
        {
            CryptoAccountTransaction cryptoAccountTransaction = new CryptoAccountTransaction();
            ResponseDTO<CryptoAccountDTO> response = new ResponseDTO<CryptoAccountDTO>();
            if (deposit.Amount <= 0)
            {
                response.Data = null;
                response.Message = "The amount should be greater than 0!";
                response.Success = false;
                return Ok(response);
            }

            _cryptoManager.AddToCryptoAccount(deposit.Id, deposit.Amount);
            return Ok();
        }

        // PUT: api/WithdrawCryptoAccount/withdraw for withdraw sold
        [HttpPut("withdraw")]
        public IActionResult WithdrawCryptoAccount([FromBody]Deposit deposit)
        {
            ResponseDTO<CryptoAccountDTO> response = new ResponseDTO<CryptoAccountDTO>();
            if (deposit.Amount <= 0)
            {
                response.Data = null;
                response.Message = "The amount should be greater than 0!";
                response.Success = false;
                return Ok(response);
            }
            _cryptoManager.WithdrawFromCryptoAccount(deposit.Id, deposit.Amount);
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
