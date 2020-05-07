﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataLayer.DTO;
using WebApplication17.Models;
using OpenQA.Selenium.Remote;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BanksManager _banksManager;

        public BanksController(IMapper mapper, BanksManager banksManager)
        {
            _mapper = mapper;
            _banksManager = banksManager;
        }

        // GET: api/Banks
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBank()
        {
            ResponseDTO<BankDTO> response = new ResponseDTO<BankDTO>();
            ListDTO<BankDTO> list = new ListDTO<BankDTO>();
            list = _banksManager.GetAllBanks();
            response.Data = new ListDTO<BankDTO>();
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

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            ResponseDTO<BankDTO> response = new ResponseDTO<BankDTO>();
            ListDTO<BankDTO> list = new ListDTO<BankDTO>();
            var bank = _banksManager.GetBankById(id);
            if (bank != null)
            {
                response.Data = null; //TODO de facut si aici sa prmeasca bank
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return BadRequest(response);
        }
        
        // POST: api/Banks
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            ResponseDTO<BankDTO> response = new ResponseDTO<BankDTO>();
            ListDTO<BankDTO> list = new ListDTO<BankDTO>();
            var bankList = _banksManager.AddBank(bank);

            if (bankList != null)
            {
                response.Data = bankList;
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return Ok(response);
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bank>> DeleteBank(int id)
        {
            ResponseDTO<BankDTO> response = new ResponseDTO<BankDTO>();
            ListDTO<BankDTO> list = new ListDTO<BankDTO>();
            var bankList = _banksManager.DeleteBank(id);
            if (bankList != null)
            {
                response.Data = bankList;
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return Ok(response);
        }

        
    }
}
