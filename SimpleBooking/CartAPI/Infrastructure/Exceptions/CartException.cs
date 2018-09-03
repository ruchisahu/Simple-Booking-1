using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcClient.Infrastructure
{
    public class CartException : Exception  
    {
        public CartException()
        {}

        public CartException(string message) : base(message)
        {}

        public CartException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
