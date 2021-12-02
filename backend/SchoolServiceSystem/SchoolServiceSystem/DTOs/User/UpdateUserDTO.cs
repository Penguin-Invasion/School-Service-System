using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.User
{
    public class UpdateUserDTO
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public string SurName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        //Admin,Manager,Driver
        public int Role { get; set; }
    }
}
