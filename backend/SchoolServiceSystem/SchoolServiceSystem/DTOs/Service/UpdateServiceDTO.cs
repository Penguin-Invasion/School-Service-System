using SchoolServiceSystem.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Service
{
    public class UpdateServiceDTO
    {
        public int BusID { get; set; }
        public int SchoolID { get; set; }
        public List<UpdateStudentDTO> Students { get; set; }

    }
}
