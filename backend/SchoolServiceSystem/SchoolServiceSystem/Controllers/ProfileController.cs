using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public ProfileController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPatch]
        [Authorize(Roles = "Admin,Manager,Driver")]
        public async Task<ServiceResponse<GetUserDTO>> Update(UpdateUserDTO updateUserDTO)
        {
            var ID = _userService.GetCurrentUserId();
            User user = await _userService.Update(ID, updateUserDTO);
            GetUserDTO data = _mapper.Map<GetUserDTO>(user);
            ServiceResponse<GetUserDTO> response = new ServiceResponse<GetUserDTO>()
            {
                Data = data,
                Success = true

            };

            return response;
        }
    }
}
