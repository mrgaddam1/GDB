using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Shared;
using GDB.Web.Shared.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class CategoryService : ICategoryService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public CategoryService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<List<CategoryViewModel>> GetAll<T>()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Category/GetAll");
                var data = await ApiStatusCodeHandler.HandleResponse<List<CategoryViewModel>>(response);
                return data;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new ApplicationException();
            }
        }

        public async  Task<bool?> Add(CategoryViewModel categoryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Category/Add", categoryViewModel);
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

        public async Task<bool?> Update(CategoryViewModel categoryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Category/Update", categoryViewModel);
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
