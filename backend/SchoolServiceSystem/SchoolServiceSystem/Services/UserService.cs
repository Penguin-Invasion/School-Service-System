using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolServiceSystem.Data;
using SchoolServiceSystem.Exceptions;
using SchoolServiceSystem.Models;
using SchoolServiceSystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> Find(int ID)
        {
            return await Find(ID, null);
        }
        public async Task<User> FindManager(int ID)
        {
            return await Find(ID, (int)Role.Manager);
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
    }
}
