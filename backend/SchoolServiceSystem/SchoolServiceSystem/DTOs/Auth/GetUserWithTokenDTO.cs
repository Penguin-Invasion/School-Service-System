using SchoolServiceSystem.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.DTOs.Auth
{
    public class GetUserWithTokenDTO : GetUserDTO
    {
        public string Token;
    }
}
