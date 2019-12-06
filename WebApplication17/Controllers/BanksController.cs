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
    public class BanksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Contexts _context;
        private readonly BanksManager _banksManager;


        public BanksController(Contexts context, IMapper mapper, BanksManager banksManager)
        {
            _context = context;
            _mapper = mapper;
            _banksManager = banksManager;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBank()
        {
           
            _banksManager.GetAllBanks();
            return Ok(_mapper.Map<IEnumerable<BankDTO>>(_banksManager));
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = _banksManager.GetBankById(id);
            if (bank == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<BankDTO>>(_banksManager));
        }
        
        // POST: api/Banks
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            var bankList = _banksManager.AddBank(bank);
            return Ok(_mapper.Map<IEnumerable<BankDTO>>(bankList));
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bank>> DeleteBank(int id)
        {
            var bank = _banksManager.DeleteBank(id);
            return Ok(_mapper.Map<IEnumerable<BankDTO>>(bank));
        }

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.Id == id);
        }
    }
}
