﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class EmailModel
    {
        public int UserId { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
