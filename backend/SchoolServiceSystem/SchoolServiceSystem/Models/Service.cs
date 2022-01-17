using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Plaque { get; set; }
        public int DriverID { get; set; }
        public int SchoolID { get; set; }
        public User Driver { get; set; }
        public School School { get; set; }
        public List<Student> Students { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
