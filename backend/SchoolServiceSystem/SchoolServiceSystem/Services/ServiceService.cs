using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.Exceptions;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public class ServiceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public ServiceService(DataContext context, IMapper mapper, UserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<Service> Get(int ID)
        {
            var result = await _context.Services
                        .Include(s => s.School)
                        .Include(s => s.Students)
                        .SingleOrDefaultAsync(s => s.ID.Equals(ID));
            if (result == null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<IEnumerable<Service>> GetAll(Service service)
        {
            var results = await _context.Services
                    .Include(s => s.School)
                    .Include(s => s.Students)
                    .Where(s => s.SchoolID.Equals(service.SchoolID)).ToListAsync();
            if (results == null)
            {
                throw new NotFoundException();
            }
            return results;
        }

        public async Task<Service> Create(Service service)
        {
            await _context.Services.AddAsync(service);
            int result = await _context.SaveChangesAsync();

            if (result != 1)
            {
                throw new NotCreatedException("Schools could not be created.");
            }

            return service;
        }

        public async Task<Service> Update(int ID, UpdateServiceDTO updateService)
        {
            var service = _context.Services
                        .SingleOrDefault(s => s.ID.Equals(ID));
            Patcher.Patch(service, updateService);
            _context.Services.Update(service);
            int result = await _context.SaveChangesAsync();
            Console.WriteLine(result);
            if (result != 1)
            {
                throw new NotUpdatedException("Service couldn't be updated.");
            }
            return service;
        }


        public async Task<bool> Delete(Service deleteService)
        {
            _context.Services.Remove(deleteService);
            int check = await _context.SaveChangesAsync();
            if (check != 1)
            {
                throw new NotDeletedException("Service couldn't be deleted.");
            }
            return true;

        }
    }
}
