using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IBanks
    {

        List<Bank> GetAllBanks();
        List<Bank> AddBank(Bank bank);

        Bank GetBankById();

    }
}
