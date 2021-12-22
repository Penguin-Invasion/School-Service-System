using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.User
{
    public class GetUserDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
