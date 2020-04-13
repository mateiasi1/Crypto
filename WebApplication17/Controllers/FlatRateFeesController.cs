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
    public class FlatRateFeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FeesManager _feesManager;
        public FlatRateFeesController(IMapper mapper, FeesManager feesManager)
        {
            _mapper = mapper;
            _feesManager = feesManager;

        }

        // GET: api/FlatRateFees
        [HttpGet]
        public double GetFlatRateFee()
        {
            double flat = _feesManager.GetAllFlatRateFees();


            return flat;
        }

        // POST: api/FlatRateFees
        [HttpPost]
        public async Task<ActionResult<FlatRateFee>> PostFlatRateFee(FlatRateFee flatRateFee)
        {
            if (flatRateFee.UserRole != "admin")
            {
                return BadRequest();
            }
            var flatRate = _feesManager.AddFlatRateFee(flatRateFee);
            return Ok(_mapper.Map<IEnumerable<FlatRateFeeDTO>>(flatRate));
        }

        // DELETE: api/FlatRateFees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlatRateFee>> DeleteFlatRateFee(int id)
        {
            _feesManager.DeleteFlatRateFee(id);
            return Ok();
        }
    }
}
