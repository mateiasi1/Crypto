using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
   public class FeeDTO : BaseDTO
    {
        public double Percentage { get; set; }
        public bool Obsolete { get; set; }

    }
}
