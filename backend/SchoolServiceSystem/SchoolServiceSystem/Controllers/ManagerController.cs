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
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public ManagerController(IMapper mapper, UserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateDriver")]
        public async Task<ServiceResponse<GetUserDTO>> Add(CreateUserDTO createUserDTO)
        {
            var createUser = _mapper.Map<User>(createUserDTO);
            createUser.Role = Utils.Roles.Driver;
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
        [Route("DeleteDriver")]
        public async Task<ServiceResponse<object>> DeleteManager(int DriverID)
        {

            var data = await _userService.DeleteDriver(DriverID);
            return new ServiceResponse<object>()
            {
                Success = data
            };
        }
    }
}
