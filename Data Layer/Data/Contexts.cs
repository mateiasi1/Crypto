using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication17.Models;

namespace WebApplication17.Data
{
public class Contexts : DbContext
{
public Contexts(DbContextOptions<Contexts> options) : base(options)
{

}

public DbSet<User> User { get; set; }
public DbSet<RegisterUser> RegisterUser { get; set; }
public DbSet<WebApplication17.Models.Bank> Bank { get; set; }
public DbSet<WebApplication17.Models.BankAccount> BankAccount { get; set; }
public DbSet<WebApplication17.Models.BankAccountTransaction> BankAccountTransaction { get; set; }
public DbSet<WebApplication17.Models.Conversion> Conversion { get; set; }
public DbSet<WebApplication17.Models.ConversionTransaction> ConversionTransaction { get; set; }
public DbSet<WebApplication17.Models.Currency> Currency { get; set; }
public DbSet<WebApplication17.Models.Fee> Fee { get; set; }
public DbSet<WebApplication17.Models.FlatRateFee> FlatRateFee { get; set; }
public DbSet<WebApplication17.Models.Wallet> Wallet { get; set; }
public DbSet<WebApplication17.Models.Login> Login { get; set; }
public DbSet<WebApplication17.Models.Token> Token { get; set; }
public DbSet<WebApplication17.Models.Crypto> Crypto { get; set; }
public DbSet<WebApplication17.Models.CryptoAccount> CryptoAccount { get; set; }
public DbSet<WebApplication17.Models.CryptoCurrency>CryptoCurrency { get; set; }
public DbSet<WebApplication17.Models.CryptoAccountTransaction> CryptoAccountTransaction { get; set; }
    }
}
