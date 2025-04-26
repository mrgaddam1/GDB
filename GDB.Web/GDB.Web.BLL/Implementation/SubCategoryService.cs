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
    public class SubCategoryService : ISubCategoryService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public SubCategoryService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<List<SubCategoryViewModel>> GetAll<T>()
        {
            try
            {
                var response = await httpClient.GetAsync("api/SubCategory/GetAll");
                var data = await ApiStatusCodeHandler.HandleResponse<List<SubCategoryViewModel>>(response);
                return data;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new ApplicationException();
            }
        }

        public async Task<bool?> Add(SubCategoryViewModel subCategoryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/SubCategory/Add", subCategoryViewModel);
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

        public async Task<bool?> Update(SubCategoryViewModel subCategoryViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/SubCategory/Update", subCategoryViewModel);
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
