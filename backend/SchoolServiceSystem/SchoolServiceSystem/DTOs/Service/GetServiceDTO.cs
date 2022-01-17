using SchoolServiceSystem.DTOs.Entry;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.DTOs.Student;
using SchoolServiceSystem.DTOs.User;
using System.Collections.Generic;

namespace SchoolServiceSystem.DTOs.Service
{
    public class GetServiceDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Plaque { get; set; }
        public int SchoolID { get; set; }
        public GetUserDTO Driver { get; set; }
        public List<GetStudentDTO> Students { get; set; }
        public List<GetEntryDTO> Entries { get; set; }
    }
}
