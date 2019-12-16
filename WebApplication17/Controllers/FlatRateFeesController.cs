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
        public async Task<ActionResult<FlatRateFee>> GetFlatRateFee()
        {
            double flat = _feesManager.GetAllFlatRateFees();
           

            return Ok(_mapper.Map<IEnumerable<FlatRateFee>>(flat));
        }

        // POST: api/FlatRateFees
        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<ActionResult<FlatRateFee>> PostFlatRateFee(FlatRateFee flatRateFee)
        {
            var flatRate = _feesManager.AddFlatRateFee(flatRateFee);
            return Ok(flatRate);
        }

        // DELETE: api/FlatRateFees/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlatRateFee>> DeleteFlatRateFee(int id)
        {
            _feesManager.DeleteFlatRateFee(id);
            return Ok();
        }
    }
}
