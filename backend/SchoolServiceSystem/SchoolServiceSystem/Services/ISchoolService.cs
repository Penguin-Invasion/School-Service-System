using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public interface ISchoolService
    {
        public Task<IEnumerable<School>> GetAll();

        public Task<School> Get(int ID);

        public Task<School> Create(School school);

        public Task<bool> Delete(int ID);

        public Task<School> Update(int ID, UpdateSchoolDTO updateSchool);


        public Task<School> Find(int ID, bool manager = false, bool services = false);

        public Task<IEnumerable<School>> FindManagerSchools(int userID, bool manager = false, bool services = false);
    }
}
