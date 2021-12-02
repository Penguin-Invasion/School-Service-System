using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Exceptions
{
    public class NotFoundException : SSSException
    {
        public NotFoundException(string message = null) : base(message)
        {

        }
    }
}
