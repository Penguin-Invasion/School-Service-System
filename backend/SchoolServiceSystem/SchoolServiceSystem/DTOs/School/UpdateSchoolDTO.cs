using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.School
{
    public class UpdateSchoolDTO
    {
        public string Name { get; set; }
        public int UserID { get; set; }
        public string SecretKey { get; set; }
    }
}
