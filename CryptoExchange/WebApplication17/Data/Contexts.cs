using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication17.Data.Registration;
using WebApplication17.Models;
using WebApplication17.Wallet;

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
public DbSet<WebApplication17.Models.ClientCurrency> ClientCurrency { get; set; }
public DbSet<WebApplication17.Models.ClientDetails> ClientDetails { get; set; }
public DbSet<WebApplication17.Models.Conversion> Conversion { get; set; }
public DbSet<WebApplication17.Models.ConversionTransaction> ConversionTransaction { get; set; }
public DbSet<WebApplication17.Models.Currency> Currency { get; set; }
public DbSet<WebApplication17.Models.Fee> Fee { get; set; }
public DbSet<WebApplication17.Models.FlatRateFee> FlatRateFee { get; set; }
public DbSet<WebApplication17.Wallet.Wallet> Wallet { get; set; }
}
}
