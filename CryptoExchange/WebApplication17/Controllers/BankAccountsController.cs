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
public class BankAccountsController : ControllerBase
{
private readonly Contexts _context;

public BankAccountsController(Contexts context)
{
_context = context;
}

// GET: api/BankAccounts
[HttpGet]
public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccount()
{
return await _context.BankAccount.ToListAsync();
}

// GET: api/BankAccounts/5
[HttpGet("{id}")]
public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
{
var bankAccount = await _context.BankAccount.FindAsync(id);

if (bankAccount == null)
{
return NotFound();
}

return bankAccount;
}

// PUT: api/BankAccounts/5
[HttpPut("{id}")]
public async Task<IActionResult> PutBankAccount(int id, BankAccount bankAccount)
{
if (id != bankAccount.Id)
{
return BadRequest();
}

_context.Entry(bankAccount).State = EntityState.Modified;

try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!BankAccountExists(id))
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

// POST: api/BankAccounts
[HttpPost]
public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
{
_context.BankAccount.Add(bankAccount);
await _context.SaveChangesAsync();

return CreatedAtAction("GetBankAccount", new { id = bankAccount.Id }, bankAccount);
}

// DELETE: api/BankAccounts/5
[HttpDelete("{id}")]
public async Task<ActionResult<BankAccount>> DeleteBankAccount(int id)
{
var bankAccount = await _context.BankAccount.FindAsync(id);
if (bankAccount == null)
{
return NotFound();
}

_context.BankAccount.Remove(bankAccount);
await _context.SaveChangesAsync();

return bankAccount;
}

private bool BankAccountExists(int id)
{
return _context.BankAccount.Any(e => e.Id == id);
}
}
}
