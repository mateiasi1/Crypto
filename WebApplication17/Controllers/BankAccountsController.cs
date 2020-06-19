using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using DataLayer.DTO;
using System;
using WebApplication17.Models;
using DataLayer.Models;

namespace DataLayer.Controllers
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
            ResponseDTO<BankAccountDTO> response = new ResponseDTO<BankAccountDTO>();
            ListDTO<BankAccountDTO> list = new ListDTO<BankAccountDTO>();

            list = _banksManager.GetAllBankAccounts();
            response.Data = new ListDTO<BankAccountDTO>();
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

            return BadRequest(response);
        }

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
        {
            ResponseDTO<BankAccountDTO> response = new ResponseDTO<BankAccountDTO>();
            ListDTO<BankAccountDTO> list = new ListDTO<BankAccountDTO>();

            list = _banksManager.GetBankAccountById(id);

            if (list != null)
            {
                response.Data = list; //TODO de facut si aici sa returneze
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return BadRequest(response);
        }
        

        // PUT: api/BankAccounts/add  for Add sold
        [HttpPut("add")]
        public IActionResult AddBankAccount([FromBody]Deposit deposit)

        {
            ResponseDTO<BankAccountDTO> response = new ResponseDTO<BankAccountDTO>();
            ListDTO<BankAccountDTO> list = new ListDTO<BankAccountDTO>();

            if (deposit.Amount <= 0)
            {
                response.Data = null;
                response.Message = "The amount should be greater than 0!";
                response.Success = false;
                return Ok(response);
            }
            

           list = _banksManager.AddToBankAccount(deposit.Id, deposit.Amount);
            response.Data = new ListDTO<BankAccountDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "Transaction successful! " + DateTime.Now;
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "Something went wrong! Please try again or contact an administrator!";
            response.Success = false;

            return Ok(response);
        }

        // PUT: api/BankAccounts/withdraw for withdraw sold
        [HttpPut("withdraw")]
        public IActionResult WithdrawBankAccount([FromBody]Deposit deposit)
        {
            ResponseDTO<BankAccountDTO> response = new ResponseDTO<BankAccountDTO>();
            ListDTO<BankAccountDTO> list = new ListDTO<BankAccountDTO>();

            if (deposit.Amount <= 0)
            {
                response.Data = null;
                response.Message = "The amount should be greater than 0!";
                response.Success = false;
                return Ok(response);
            }


            list = _banksManager.WithdrawFromBankAccount(deposit.Id, deposit.Amount);
            response.Data = new ListDTO<BankAccountDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "Transaction successful! " + DateTime.Now ;
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "Insufficient funds!";
            response.Success = false;

            return Ok(response);
        }

        // POST: api/BankAccounts
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
        {
            ResponseDTO<BankAccountDTO> response = new ResponseDTO<BankAccountDTO>();
            ListDTO<BankAccountDTO> list = new ListDTO<BankAccountDTO>();

            list = _banksManager.AddBankAccount(bankAccount);
            response.Data = new ListDTO<BankAccountDTO>();
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

            return BadRequest(response);
        }

        // DELETE: api/BankAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BankAccount>> DeleteBankAccount(int id)
        {
            ResponseDTO<BankAccountDTO> response = new ResponseDTO<BankAccountDTO>();
            ListDTO<BankAccountDTO> list = new ListDTO<BankAccountDTO>();

            list = _banksManager.DeleteBankAccount(id);
            response.Data = new ListDTO<BankAccountDTO>();
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

            return BadRequest(response);
        }

        
    }
}
