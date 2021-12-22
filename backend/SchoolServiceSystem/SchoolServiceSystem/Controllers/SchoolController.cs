using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.Filters;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services.ScoolService;
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
    public class SchoolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SchoolService _schoolService;

        public SchoolController(IMapper mapper, SchoolService schoolService)
        {
            _mapper = mapper;
            _schoolService = schoolService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetSchoolDTO>>> GetAll()
        {
            var data = await _schoolService.GetAll();
            var response = _mapper.Map<IEnumerable<GetSchoolDTO>>(data);
            return response.ToList();
        }

        [HttpGet]
        [Route("{ID}")]
        public async Task<ActionResult<GetSchoolDTO>> Get(int ID)
        {
            var data = await _schoolService.Get(ID);
            var response = _mapper.Map<GetSchoolDTO>(data);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<GetSchoolDTO>> Add(CreateSchoolDTO createSchoolDTO)
        {
            School createSchool = _mapper.Map<School>(createSchoolDTO);
            School school = await _schoolService.Create(createSchool);
            GetSchoolDTO getSchoolDTO = _mapper.Map<GetSchoolDTO>(school);
            return getSchoolDTO;
        }

        [HttpDelete]
        [Route("{ID}")]
        public async Task<ServiceResponse<Object>> Delete(int ID)
        {
            bool result = await _schoolService.Delete(ID);
            var response = new ServiceResponse<Object>() { Success = result };
            return response;
        }

        [HttpPatch]
        [Route("{ID}")]
        public async Task<ActionResult<ServiceResponse<GetSchoolDTO>>> Update(int ID, UpdateSchoolDTO updateSchoolDTO)
        {
            School school = _mapper.Map<School>(updateSchoolDTO);
            school = await _schoolService.Update(ID, school);
            GetSchoolDTO data = _mapper.Map<GetSchoolDTO>(school);
            ServiceResponse<GetSchoolDTO> response = new ServiceResponse<GetSchoolDTO>()
            {
                Data = data,
                Success = true

            };

            return response;
        }


        [HttpPost]
        [Route("{SchoolID}/add/managers/{ManagerID}")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddManagers(int SchoolID, int ManagerID)
        {
            bool data = await _schoolService.AddManager(SchoolID, ManagerID);
            var response = new ServiceResponse<bool>()
            {
                Data = data,
                Success = data
            };
            return response;
        }

        [HttpDelete]
        [Route("{SchoolID}/delete/managers/{ManagerID}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveManager(int SchoolID, int ManagerID)
        {
            bool data = await _schoolService.RemoveManager(SchoolID, ManagerID);
            var response = new ServiceResponse<bool>()
            {
                Data = data,
                Success = data
            };
            return response;
        }
    }
}
