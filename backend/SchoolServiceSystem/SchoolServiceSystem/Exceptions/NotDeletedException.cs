using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Exceptions
{
    public class NotDeletedException : SSSException
    {
        public NotDeletedException(string message = null) : base(message)
        {

        }
    }
}
