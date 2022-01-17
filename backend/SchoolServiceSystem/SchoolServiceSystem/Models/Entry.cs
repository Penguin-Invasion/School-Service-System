using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Models
{
    public class Entry
    {

        public int ID { get; set; }

        public int ServiceID { get; set; }

        public DateTime Time { get; set; }

        public Service Service { get; set; }

    }
}
