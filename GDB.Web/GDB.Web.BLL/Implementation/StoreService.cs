using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class StoreService : IStoreService
    {
        public HttpClient httpClient { get; set; }
        public StoreService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<StoreViewModel>> GetAll<T>()
        {
            try
            {
               
                var response = await httpClient.GetAsync("api/Store/GetAll");
                var data = await ApiStatusCodeHandler.HandleResponse<List<StoreViewModel>>(response);
                return data;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new ApplicationException();
            }

        }

        public async Task<bool?> Add(StoreViewModel storeViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Store/Add", storeViewModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                    isSuccess = false;
                }
                else
                {
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return isSuccess;
            }
        }

        public async Task<bool?> Update(StoreViewModel storeViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Store/Update", storeViewModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                }
                else
                {
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return isSuccess;
            }
        }
    }
}
