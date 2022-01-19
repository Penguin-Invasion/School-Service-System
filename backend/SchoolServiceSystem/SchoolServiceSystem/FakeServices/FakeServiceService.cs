using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.DTOs.Student;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.FakeServices
{
    public class FakeServiceService : IServiceService
    {
        private readonly List<Service> _mockData;
        private readonly List<Student> _mockDataStudent;

        public FakeServiceService()
        {
            _mockData = new List<Service>
            {
                new Service
                {
                    ID = 1,
                    Name = "Test Servis1",
                    Plaque = "34A0001",
                    DriverID = 4,
                    SchoolID = 1
                },
                new Service
                {
                    ID = 2,
                    Name = "Test Servis2",
                    Plaque = "34A0002",
                    DriverID = 5,
                    SchoolID = 2
                }
            };

            _mockDataStudent = new List<Student>
            {
                new Student
                {
                    ID=1,
                    Name="Student1",
                    SurName="Student1",
                    Year=2020,
                    ServiceID=1
                },
                new Student
                {
                    ID=2,
                    Name="Student2",
                    SurName="Student2",
                    Year=2020,
                    ServiceID=1
                },
                new Student
                {
                    ID=3,
                    Name="Student3",
                    SurName="Student3",
                    Year=2020,
                    ServiceID=2
                },
                new Student
                {
                    ID=4,
                    Name="Student4",
                    SurName="Student4",
                    Year=2020,
                    ServiceID=2
                }
            };
        }
        public async Task<Service> Create(Service service)
        {
            await Task.Delay(1);
            if (service == null)
            {
                return null;
            }
            _mockData.Add(service);

            return service;

        }
        public async Task<Service> Get(int ID)
        {
            await Task.Delay(1);
            return _mockData.SingleOrDefault(s => s.ID.Equals(ID));
        }

        public async Task<IEnumerable<Service>> GetAll(Service service)
        {
            if (service == null)
            {
                return null;
            }
            await Task.Delay(1);
            return _mockData.Where(s => s.SchoolID.Equals(service.SchoolID));
        }

        public async Task<bool> Delete(Service deleteService)
        {
            await Task.Delay(1);
            if (deleteService == null)
            {
                return false;
            }
            return _mockData.Remove(deleteService);
        }

        public async Task<Service> Update(int ID, UpdateServiceDTO updateService)
        {

            var service = await Get(ID);
            if (updateService == null || service == null)
            {
                return null;
            }
            Patcher.Patch(service, updateService);
            return service;
        }

        public async Task<Student> CreateStudent(Student student)
        {
            await Task.Delay(1);
            if (student == null)
            {
                return null;
            }
            _mockDataStudent.Add(student);

            return student;
        }



        public async Task<Student> FindStudent(int ServiceID, int StudentID)
        {
            var service = await Get(ServiceID);
            if (service == null)
            {
                return null;
            }
            return _mockDataStudent.SingleOrDefault(s => s.ServiceID.Equals(ServiceID) && s.ID.Equals(StudentID));
        }



        public async Task<bool> RemoveStudent(int ServiceID, int StudentID)
        {
            var student = await FindStudent(ServiceID, StudentID);
            if (student == null)
            {
                return false;
            }
            return _mockDataStudent.Remove(student);
        }



        public async Task<Student> UpdateStudent(int ServiceID, int StudentID, UpdateStudentDTO updateStudent)
        {
            var student = await FindStudent(ServiceID, StudentID);
            if (student == null)
            {
                return null;
            }
            Patcher.Patch(student, updateStudent);

            return student;
        }
    }
}
