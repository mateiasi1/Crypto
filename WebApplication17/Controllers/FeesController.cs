using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public FeesController(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Fees
        [HttpGet]
        public async Task<ActionResult<double>> GetFee()
        {
            double fee = _context.Fee.Where(item => item.Obsolete == false).Select(item => item.Percentage).FirstOrDefault();
            return fee;
        }

        // GET: api/Fees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fee>> GetFee(int id)
        {
            var fee = await _context.Fee.FindAsync(id);

            if (fee == null)
            {
                return NotFound();
            }

            return fee;
        }

        // PUT: api/Fees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFee(int id, Fee fee)
        {
            if (id != fee.Id)
            {
                return BadRequest();
            }

            _context.Entry(fee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fees
        [HttpPost]
        public async Task<ActionResult<Fee>> PostFee(Fee fee)
        {
            Fee fees = _context.Fee.Where(item => item.Obsolete == false).FirstOrDefault();
            if (fees == null)
            {
                _context.Fee.Add(fee);
                _context.SaveChanges();
            }
            else
            {
                fees.Obsolete = true;
                _context.SaveChanges();

                _context.Fee.Add(fee);
                _context.SaveChanges();
            }
            var feeList = _context.Fee.ToList();
            return Ok(_mapper.Map<IEnumerable<FeeDTO>>(feeList));
        }

        // DELETE: api/Fees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fee>> DeleteFee(int id)
        {
            var fee = await _context.Fee.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }

            _context.Fee.Remove(fee);
            await _context.SaveChangesAsync();

            return fee;
        }

        private bool FeeExists(int id)
        {
            return _context.Fee.Any(e => e.Id == id);
        }
    }
}
