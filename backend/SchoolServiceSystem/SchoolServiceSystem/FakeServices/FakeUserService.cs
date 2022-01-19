using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Services;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.FakeServices
{
    public class FakeUserService : IUserService
    {
        private readonly List<User> _mockData;
        public FakeUserService()
        {
            _mockData = new List<User>
            {
                 new User
                    {
                        ID = 1,
                        Name = "Admin",
                        SurName = "Test",
                        Email = "admin",
                        Password = "123",
                        Role = Utils.Roles.Admin
                    },
                    new User
                    {
                        ID = 2,
                        Name = "Manager1",
                        SurName = "Test",
                        Email = "manager1",
                        Password = "123",
                        Role = Utils.Roles.Manager
                    },
                    new User
                    {
                        ID = 3,
                        Name = "Manager2",
                        SurName = "Test",
                        Email = "manager2",
                        Password = "123",
                        Role = Utils.Roles.Manager
                    },
                    new User
                    {
                        ID = 4,
                        Name = "Driver1",
                        SurName = "Test",
                        Email = "driver1",
                        Password = "123",
                        Role = Utils.Roles.Driver
                    },
                    new User
                    {
                        ID = 5,
                        Name = "Driver2",
                        SurName = "Test",
                        Email = "driver2",
                        Password = "123",
                        Role = Utils.Roles.Driver
                    },
                    new User
                    {
                        ID = 6,
                        Name = "MockData",
                        SurName = "MockData",
                        Email = "mockdata6",
                        Password = "123",
                        Role = Utils.Roles.Driver
                    }
                    ,
                    new User
                    {
                        ID = 7,
                        Name = "MockData",
                        SurName = "MockData",
                        Email = "mockdata7",
                        Password = "123",
                        Role = Utils.Roles.Driver
                    }
            }; ;
        }
        public Task<bool> checkAuthorityManager(int schoolID)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Create(User createUser)
        {
            await Task.Delay(1);
            if (createUser == null)
            {
                return null;
            }
            _mockData.Add(createUser);

            return createUser;
        }

        public async Task<bool> Delete(int ID)
        {
            var search = await Find(ID);
            if (search == null)
            {
                return false;
            }
            return _mockData.Remove(search);
        }

        public async Task<bool> Delete(User deletedUser)
        {
            await Task.Delay(1);
            if (deletedUser == null)
            {
                return false;
            }
            return _mockData.Remove(deletedUser);
        }

        public async Task<bool> DeleteDriver(int ID)
        {
            var search = await FindDriver(ID);
            if (search == null)
            {
                return false;
            }

            return await Delete(search);
        }

        public async Task<bool> DeleteManager(int ID)
        {
            var search = await FindManager(ID);
            if (search == null)
            {
                return false;
            }

            return await Delete(search);
        }

        public async Task<User> Find(string Email, string Pass)
        {
            await Task.Delay(1);
            return _mockData.SingleOrDefault(u => u.Email.Equals(Email) && u.Password.Equals(Pass));
        }

        public async Task<User> Find(int ID, int? RoleID = null)
        {
            await Task.Delay(1);
            if (RoleID == null)
            {
                return _mockData.SingleOrDefault(u => u.ID.Equals(ID));
            }
            return _mockData.SingleOrDefault(u => u.ID.Equals(ID) && u.Role.Equals(RoleID));
        }

        public async Task<User> FindDriver(int ID)
        {
            return await Find(ID, 2);
        }

        public async Task<User> FindManager(int ID)
        {
            return await Find(ID, 1);
        }

        public int GetCurrentUserId()
        {
            throw new NotImplementedException();
        }

        public Roles GetCurrentUserRole()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetMyInfo()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(int ID, UpdateUserDTO updateUser)
        {
            var search = await Find(ID);
            if (search == null)
            {
                return null;
            }
            Patcher.Patch(search, updateUser);
            return search;
        }
    }
}
