using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
public class Wallet
{
public int Id { get; set; }
public int IdClientCurrency { get; set; }
public double Sold { get; set; }
public int IdBank { get; set; }
}
}
