using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.School
{
    public class CreateSchoolDTO
    {
        public string Name { get; set; }
        public string UserID { get; set; }
    }
}
