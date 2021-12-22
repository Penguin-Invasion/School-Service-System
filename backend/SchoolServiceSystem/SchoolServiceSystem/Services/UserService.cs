using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
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
    public class UserService
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

        public async Task<User> Find(int ID)
        {
            return await Find(ID, null);
        }
        public async Task<User> FindManager(int ID)
        {
            return await Find(ID, (int)Roles.Manager);
        }
        public async Task<User> Find(string Email, string Pass)
        {
            User user = null;
            try
            {
                user = await _context.Users
                    .SingleOrDefaultAsync(u => (u.Email.Equals(Email) && u.Password.Equals(Pass)));
                if (user == null)
                {
                    throw new NotFoundException("User could not be found.");
                }
                return user;
            }
            catch (Exception)
            {

                throw new NotFoundException("User could not be found.");
            }
        }


        public async Task<User> Find(int ID, int? RoleID)
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
            var result = await _context.Users
                    .SingleOrDefaultAsync(user => user.ID.Equals(userID) && user.SchoolID.Equals(schoolID));
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
            int roleID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            return (Roles)roleID;
        }
    }
}
