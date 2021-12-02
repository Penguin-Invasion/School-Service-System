using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Exceptions
{
    public class NotCreatedException : SSSException
    {
        public NotCreatedException(string message = null) : base(message)
        {

        }
    }
}
