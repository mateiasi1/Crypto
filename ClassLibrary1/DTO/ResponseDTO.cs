using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTO
{
    public class ResponseDTO<T> where T: BaseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ListDTO<T> Data {get;set;}
    }
}
