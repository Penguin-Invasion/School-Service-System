using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.Exceptions;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
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
            IEnumerable<School> data = await _context.Schools.Include(s => s.User).Include(s => s.Services).ToListAsync();
            return data;
        }

        public async Task<School> Get(int ID)
        {
            School school = await Find(ID);
            return school;
        }
        public async Task<School> Create(School school)
        {
            school.SecretKey = Patcher.RandomString(30);
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
            School school = await Find(ID, true, true);
            _context.Schools.Remove(school);
            int result = await _context.SaveChangesAsync();
            if (result != 1)
            {
                throw new NotDeletedException("School couldn't be deleted.");
            }

            return true;
        }

        public async Task<School> Update(int ID, UpdateSchoolDTO updateSchool)
        {
            School school = await Find(ID);
            Patcher.Patch(school, updateSchool);
            _context.Schools.Update(school);
            int result = await _context.SaveChangesAsync();
            if (result != 1)
            {
                throw new NotUpdatedException("School couldn't be updated.");
            }
            return school;
        }


        public async Task<School> Find(int ID, bool manager = false, bool services = false)
        {
            School school;
            try
            {
                if (manager && services)
                {
                    school = await _context.Schools
                        .Include(school => school.User)
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
                        .Include(school => school.User)
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

        public async Task<IEnumerable<School>> FindManagerSchools(int userID, bool manager = false, bool services = false)
        {
            IEnumerable<School> schools;
            try
            {
                if (manager && services)
                {
                    schools = await _context.Schools
                        .Include(school => school.User)
                        .Include(school => school.Services)
                        .Where(school => school.User.ID.Equals(userID)).ToListAsync();
                }
                else if (!manager && !services)
                {
                    schools = await _context.Schools.Where(school => school.User.ID.Equals(userID)).ToListAsync();
                }
                else if (manager)
                {
                    schools = await _context.Schools
                        .Include(school => school.User)
                        .Where(school => school.User.ID.Equals(userID)).ToListAsync();
                }
                else
                {
                    schools = await _context.Schools
                        .Include(school => school.Services)
                        .Where(school => school.User.ID.Equals(userID)).ToListAsync();
                }

            }
            catch (Exception)
            {
                throw new NotFoundException("School couldn't be found.");
            }
            if (schools == null)
            {
                throw new NotFoundException("School couldn't be found.");
            }
            return schools;
        }
    }
}
