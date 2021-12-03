using AutoMapper;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services.AuthService
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public AuthService(DataContext context, Mapper mapper, UserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }


        public async Task<User> Login(User loginUser)
        {
            User user = await _userService.Find(loginUser.Email, loginUser.Password);
            return user;
        }

    }
}
