using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.Filters;
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
    public class SchoolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISchoolService _schoolService;
        private readonly IUserService _userService;

        public SchoolController(IMapper mapper, ISchoolService schoolService, IUserService userService)
        {
            _mapper = mapper;
            _schoolService = schoolService;
            _userService = userService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ServiceResponse<IEnumerable<GetSchoolDTO>>> GetAll()
        {
            IEnumerable<School> data = null;
            if (_userService.GetCurrentUserRole() == Roles.Admin)
            {
                data = await _schoolService.GetAll();
            }
            else if (_userService.GetCurrentUserRole() == Roles.Manager)
            {
                var userID = _userService.GetCurrentUserId();
                data = await _schoolService.FindManagerSchools(userID, true, true);
            }

            var response = _mapper.Map<IEnumerable<GetSchoolDTO>>(data);
            var result = new ServiceResponse<IEnumerable<GetSchoolDTO>>()
            {
                Data = response.ToList(),
                Success = true
            };
            return result;
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<ServiceResponse<GetSchoolDTO>> Get(int ID)
        {
            var data = await _schoolService.Get(ID);
            var response = _mapper.Map<GetSchoolDTO>(data);
            var result = new ServiceResponse<GetSchoolDTO>()
            {
                Data = response,
                Success = true
            };
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResponse<GetSchoolDTO>> Add(CreateSchoolDTO createSchoolDTO)
        {
            School createSchool = _mapper.Map<School>(createSchoolDTO);
            School school = await _schoolService.Create(createSchool);
            GetSchoolDTO getSchoolDTO = _mapper.Map<GetSchoolDTO>(school);
            var result = new ServiceResponse<GetSchoolDTO>()
            {
                Data = getSchoolDTO,
                Success = true
            };
            return result;
        }

        [HttpDelete]
        [Route("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ServiceResponse<Object>> Delete(int ID)
        {
            bool result = await _schoolService.Delete(ID);
            var response = new ServiceResponse<Object>() { Success = result };
            return response;
        }

        [HttpPatch]
        [Route("{ID}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ServiceResponse<GetSchoolDTO>>> Update(int ID, UpdateSchoolDTO updateSchoolDTO)
        {
            School school = await _schoolService.Update(ID, updateSchoolDTO);
            GetSchoolDTO data = _mapper.Map<GetSchoolDTO>(school);
            ServiceResponse<GetSchoolDTO> response = new ServiceResponse<GetSchoolDTO>()
            {
                Data = data,
                Success = true

            };

            return response;
        }

    }
}
