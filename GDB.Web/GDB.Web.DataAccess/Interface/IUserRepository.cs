using GDB.Web.Core.Models;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(string emailId, string password);
        Task<UserViewModel> AuthenticateUser(string emailId, string password);
    }
}
