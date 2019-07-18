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
public class ConversionsController : ControllerBase
{
private readonly Contexts _context;

public ConversionsController(Contexts context)
{
_context = context;
}

// GET: api/Conversions
[HttpGet]
public async Task<ActionResult<IEnumerable<Conversion>>> GetConversion()
{
return await _context.Conversion.ToListAsync();
}

// GET: api/Conversions/5
[HttpGet("{id}")]
public async Task<ActionResult<Conversion>> GetConversion(int id)
{
var conversion = await _context.Conversion.FindAsync(id);

if (conversion == null)
{
return NotFound();
}

return conversion;
}

// PUT: api/Conversions/5
[HttpPut("{id}")]
public async Task<IActionResult> PutConversion(int id, Conversion conversion)
{
if (id != conversion.Id)
{
return BadRequest();
}

_context.Entry(conversion).State = EntityState.Modified;

try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!ConversionExists(id))
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

// POST: api/Conversions
[HttpPost]
public async Task<ActionResult<Conversion>> PostConversion(Conversion conversion)
{
_context.Conversion.Add(conversion);
await _context.SaveChangesAsync();

return CreatedAtAction("GetConversion", new { id = conversion.Id }, conversion);
}

// DELETE: api/Conversions/5
[HttpDelete("{id}")]
public async Task<ActionResult<Conversion>> DeleteConversion(int id)
{
var conversion = await _context.Conversion.FindAsync(id);
if (conversion == null)
{
return NotFound();
}

_context.Conversion.Remove(conversion);
await _context.SaveChangesAsync();

return conversion;
}

private bool ConversionExists(int id)
{
return _context.Conversion.Any(e => e.Id == id);
}
}
}
