using AutoMapper;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        public AuthService(IUserService userService)
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
