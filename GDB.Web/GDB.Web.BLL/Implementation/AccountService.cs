using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class AccountService : IAccountService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public AccountService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<AccountsViewModel>> GetAllAccounts()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccounts");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);          
            return result ?? new List<AccountsViewModel>();
        }
    }
}
