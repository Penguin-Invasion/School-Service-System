using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Exceptions
{
    public class SSSException : Exception
    {
        public SSSException(string message = null) : base(message)
        {

        }
    }
}
