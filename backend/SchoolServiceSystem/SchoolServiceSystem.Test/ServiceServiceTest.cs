using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.FakeServices;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolServiceSystem.Test
{
    public class ServiceServiceTest
    {
        private readonly IServiceService _serviceService;
        public ServiceServiceTest()
        {
            _serviceService = new FakeServiceService();
        }

        [Fact]
        public void Create()
        {
            var service = new Service
            {
                ID = 3,
                Name = "Test Servis3",
                Plaque = "34A0003",
                DriverID = 5,
                SchoolID = 1
            };

            var result = _serviceService.Create(service).Result;

            Assert.Equal(service, result);
        }

        [Fact]
        public void CreateFail()
        {
            var result = _serviceService.Create(null).Result;
            Assert.Null(result);
        }

        [Fact]
        public void Find()
        {
            var result = _serviceService.Get(1).Result;
            Assert.NotNull(result);
            Assert.Equal("34A0001", result.Plaque);
        }

        [Fact]
        public void FindFail()
        {
            var result = _serviceService.Get(4).Result;
            Assert.Null(result);
        }

        [Fact]
        public void GetAll()
        {
            var service = new Service
            {
                SchoolID = 1,
            };
            var result = _serviceService.GetAll(service).Result;
            Assert.Equal(1, result.ToList().Count);
        }

        [Fact]
        public void Update()
        {
            var service = new UpdateServiceDTO
            {
                Name = "Test Servis1 Change"
            };

            var result = _serviceService.Update(1, service).Result;
            Assert.NotNull(result);
            Assert.Equal(service.Name, result.Name);
        }

        [Fact]
        public void UpdateFail()
        {
            var service = new UpdateServiceDTO
            {
                Name = "Test Servis1 Change"
            };

            var result = _serviceService.Update(4, service).Result;
            Assert.Null(result);
        }
        [Fact]
        public void Delete()
        {
            var service = _serviceService.Get(1).Result;
            var result = _serviceService.Delete(service).Result;
            Assert.True(result);
        }

        [Fact]
        public void DeleteFail()
        {
            var service = new Service
            {
                ID = 2,
                Name = "Test Servis123",
                Plaque = "34A000123",
                DriverID = 9,
                SchoolID = 5
            };
            var result = _serviceService.Delete(service).Result;
            Assert.False(result);
        }
    }
}
