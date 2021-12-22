using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.Auth;
using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Services.AuthService;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;
        private readonly AuthService _authService;
        private readonly UserService _userService;
        public AuthController(IMapper mapper, AuthService authService, TokenService tokenService, UserService userService)
        {
            _mapper = mapper;
            _authService = authService;
            _tokenService = tokenService;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Driver")]
        public async Task<GetUserDTO> GetMyInfo()
        {
            var user = await _userService.GetMyInfo();
            var result = _mapper.Map<GetUserDTO>(user);
            return result;
        }

        [HttpPost]
        public async Task<GetUserWithTokenDTO> Login(LoginDTO loginDTO)
        {
            var loginUser = _mapper.Map<User>(loginDTO);
            var user = await _authService.Login(loginUser);
            var result = _mapper.Map<GetUserWithTokenDTO>(user);

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                new Claim(ClaimTypes.Role, Roles.GetName(user.Role))
            };

            result.Token = _tokenService.CreateToken(user, claims);

            return result;
        }
    }
}
