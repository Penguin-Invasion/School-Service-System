using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public class EntryService : IEntryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceService _serviceService;
        private readonly ISchoolService _schoolService;

        public EntryService(DataContext context, IMapper mapper, IServiceService serviceService, ISchoolService schoolService)
        {
            _context = context;
            _mapper = mapper;
            _serviceService = serviceService;
            _schoolService = schoolService;
        }

        public async Task<bool> addEntry(string SecretKey, string Plaque)
        {
            try
            {
                var service = await _context.Services
                .Include(service => service.Entries)
                .SingleOrDefaultAsync(service => service.Plaque.Equals(Plaque) && service.School.SecretKey.Equals(SecretKey));

                if (service == null)
                {
                    return false;
                }

                await _context.Entries.AddAsync(new Entry()
                {
                    Time = System.DateTime.Now,
                    ServiceID = service.ID
                });

                int result = await _context.SaveChangesAsync();
                Console.WriteLine(result);
                if (result != 1)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw new NotFoundException();
            }


            return true;
        }
    }
}
