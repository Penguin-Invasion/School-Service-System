using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.Filters;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services.ServiceService;
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
            if (_userService.GetCurrentUserRole() == Roles.Manager)
            {
                var auth = _userService.checkAuthorityManager(data);
                if (auth == false)
                {
                    throw new UnauthorizedAccessException();
                }

            }

            var response = _mapper.Map<GetServiceDTO>(data);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<GetServiceDTO>> Add(CreateServiceDTO createServiceDTO)
        {
            Service createService = _mapper.Map<Service>(createServiceDTO);
            Service service = await _serviceService.Create(createService);
            GetServiceDTO getServiceDTO = _mapper.Map<GetServiceDTO>(service);
            return getServiceDTO;
        }
    }
}
