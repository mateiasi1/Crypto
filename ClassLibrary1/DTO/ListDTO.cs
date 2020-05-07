using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTO
{
    public class ListDTO<T>
        where T: BaseDTO
    {
        public  List<T> Items { get; set; }
    }
}
