using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Models
{
    public class Bus
    {
        public int ID { get; set; }
        //User bu has-one'dan bağlanacak.
        public int DriverID { get; set; }

        public User Driver { get; set; }

        public string Plaque { get; set; }
    }
}
