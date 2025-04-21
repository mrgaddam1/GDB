using GDB.Web.Common.Extensions;
using GDB.Web.Common.Helpers;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private GDBContext DbContext { get; set; }

        public AccountRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public async Task<List<AccountsViewModel>> GetAllAccounts()
        {
            var accountData = new List<AccountsViewModel>();
            try
            {
                var data = DataHelper.GetData(DbContext.Database.GetDbConnection(), "Udp_Accounts_GetAllAccounts", null);
                accountData = ConvertDataTableToGenericList.ConvertDataTable<AccountsViewModel>(data).
                                   OrderByDescending(x => x.WeekId).ToList();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return accountData;
        }
    }
}
