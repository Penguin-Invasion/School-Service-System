using SchoolServiceSystem.DTOs.School;
using SchoolServiceSystem.FakeServices;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolServiceSystem.Test
{
    public class SchoolServiceTest
    {
        private readonly FakeSchoolService _fakeSchoolService;
        public SchoolServiceTest()
        {
            _fakeSchoolService = new FakeSchoolService();
        }

        [Fact]
        public void Create()
        {
            var school = new School
            {
                ID = 3,
                Name = "Okul MockData",
                SecretKey = "A1B3C23",
                UserID = 2
            };

            var result = _fakeSchoolService.Create(school).Result;

            Assert.Equal(school, result);
        }

        [Fact]
        public void CreateFail()
        {
            var result = _fakeSchoolService.Create(null).Result;
            Assert.Equal(null, result);
        }

        [Fact]
        public void Find()
        {
            var result = _fakeSchoolService.Find(1).Result;
            Assert.Equal("Test Okul1", result.Name);
        }

        [Fact]
        public void FindFail()
        {
            var result = _fakeSchoolService.Find(2).Result;
            Assert.Equal("Test Okul2", result.Name);
        }

        [Fact]
        public void Update()
        {
            var updateInfo = new UpdateSchoolDTO()
            {
                Name = "MockDataTestUpdate"
            };
            var result = _fakeSchoolService.Update(2, updateInfo).Result;
            Assert.Equal("MockDataTestUpdate", result.Name);
        }

        [Fact]
        public void UpdateFail()
        {
            var updateInfo = new UpdateSchoolDTO()
            {
                Name = "MockDataTestUpdate2"
            };
            var result = _fakeSchoolService.Update(2, updateInfo).Result;
            Assert.NotEqual("MockDataTestUpdate", result.Name);
        }


        [Fact]
        public void Delete()
        {
            var result = _fakeSchoolService.Delete(2).Result;
            Assert.Equal(true, result);
        }

        [Fact]
        public void DeleteFail()
        {
            var result = _fakeSchoolService.Delete(8).Result;
            Assert.NotEqual(true, result);
        }
    }
}
