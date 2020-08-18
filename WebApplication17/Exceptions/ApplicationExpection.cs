using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoExchangeAPI.Exceptions
{
    public abstract class ApplicationExpection : Exception
    {
        public abstract class ApplicationException : Exception
        {
            public string Code { get; }

            protected ApplicationException()
            {
            }

            public ApplicationException(string code)
            {
                Code = code;
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
