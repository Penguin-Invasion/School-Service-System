using SchoolServiceSystem.DTOs.Service;
using SchoolServiceSystem.DTOs.Student;
using SchoolServiceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public interface IServiceService
    {
        public Task<Service> Get(int ID);
        public Task<IEnumerable<Service>> GetAll(Service service);

        public Task<Service> Create(Service service);

        public Task<Service> Update(int ID, UpdateServiceDTO updateService);

        public Task<bool> Delete(Service deleteService);

        public Task<Student> CreateStudent(Student student);

        public Task<Student> UpdateStudent(int ServiceID, int StudentID, UpdateStudentDTO updateStudent);

        public Task<bool> RemoveStudent(int ServiceID, int StudentID);

        public Task<Student> FindStudent(int ServiceID, int StudentID);
    }
}
