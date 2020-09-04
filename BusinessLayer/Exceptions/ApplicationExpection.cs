using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions
{
    public  class ApplicationExpection : Exception
    {
        
        public  class ApplicationException : Exception
        {
            public string Code { get; }

            protected ApplicationException()
            {
            }

            public ApplicationException(string code)
            {
                Code = code;
            }
            public ApplicationException(Exception ex)
            {
            }

            public ApplicationException(string message, params object[] args)
                : this(string.Empty, message, args)
            {
            }

            public ApplicationException(string code, string message, params object[] args)
                : this(null, code, message, args)
            {
            }

            public ApplicationException(Exception innerException, string message, params object[] args)
                : this(innerException, string.Empty, message, args)
            {
            }

            public ApplicationException(Exception innerException, string code, string message, params object[] args)
                : base(string.Format(message, args), innerException)
            {
                Code = code;
            }
        }
    }
}
