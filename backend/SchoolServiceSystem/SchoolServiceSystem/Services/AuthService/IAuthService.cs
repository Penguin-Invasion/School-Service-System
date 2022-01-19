using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services.AuthService
{
    public interface IAuthService
    {
        public Task<User> Login(User loginUser);
    }
}
