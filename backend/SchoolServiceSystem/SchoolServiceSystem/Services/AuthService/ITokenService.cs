using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services.AuthService
{
    public interface ITokenService
    {
        public string CreateToken(User user, List<Claim> claims);
    }
}
