using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class UserRepository : IUserRepository
    {
         private readonly ILogger<UserRepository> logger;
        private GDBContext DbContext { get; set; }

        public UserRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }

        public async Task<bool> RegisterUser(string  emailId, string password)
        {
            var existingUser = await DbContext.Users.FirstOrDefaultAsync(u => u.EmailId == emailId);
            if (existingUser != null) return false; // User already exists

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { EmailId = emailId, Password = hashedPassword };
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserViewModel?> AuthenticateUser(string emailId, string password)
        {
            var userViewModel = new UserViewModel();
            var user = await DbContext.Users.FirstOrDefaultAsync(u => u.EmailId == emailId);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return userViewModel = null;
            }
            else
            {
                userViewModel.UserName = user.FirstName + " " + user.LastName;
                userViewModel.UserId = user.UserId;
                userViewModel.Email = user.EmailId;
                userViewModel.PasswordHash = user.Password;
                userViewModel.Role = null;
            }              
         
            return userViewModel;
        }

    }
}
