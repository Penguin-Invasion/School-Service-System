using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int BusID { get; set; }
        public int SchoolID { get; set; }
        public Bus Bus { get; set; }
        public School School { get; set; }
        public List<Student> Students { get; set; }
    }
}
