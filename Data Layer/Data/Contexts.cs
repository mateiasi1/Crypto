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
public DbSet<Bank> Bank { get; set; }
public DbSet<BankAccount> BankAccount { get; set; }
public DbSet<BankAccountTransaction> BankAccountTransaction { get; set; }
public DbSet<Conversion> Conversion { get; set; }
public DbSet<ConversionTransaction> ConversionTransaction { get; set; }
public DbSet<Currency> Currency { get; set; }
public DbSet<Fee> Fee { get; set; }
public DbSet<FlatRateFee> FlatRateFee { get; set; }
public DbSet<Wallet> Wallet { get; set; }
public DbSet<Login> Login { get; set; }
public DbSet<Token> Token { get; set; }
public DbSet<Crypto> Crypto { get; set; }
public DbSet<CryptoAccount> CryptoAccount { get; set; }
public DbSet<CryptoCurrency> CryptoCurrency { get; set; }
public DbSet<CryptoAccountTransaction> CryptoAccountTransaction { get; set; }
    }
}
