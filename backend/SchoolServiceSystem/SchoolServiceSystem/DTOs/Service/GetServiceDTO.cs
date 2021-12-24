using SchoolServiceSystem.DTOs.Bus;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Service
{
    public class GetServiceDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public GetBusDTO Bus { get; set; }
        public GetSchoolDTO School { get; set; }
        public List<GetStudentDTO> Students { get; set; }
    }
}
