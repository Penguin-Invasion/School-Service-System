using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Models
{
    public class School
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<Service> Services { get; set; }

    }
}
