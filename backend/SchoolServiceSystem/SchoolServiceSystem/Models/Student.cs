using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Year { get; set; }
        public int ServiceID { get; set; }
        public Service Service { get; set; }

    }
}
