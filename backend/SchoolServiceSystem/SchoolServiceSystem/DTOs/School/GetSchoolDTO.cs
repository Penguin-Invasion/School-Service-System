using SchoolServiceSystem.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.School
{
    public class GetSchoolDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public List<GetServiceDTO> Services { get; set; }
    }
}
