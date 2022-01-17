using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.School;
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
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public AdminController(IMapper mapper, UserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateManager")]
        public async Task<ServiceResponse<GetUserDTO>> Add(CreateUserDTO createUserDTO)
        {

            var createUser = _mapper.Map<User>(createUserDTO);
            createUser.Role = Utils.Roles.Manager;
            createUser = await _userService.Create(createUser);
            var data = _mapper.Map<GetUserDTO>(createUser);
            var result = new ServiceResponse<GetUserDTO>()
            {
                Data = data,
                Success = true
            };

            return result;
        }


        [HttpPost]
        [Route("DeleteManager")]
        public async Task<ServiceResponse<object>> DeleteManager(int ManagerID)
        {
            await _userService.DeleteManager(ManagerID);
            return new ServiceResponse<object>()
            {
                Success = true
            };
        }

    }
}
