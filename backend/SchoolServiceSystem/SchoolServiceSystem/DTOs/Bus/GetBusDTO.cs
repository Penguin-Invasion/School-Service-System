using SchoolServiceSystem.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Bus
{
    public class GetBusDTO
    {
        public int ID { get; set; }
        public GetUserDTO Driver { get; set; }
        public string Plaque { get; set; }
    }
}
