using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.DTOs.Student;
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

    [Route("api/School/{SchoolID}/[controller]")]
    [Authorize(Roles = "Admin,Manager")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ServiceService _serviceService;
        private readonly UserService _userService;
        public ServiceController(IMapper mapper, ServiceService ServiceService, UserService userService)
        {
            _mapper = mapper;
            _serviceService = ServiceService;
            _userService = userService;

        }

        [HttpGet]
        [Route("{ServiceID}")]
        public async Task<ServiceResponse<GetServiceDTO>> Get(int SchoolID, int ServiceID)
        {
            await checkAuthManagerAsync(SchoolID);

            var data = await _serviceService.Get(ServiceID);
            var response = _mapper.Map<GetServiceDTO>(data);
            var result = new ServiceResponse<GetServiceDTO>()
            {
                Data = response,
                Success = true
            };
            return result;
        }

        [HttpPost]
        public async Task<ServiceResponse<GetServiceDTO>> Add(int SchoolID, CreateServiceDTO createServiceDTO)
        {
            await checkAuthManagerAsync(SchoolID);

            Service createService = _mapper.Map<Service>(createServiceDTO);
            Service service = await _serviceService.Create(createService);
            GetServiceDTO getServiceDTO = _mapper.Map<GetServiceDTO>(service);
            var result = new ServiceResponse<GetServiceDTO>()
            {
                Data = getServiceDTO,
                Success = true
            };
            return result;
        }

        [HttpPatch]
        [Route("{ServiceID}")]
        public async Task<ServiceResponse<GetServiceDTO>> Update(int SchoolID, int ServiceID, UpdateServiceDTO updateServiceDTO)
        {
            await checkAuthManagerAsync(SchoolID);

            Service service = await _serviceService.Update(ServiceID, updateServiceDTO);
            GetServiceDTO data = _mapper.Map<GetServiceDTO>(service);
            ServiceResponse<GetServiceDTO> response = new ServiceResponse<GetServiceDTO>()
            {
                Data = data,
                Success = true

            };

            return response;
        }

        [HttpDelete]
        [Route("{ServiceID}")]
        public async Task<ServiceResponse<Object>> Delete(int SchoolID, int ServiceID)
        {
            await checkAuthManagerAsync(SchoolID);

            var data = await _serviceService.Get(ServiceID);
            bool result = await _serviceService.Delete(data);
            var response = new ServiceResponse<Object>() { Success = result };
            return response;
        }

        [HttpGet]
        [Route("{ServiceID}/Student/{StudentID}")]
        public async Task<ServiceResponse<GetStudentDTO>> GetStudent(int SchoolID, int ServiceID, int StudentID)
        {
            await checkAuthManagerAsync(SchoolID);

            var student = await _serviceService.FindStudent(ServiceID, StudentID);
            var result = _mapper.Map<GetStudentDTO>(student);
            var response = new ServiceResponse<GetStudentDTO>()
            {
                Data = result,
                Success = true
            };
            return response;
        }

        [HttpPost]
        [Route("{ServiceID}/Student")]
        public async Task<ServiceResponse<GetStudentDTO>> AddStudent(int SchoolID, int ServiceID, CreateStudentDTO createStudentDTO)
        {
            await checkAuthManagerAsync(SchoolID);
            var student = _mapper.Map<Student>(createStudentDTO);
            student.ServiceID = ServiceID;
            student = await _serviceService.CreateStudent(student);
            var result = _mapper.Map<GetStudentDTO>(student);
            var response = new ServiceResponse<GetStudentDTO>() { Data = result, Success = true };
            return response;
        }

        [HttpPatch]
        [Route("{ServiceID}/Student/{StudentID}")]
        public async Task<ServiceResponse<GetStudentDTO>> UpdateStudent(int SchoolID, int ServiceID, int StudentID, UpdateStudentDTO updateStudent)
        {
            await checkAuthManagerAsync(SchoolID);

            var data = await _serviceService.UpdateStudent(ServiceID, StudentID, updateStudent);
            var result = _mapper.Map<GetStudentDTO>(data);
            var response = new ServiceResponse<GetStudentDTO>() { Data = result, Success = true };
            return response;
        }

        [HttpDelete]
        [Route("{ServiceID}/Student/{StudentID}")]
        public async Task<ServiceResponse<bool>> RemoveStudent(int SchoolID, int ServiceID, int StudentID)
        {
            await checkAuthManagerAsync(SchoolID);

            var data = await _serviceService.RemoveStudent(ServiceID, StudentID);
            var result = new ServiceResponse<bool>() { Success = data };
            return result;
        }

        private async Task<bool> checkAuthManagerAsync(int SchoolID)
        {
            bool auth = false;
            if (_userService.GetCurrentUserRole() == Roles.Admin)
            {
                auth = true;
            }
            else if (_userService.GetCurrentUserRole() == Roles.Manager)
            {
                auth = await _userService.checkAuthorityManager(SchoolID);
                if (auth == false)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            return auth;

        }

    }
}
