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
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IMapper mapper, IAuthService authService, ITokenService tokenService, IUserService userService)
        {
            _mapper = mapper;
            _authService = authService;
            _tokenService = tokenService;
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Driver")]
        public async Task<ServiceResponse<GetUserDTO>> GetMyInfo()
        {
            var result = new ServiceResponse<GetUserDTO>();
            var user = await _userService.GetMyInfo();
            var data = _mapper.Map<GetUserDTO>(user);
            result.Data = data;
            result.Success = true;
            return result;
        }

        [HttpPost]
        public async Task<ServiceResponse<GetUserWithTokenDTO>> Login(LoginDTO loginDTO)
        {
            var result = new ServiceResponse<GetUserWithTokenDTO>();
            var loginUser = _mapper.Map<User>(loginDTO);
            var user = await _authService.Login(loginUser);
            var data = _mapper.Map<GetUserWithTokenDTO>(user);

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                new Claim(ClaimTypes.Role, Roles.GetName(user.Role))
            };

            data.Token = _tokenService.CreateToken(user, claims);
            result.Data = data;
            result.Success = true;
            return result;
        }
    }
}
