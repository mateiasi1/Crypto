using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class FeesController : ControllerBase
{
private readonly Contexts _context;

public FeesController(Contexts context)
{
_context = context;
}

// GET: api/Fees
[HttpGet]
public async Task<ActionResult<IEnumerable<Fee>>> GetFee()
{
return await _context.Fee.ToListAsync();
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
_context.Fee.Add(fee);
await _context.SaveChangesAsync();

return CreatedAtAction("GetFee", new { id = fee.Id }, fee);
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
