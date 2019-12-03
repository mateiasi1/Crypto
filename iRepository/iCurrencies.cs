using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface iCurrencies
    {
        List<Currency> GetAllCurrencies();
        Currency AddCurrency(Currency currency);
    }
}
