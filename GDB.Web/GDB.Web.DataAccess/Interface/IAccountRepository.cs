using GDB.Web.Core.Models;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IAccountRepository
    {
        Task<List<AccountsViewModel>> GetAllAccounts();
    }
}
