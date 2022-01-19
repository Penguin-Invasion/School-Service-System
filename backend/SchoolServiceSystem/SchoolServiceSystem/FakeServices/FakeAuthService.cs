using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.FakeServices
{
    public class FakeAuthService : IAuthService
    {
        private readonly IUserService _userService;
        public FakeAuthService(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<User> Login(User loginUser)
        {
            User user = await _userService.Find(loginUser.Email, loginUser.Password);
            return user;
        }
    }
}
