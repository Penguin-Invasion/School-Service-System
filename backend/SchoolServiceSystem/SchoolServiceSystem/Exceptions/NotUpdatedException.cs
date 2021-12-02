using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Exceptions
{
    public class NotUpdatedException : SSSException
    {
        public NotUpdatedException(string message = null) : base(message)
        {

        }
    }
}
