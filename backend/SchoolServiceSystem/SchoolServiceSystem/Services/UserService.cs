using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.DTOs.User;
using SchoolServiceSystem.Exceptions;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }


        public async Task<User> Create(User createUser)
        {
            var data = await _context.Users.AddAsync(createUser);
            int result = await _context.SaveChangesAsync();

            if (result != 1)
            {
                throw new NotCreatedException("User could not be created.");
            }

            return data.Entity;
        }

        public async Task<bool> DeleteManager(int ID)
        {
            User user = await FindManager(ID);
            return await Delete(user); ;
        }

        public async Task<bool> DeleteDriver(int ID)
        {
            User user = await FindDriver(ID);
            return await Delete(user); ;
        }


        public async Task<bool> Delete(int ID)
        {
            User user = await Find(ID);
            return await Delete(user); ;
        }

        public async Task<bool> Delete(User deletedUser)
        {
            _context.Users.Remove(deletedUser);
            int result = await _context.SaveChangesAsync();
            if (result != 1)
            {
                throw new NotDeletedException("User couldn't be deleted.");
            }

            return true;
        }

        public async Task<User> Update(int ID, UpdateUserDTO updateUser)
        {
            User user = await Find(ID);
            Patcher.Patch(user, updateUser);
            _context.Users.Update(user);
            int result = await _context.SaveChangesAsync();
            if (result != 1)
            {
                throw new NotUpdatedException("School couldn't be updated.");
            }
            return user;
        }


        public async Task<User> FindManager(int ID)
        {
            return await Find(ID, (int)Roles.Manager);
        }
        public async Task<User> FindDriver(int ID)
        {
            return await Find(ID, (int)Roles.Driver);
        }
        public async Task<User> Find(string Email, string Pass)
        {
            User user = null;
            user = await _context.Users
                    .SingleOrDefaultAsync(u => (u.Email.Equals(Email) && u.Password.Equals(Pass)));
            if (user == null)
            {
                throw new NotFoundException("User could not be found.1");
            }
            return user;
        }


        public async Task<User> Find(int ID, int? RoleID = null)
        {
            User user = null;
            try
            {
                if (RoleID == null)
                {
                    user = await _context.Users
                    .SingleOrDefaultAsync(user => user.ID.Equals(ID));
                }
                else
                {
                    user = await _context.Users
                    .Where(user => user.Role.Equals(2))//Manager arıyorum sadece
                    .SingleOrDefaultAsync(user => user.ID.Equals(ID));
                }
            }
            catch (Exception)
            {
                throw new NotFoundException("User couldn't be found.");
            }
            if (user == null)
            {
                throw new NotFoundException("User couldn't be found.");
            }
            return user;
        }

        public async Task<bool> checkAuthorityManager(int schoolID)
        {
            var userID = GetCurrentUserId();
            var result = await _context.Users.Include(u => u.School)
                    .SingleOrDefaultAsync(user =>
                        user.ID.Equals(userID)
                        && user.School.ID.Equals(schoolID)
                        && user.Role.Equals(Roles.Manager)
                    );

            if (result == null)
            {
                return false;
            }

            return true;
        }


        public async Task<User> GetMyInfo()
        {
            return await Find(GetCurrentUserId());
        }

        public int GetCurrentUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public Roles GetCurrentUserRole()
        {
            string role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            int roleID = 2;
            if (role.Equals("Admin"))
            {
                roleID = 0;
            }
            else if (role.Equals("Manager"))
            {
                roleID = 1;
            }
            return (Roles)roleID;
        }
    }
}
