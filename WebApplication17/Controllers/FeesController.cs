using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
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
    public class FeesController : ControllerBase
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        private readonly FeesManager _feesManager;

        public FeesController(Contexts context, IMapper mapper, FeesManager feesManager)
        {
            _mapper = mapper;
            _context = context;
            _feesManager = feesManager;
        }

        // GET: api/Fees
        [HttpGet]
        public async Task<ActionResult<Fee>> GetFee()
        {
            var feeList = _feesManager.GetAllFees();
            return Ok(_mapper.Map<IEnumerable<FeeDTO>>(feeList));
        }

        // POST: api/Fees
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Fee>> PostFee(Fee fee)
        {
            var feeList = _feesManager.AddFee(fee);
            return Ok(_mapper.Map<IEnumerable<FeeDTO>>(feeList));
        }

        // DELETE: api/Fees/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fee>> DeleteFee(int id)
        {
            var feeList = _feesManager.DeleteFee(id);
            return Ok(_mapper.Map<IEnumerable<FeeDTO>>(feeList));
        }
    }
}

