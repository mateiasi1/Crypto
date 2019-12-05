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
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatRateFeesController : ControllerBase
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        private readonly FeesManager _feesManager;
        public FlatRateFeesController(Contexts context, IMapper mapper, FeesManager feesManager)
        {
            _context = context;
            _mapper = mapper;
            _feesManager = feesManager;

        }

        // GET: api/FlatRateFees
        [HttpGet]
        public async Task<ActionResult<double>> GetFlatRateFee()
        {
            double flat = _context.FlatRateFee.Where(item => item.Obsolete == false).Select(item => item.Ammount).FirstOrDefault();
           

            return Ok(_mapper.Map<IEnumerable<FlatRateFee>>(flat));
        }

        // POST: api/FlatRateFees
        [HttpPost]
        public async Task<ActionResult<FlatRateFee>> PostFlatRateFee(FlatRateFee flatRateFee)
        {
            var flatRate = _feesManager.AddFlatRateFee(flatRateFee);
            return Ok(flatRate);
        }

        // DELETE: api/FlatRateFees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FlatRateFee>> DeleteFlatRateFee(int id)
        {
            var flatRateFee = await _context.FlatRateFee.FindAsync(id);
            if (flatRateFee == null)
            {
                return NotFound();
            }

            _context.FlatRateFee.Remove(flatRateFee);
            await _context.SaveChangesAsync();

            return flatRateFee;
        }

        private bool FlatRateFeeExists(int id)
        {
            return _context.FlatRateFee.Any(e => e.Id == id);
        }
    }
}
