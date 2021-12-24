using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.DTOs.Service;
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
        [Route("{ID}")]
        public async Task<ActionResult<GetServiceDTO>> Get(int ID)
        {

            var data = await _serviceService.Get(ID);
            await checkAuthManagerAsync(data.SchoolID);

            var response = _mapper.Map<GetServiceDTO>(data);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<GetServiceDTO>> Add(CreateServiceDTO createServiceDTO)
        {
            await checkAuthManagerAsync(createServiceDTO.SchoolID);

            Service createService = _mapper.Map<Service>(createServiceDTO);
            Service service = await _serviceService.Create(createService);
            GetServiceDTO getServiceDTO = _mapper.Map<GetServiceDTO>(service);
            return getServiceDTO;
        }

        [HttpPatch]
        [Route("{ID}")]
        public async Task<ActionResult<ServiceResponse<GetServiceDTO>>> Update(int ID, UpdateServiceDTO updateServiceDTO)
        {
            Service Service = _mapper.Map<Service>(updateServiceDTO);
            Service = await _serviceService.Update(ID, updateServiceDTO);
            GetServiceDTO data = _mapper.Map<GetServiceDTO>(Service);
            ServiceResponse<GetServiceDTO> response = new ServiceResponse<GetServiceDTO>()
            {
                Data = data,
                Success = true

            };

            return response;
        }
        [HttpDelete]
        [Route("{ID}")]
        public async Task<ServiceResponse<Object>> Delete(int ID)
        {
            var data = await _serviceService.Get(ID);

            await checkAuthManagerAsync(data.SchoolID);

            bool result = await _serviceService.Delete(data);
            var response = new ServiceResponse<Object>() { Success = result };
            return response;
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
                Console.WriteLine(auth);
                if (auth == false)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            return auth;

        }

    }
}
