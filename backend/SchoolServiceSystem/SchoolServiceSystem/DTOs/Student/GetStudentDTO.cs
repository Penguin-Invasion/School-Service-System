using SchoolServiceSystem.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Student
{
    public class GetStudentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Year { get; set; }
        //public GetServiceDTO Service { get; set; }
    }
}
