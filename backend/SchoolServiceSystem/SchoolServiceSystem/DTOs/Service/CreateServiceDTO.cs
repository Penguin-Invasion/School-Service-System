using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Service
{
    public class CreateServiceDTO
    {
        public int BusID { get; set; }
        public int SchoolID { get; set; }
        public List<int> Students { get; set; }
    }
}
