using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class FlatRateFeeDTO : BaseDTO
    {
        public double Ammount { get; set; }
        public bool Obsolete { get; set; }
    }
}
