using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.FakeServices
{
    public class FakeSchoolService : ISchoolService
    {
        private readonly List<School> _mockData;
        public FakeSchoolService()
        {
            _mockData = new List<School>
            {
                new School
                    {
                        ID = 1,
                        Name = "Test Okul1",
                        SecretKey = "RKF6GUMC84WH2BRM7FRLOK67WWRSZG",
                        UserID = 2

                    },
                    new School
                    {
                        ID = 2,
                        Name = "Test Okul2",
                        SecretKey = "0AT454ZZXN3PCZWU3YYA4CHLBHOHJU",
                        UserID = 3

                    }
            };
        }
        public async Task<School> Create(School school)
        {
            await Task.Delay(1);
            if (school == null)
            {
                return null;
            }
            _mockData.Add(school);

            return school;
        }

        public async Task<School> Get(int ID)
        {
            await Task.Delay(1);
            var search = await Find(ID);
            if (search == null)
            {
                return null;
            }

            return search;
        }

        public async Task<bool> Delete(int ID)
        {
            await Task.Delay(1);
            var search = await Find(ID);
            if (search == null)
            {
                return false;
            }
            return _mockData.Remove(search);
        }

        public async Task<School> Find(int ID, bool manager = false, bool services = false)
        {
            await Task.Delay(1);
            var search = _mockData.SingleOrDefault(s => s.ID.Equals(ID));
            if (search == null)
            {
                return null;
            }
            return search;
        }

        public async Task<IEnumerable<School>> FindManagerSchools(int userID, bool manager = false, bool services = false)
        {
            await Task.Delay(1);
            var search = _mockData.Where(s => s.UserID.Equals(userID));
            if (search == null)
            {
                return null;
            }
            return search;
        }

        public async Task<IEnumerable<School>> GetAll()
        {
            await Task.Delay(1);
            return _mockData;
        }

        public async Task<School> Update(int ID, UpdateSchoolDTO updateSchool)
        {
            await Task.Delay(1);
            var search = await Find(ID);
            if (search == null)
            {
                return null;
            }
            Patcher.Patch(search, updateSchool);
            return search;
        }
    }
}
