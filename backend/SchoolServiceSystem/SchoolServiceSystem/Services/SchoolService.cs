using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.Exceptions;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services.ScoolService
{
    public class SchoolService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public SchoolService(DataContext context, IMapper mapper, UserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<IEnumerable<School>> GetAll()
        {
            IEnumerable<School> data = await _context.Schools.ToListAsync();
            return data;
        }

        public async Task<School> Get(int ID)
        {
            School school = await Find(ID);
            return school;
        }
        public async Task<School> Create(School school)
        {
            await _context.Schools.AddAsync(school);
            int result = await _context.SaveChangesAsync();

            if (result != 1)
            {
                throw new NotCreatedException("Schools could not be created.");
            }

            return school;

        }
        public async Task<bool> Delete(int ID)
        {
            School school = await Find(ID);
            _context.Schools.Remove(school);
            int result = await _context.SaveChangesAsync();
            if (result != 1)
            {
                throw new NotDeletedException("School couldn't be deleted.");
            }

            return true;
        }

        public async Task<School> Update(int id, School updateSchool)
        {
            School school = await Find(id);
            _mapper.Map<School, School>(updateSchool, school);
            _context.Schools.Update(school);
            int result = await _context.SaveChangesAsync();
            if (result != 1)
            {
                throw new NotUpdatedException("School couldn't be updated.");
            }
            return school;
        }

        public async Task<bool> AddManager(int ID, int managerID)
        {

            School school = await Find(ID, true);
            User manager = await _userService.FindManager(managerID);

            school.Users.Add(manager);
            int result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                throw new NotCreatedException("Manager couldn't be added to school");
            }

            return true;
        }


        public async Task<bool> RemoveManager(int ID, int managerID)
        {
            School school = await Find(ID, true);

            User manager = school.Users.SingleOrDefault(user => user.ID.Equals(managerID));
            if (manager == null)
            {
                throw new NotDeletedException("Manager couldn't be deleted.");
            }
            school.Users.Remove(manager);
            int result = await _context.SaveChangesAsync();
            if (result < 1)
            {
                throw new NotCreatedException("Manager couldn't be deleted from school");
            }

            return true;
        }
        public async Task<School> Find(int ID, bool manager = false, bool services = false)
        {
            School school;
            try
            {
                if (manager && services)
                {
                    school = await _context.Schools
                        .Include(school => school.Users)
                        .Include(school => school.Services)
                        .SingleOrDefaultAsync(school => school.ID.Equals(ID));
                }
                else if (!manager && !services)
                {
                    school = await _context.Schools.SingleOrDefaultAsync(school => school.ID.Equals(ID));
                }
                else if (manager)
                {
                    school = await _context.Schools
                        .Include(school => school.Users)
                        .SingleOrDefaultAsync(school => school.ID.Equals(ID));
                }
                else
                {
                    school = await _context.Schools
                        .Include(school => school.Services)
                        .SingleOrDefaultAsync(school => school.ID.Equals(ID));
                }

            }
            catch (Exception)
            {
                throw new NotFoundException("School couldn't be found.");
            }
            if (school == null)
            {
                throw new NotFoundException("School couldn't be found.");
            }
            return school;
        }
    }
}
