using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Bus
{
    public class CreateBusDTO
    {
        public int DriverID { get; set; }
        public string Plaque { get; set; }
    }
}
