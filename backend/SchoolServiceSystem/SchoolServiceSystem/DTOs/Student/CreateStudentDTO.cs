using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Student
{
    public class CreateStudentDTO
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Year { get; set; }
    }
}
