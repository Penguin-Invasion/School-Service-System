using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public interface IUserService
    {
        public Task<User> Create(User createUser);

        public Task<bool> DeleteManager(int ID);

        public Task<bool> DeleteDriver(int ID);

        public Task<bool> Delete(int ID);

        public Task<bool> Delete(User deletedUser);

        public Task<User> Update(int ID, UpdateUserDTO updateUser);

        public Task<User> FindManager(int ID);
        public Task<User> FindDriver(int ID);
        public Task<User> Find(string Email, string Pass);
        public Task<User> Find(int ID, int? RoleID = null);
        public Task<bool> checkAuthorityManager(int schoolID);
        public Task<User> GetMyInfo();
        public int GetCurrentUserId();
        public Roles GetCurrentUserRole();
    }
}
