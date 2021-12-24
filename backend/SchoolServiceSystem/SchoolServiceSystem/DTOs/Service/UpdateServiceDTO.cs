using SchoolServiceSystem.DTOs.Bus;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Service
{
    public class UpdateServiceDTO
    {
        public string Name { get; set; }
        public int? BusID { get; set; }
        public int? SchoolID { get; set; }

    }
}
