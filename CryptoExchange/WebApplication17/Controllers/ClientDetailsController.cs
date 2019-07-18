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
public class ClientDetailsController : ControllerBase
{
private readonly Contexts _context;

public ClientDetailsController(Contexts context)
{
_context = context;
}

// GET: api/ClientDetails
[HttpGet]
public async Task<ActionResult<IEnumerable<ClientDetails>>> GetClientDetails()
{
return await _context.ClientDetails.ToListAsync();
}

// GET: api/ClientDetails/5
[HttpGet("{id}")]
public async Task<ActionResult<ClientDetails>> GetClientDetails(int id)
{
var clientDetails = await _context.ClientDetails.FindAsync(id);

if (clientDetails == null)
{
return NotFound();
}

return clientDetails;
}

// PUT: api/ClientDetails/5
[HttpPut("{id}")]
public async Task<IActionResult> PutClientDetails(int id, ClientDetails clientDetails)
{
if (id != clientDetails.Id)
{
return BadRequest();
}

_context.Entry(clientDetails).State = EntityState.Modified;

try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!ClientDetailsExists(id))
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

// POST: api/ClientDetails
[HttpPost]
public async Task<ActionResult<ClientDetails>> PostClientDetails(ClientDetails clientDetails)
{
_context.ClientDetails.Add(clientDetails);
await _context.SaveChangesAsync();

return CreatedAtAction("GetClientDetails", new { id = clientDetails.Id }, clientDetails);
}

// DELETE: api/ClientDetails/5
[HttpDelete("{id}")]
public async Task<ActionResult<ClientDetails>> DeleteClientDetails(int id)
{
var clientDetails = await _context.ClientDetails.FindAsync(id);
if (clientDetails == null)
{
return NotFound();
}

_context.ClientDetails.Remove(clientDetails);
await _context.SaveChangesAsync();

return clientDetails;
}

private bool ClientDetailsExists(int id)
{
return _context.ClientDetails.Any(e => e.Id == id);
}
}
}
