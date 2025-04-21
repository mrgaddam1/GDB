using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GDB.Web.BLL.Implementation
{
    public class MLModelService : IMLModelService
    {
        public HttpClient httpClient { get; set; }
        private string errorMessage;
        public MLModelService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }    

        public async Task<T?> PredictSales<T>()
        {      
            var response = await httpClient.GetAsync("api/ML/PredictSales/");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> PredictExpenses<T>()
        {
            var response = await httpClient.GetAsync("api/ML/PredictExpenses/");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetExistingSalesData<T>()
        {
            var response = await httpClient.GetAsync("api/ML/GetExistingSalesData/");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<T?> GetExistingExpensesData<T>()
        {
            var response = await httpClient.GetAsync("api/ML/GetExistingExpensesData/");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }
    }
}
