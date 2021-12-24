using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Service
{
    public class CreateServiceDTO
    {
        public string Name { get; set; }
        public int BusID { get; set; }
        public int SchoolID { get; set; }
    }
}
