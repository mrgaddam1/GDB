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
    public class ProductService : IProductService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public ProductService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<List<ProductViewModel>> GetAll<T>()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Product/GetAll");
                var data = await ApiStatusCodeHandler.HandleResponse<List<ProductViewModel>>(response);
                return data;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw new ApplicationException();
            }
        }

        public async Task<bool?> Add(ProductViewModel productViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Product/Add", productViewModel);
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

        public async Task<bool?> Update(ProductViewModel productViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Product/Update", productViewModel);
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
