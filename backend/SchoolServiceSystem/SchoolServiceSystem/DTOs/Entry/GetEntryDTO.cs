using SchoolServiceSystem.DTOs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Entry
{
    public class GetEntryDTO
    {
        public int ID { get; set; }

        public string Time { get; set; }
        public string Date { get; set; }
    }
}
