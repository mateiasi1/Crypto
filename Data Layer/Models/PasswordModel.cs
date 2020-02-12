using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Models
{
    public class PasswordModel
    {
        public string PasswordSalt { get; set; }
        public string Passwordhash { get; set; }
    }
}
