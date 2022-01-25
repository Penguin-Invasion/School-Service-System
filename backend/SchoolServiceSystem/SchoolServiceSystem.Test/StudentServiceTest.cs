using SchoolServiceSystem.DTOs.Student;
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
    public class StudentServiceTest
    {
        private readonly IServiceService _serviceService;
        public StudentServiceTest()
        {
            _serviceService = new FakeServiceService();
        }
        [Fact]
        public void Create()
        {
            var student = new Student
            {
                ID = 5,
                Name = "Student5",
                SurName = "Student5",
                Year = 2020,
                ServiceID = 2
            };
            var result = _serviceService.CreateStudent(student).Result;
            Assert.NotNull(result);
            Assert.Equal(student, result);
        }
        [Fact]
        public void CreateFail()
        {
            var result = _serviceService.CreateStudent(null).Result;
            Assert.Null(result);
        }
        [Fact]
        public void Find()
        {
            var result = _serviceService.FindStudent(1, 1).Result;
            Assert.NotNull(result);
            Assert.Equal(1, result.ServiceID);
            Assert.Equal(1, result.ID);
        }
        [Fact]
        public void FindFail()
        {
            var result = _serviceService.FindStudent(3, 1).Result;
            Assert.Null(result);
        }

        [Fact]
        public void Update()
        {
            var student = new UpdateStudentDTO
            {
                Name = "Student4 Change"
            };
            var result = _serviceService.UpdateStudent(2, 4, student).Result;
            Assert.NotNull(result);
            Assert.Equal(student.Name, result.Name);
        }
        [Fact]
        public void UpdateFail()
        {
            var student = new UpdateStudentDTO
            {
                Name = "Student6 Change"
            };
            var result = _serviceService.UpdateStudent(2, 6, student).Result;
            Assert.Null(result);
        }

        [Fact]
        public void Remove()
        {
            var result = _serviceService.RemoveStudent(2, 4).Result;
            Assert.True(result);
        }

        [Fact]
        public void RemoveFail()
        {
            var result = _serviceService.RemoveStudent(2, 9).Result;
            Assert.False(result);
        }
    }
}
