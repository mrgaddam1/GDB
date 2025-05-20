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
        public async Task<List<AccountsViewModel>> GetAllAccountsBy_Yearly()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccountsBy_Yearly");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);
            return result ?? new List<AccountsViewModel>();
        }
        public async Task<List<AccountsViewModel>> GetAllAccountsBy_HalfYearly()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccountsBy_HalfYearly");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);
            return result ?? new List<AccountsViewModel>();
        }
        public async Task<List<AccountsViewModel>> GetAllAccountsBy_Quarterly()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccountsBy_Quarterly");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);
            return result ?? new List<AccountsViewModel>();
        }
        public async Task<List<AccountsViewModel>> GetAllAccountsBy_LastMonth()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccountsBy_LastMonth");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);
            return result ?? new List<AccountsViewModel>();
        }
        public async Task<List<AccountsViewModel>> GetAllAccountsBy_BIWeekly()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccountsBy_BIWeekly");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);
            return result ?? new List<AccountsViewModel>();
        }
        public async Task<List<AccountsViewModel>> GetAllAccountsBy_Weekly()
        {
            var response = await httpClient.GetAsync("api/Accounts/GetAllAccountsBy_Weekly");
            var result = await ApiStatusCodeHandler.HandleResponse<List<AccountsViewModel>>(response);
            return result ?? new List<AccountsViewModel>();
        }

    }
}
