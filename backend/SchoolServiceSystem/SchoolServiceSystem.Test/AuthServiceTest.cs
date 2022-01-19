using SchoolServiceSystem.FakeServices;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolServiceSystem.Test
{
    public class AuthServiceTest
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthServiceTest()
        {
            _userService = new FakeUserService();
            _authService = new FakeAuthService(_userService);
        }

        [Fact]
        public void Find()
        {
            var search = new User
            {
                Email = "admin",
                Password = "123"
            };
            var result = _authService.Login(search).Result;

            Assert.Equal(search.Email, result.Email);
        }

        [Fact]
        public void FindFail()
        {
            var search = new User
            {
                Email = "admin123",
                Password = "123"
            };
            var result = _authService.Login(search).Result;

            Assert.Null(result);
        }
    }
}
