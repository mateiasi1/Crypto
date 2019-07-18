﻿using System;
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
public class BanksController : ControllerBase
{
private readonly Contexts _context;

public BanksController(Contexts context)
{
_context = context;
}

// GET: api/Banks
[HttpGet]
public async Task<ActionResult<IEnumerable<Bank>>> GetBank()
{
return await _context.Bank.ToListAsync();
}

// GET: api/Banks/5
[HttpGet("{id}")]
public async Task<ActionResult<Bank>> GetBank(int id)
{
var bank = await _context.Bank.FindAsync(id);

if (bank == null)
{
return NotFound();
}

return bank;
}

// PUT: api/Banks/5
[HttpPut("{id}")]
public async Task<IActionResult> PutBank(int id, Bank bank)
{
if (id != bank.Id)
{
return BadRequest();
}

_context.Entry(bank).State = EntityState.Modified;

try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!BankExists(id))
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

// POST: api/Banks
[HttpPost]
public async Task<ActionResult<Bank>> PostBank(Bank bank)
{
_context.Bank.Add(bank);
await _context.SaveChangesAsync();

return CreatedAtAction("GetBank", new { id = bank.Id }, bank);
}

// DELETE: api/Banks/5
[HttpDelete("{id}")]
public async Task<ActionResult<Bank>> DeleteBank(int id)
{
var bank = await _context.Bank.FindAsync(id);
if (bank == null)
{
return NotFound();
}

_context.Bank.Remove(bank);
await _context.SaveChangesAsync();

return bank;
}

private bool BankExists(int id)
{
return _context.Bank.Any(e => e.Id == id);
}
}
}
