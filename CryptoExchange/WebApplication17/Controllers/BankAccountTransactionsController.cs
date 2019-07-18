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
public class BankAccountTransactionsController : ControllerBase
{
private readonly Contexts _context;

public BankAccountTransactionsController(Contexts context)
{
_context = context;
}

// GET: api/BankAccountTransactions
[HttpGet]
public async Task<ActionResult<IEnumerable<BankAccountTransaction>>> GetBankAccountTransaction()
{
return await _context.BankAccountTransaction.ToListAsync();
}

// GET: api/BankAccountTransactions/5
[HttpGet("{id}")]
public async Task<ActionResult<BankAccountTransaction>> GetBankAccountTransaction(int id)
{
var bankAccountTransaction = await _context.BankAccountTransaction.FindAsync(id);

if (bankAccountTransaction == null)
{
return NotFound();
}

return bankAccountTransaction;
}

// PUT: api/BankAccountTransactions/5
[HttpPut("{id}")]
public async Task<IActionResult> PutBankAccountTransaction(int id, BankAccountTransaction bankAccountTransaction)
{
if (id != bankAccountTransaction.Id)
{
return BadRequest();
}

_context.Entry(bankAccountTransaction).State = EntityState.Modified;

try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!BankAccountTransactionExists(id))
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

// POST: api/BankAccountTransactions
[HttpPost]
public async Task<ActionResult<BankAccountTransaction>> PostBankAccountTransaction(BankAccountTransaction bankAccountTransaction)
{
_context.BankAccountTransaction.Add(bankAccountTransaction);
await _context.SaveChangesAsync();

return CreatedAtAction("GetBankAccountTransaction", new { id = bankAccountTransaction.Id }, bankAccountTransaction);
}

// DELETE: api/BankAccountTransactions/5
[HttpDelete("{id}")]
public async Task<ActionResult<BankAccountTransaction>> DeleteBankAccountTransaction(int id)
{
var bankAccountTransaction = await _context.BankAccountTransaction.FindAsync(id);
if (bankAccountTransaction == null)
{
return NotFound();
}

_context.BankAccountTransaction.Remove(bankAccountTransaction);
await _context.SaveChangesAsync();

return bankAccountTransaction;
}

private bool BankAccountTransactionExists(int id)
{
return _context.BankAccountTransaction.Any(e => e.Id == id);
}
}
}
