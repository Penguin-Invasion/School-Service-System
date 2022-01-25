using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class EntryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEntryService _entryService;

        public EntryController(IMapper mapper, IEntryService entryService)
        {
            _mapper = mapper;
            _entryService = entryService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Add(string SecretKey, string Plaque)
        {
            var success = await _entryService.addEntry(SecretKey, Plaque);
            return new ServiceResponse<string>() { Success = success };
        }


    }
}
