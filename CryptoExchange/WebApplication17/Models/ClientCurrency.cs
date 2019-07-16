using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class ClientCurrency
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdCurrency { get; set; }
    }
}
