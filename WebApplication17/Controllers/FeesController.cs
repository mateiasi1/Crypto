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
        private readonly IMapper _mapper;
        private readonly FeesManager _feesManager;

        public FeesController(IMapper mapper, FeesManager feesManager)
        {
            _mapper = mapper;
            _feesManager = feesManager;
        }

        // GET: api/Fees
        [HttpGet]
        public double GetFee()
        {
            var fee = _feesManager.GetAllFees();
            return fee;
        }

        // POST: api/Fees
        [HttpPost]
        public async Task<ActionResult<Fee>> PostFee(Fee fee)
        {
            if( fee.UserRole != "admin")
            {
                return BadRequest();
            }
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

