using SchoolServiceSystem.Controllers;
using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.FakeServices;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using System;
using System.Collections.Generic;
using Xunit;


namespace SchoolServiceSystem.Test
{
    public class UserServiceTest
    {
        private readonly IUserService _userService;
        public UserServiceTest()
        {

            _userService = new FakeUserService();
        }
        [Fact]
        public void Create()
        {
            var added = new User
            {
                ID = 10,
                Name = "MockData",
                SurName = "MockData",
                Email = "mock",
                Password = "123",
                Role = Utils.Roles.Admin
            };
            var result = _userService.Create(added).Result;

            Assert.Equal(added, result);
        }
        [Fact]
        public void CreateFail()
        {
            var result = _userService.Create(null).Result;
            Assert.Equal(null, result);
        }

        [Fact]
        public void Find()
        {
            var result = _userService.Find(1).Result;
            Assert.Equal("admin", result.Email);
        }
        [Fact]
        public void FindFail()
        {
            var result = _userService.Find(2).Result;
            Assert.NotEqual("admin", result.Email);
        }

        [Fact]
        public void Update()
        {
            var updateInfo = new UpdateUserDTO()
            {
                Name = "MockDataTestUpdate"
            };
            var result = _userService.Update(6, updateInfo).Result;
            Assert.Equal("MockDataTestUpdate", result.Name);
        }

        [Fact]
        public void UpdateFail()
        {
            var updateInfo = new UpdateUserDTO()
            {
                Name = "MockDataTestUpdate"
            };
            var result = _userService.Update(6, updateInfo).Result;
            Assert.NotEqual("MockData", result.Name);
        }

        [Fact]
        public void Delete()
        {
            var result = _userService.Delete(7).Result;
            Assert.Equal(true, result);
        }

        [Fact]
        public void DeleteFail()
        {
            var result = _userService.Delete(8).Result;
            Assert.NotEqual(true, result);
        }
    }
}
