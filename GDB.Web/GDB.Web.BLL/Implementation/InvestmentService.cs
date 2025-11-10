using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using GDB.Web.Common.Helpers;
using GDB.Web.Shared;
using System.Net.Http.Json;

namespace GDB.Web.BLL.Implementation
{
    public class InvestmentService : IInvestmentService
    {
        public HttpClient httpClient { get; set; }

        public InvestmentService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<List<InvestmentOptionCategoryViewModel>> GetAllInvestmentCategories()
        {
            var response = await httpClient.GetAsync($"{ApiRoutes.Investments.Base}{ApiRoutes.Investments.GetAllInvestmentCategories}");
            if(!response.IsSuccessStatusCode)
                 return new List<InvestmentOptionCategoryViewModel>();

            return await response
                        .Content
                        .ReadFromJsonAsync<List<InvestmentOptionCategoryViewModel>>() 
                        ?? new List<InvestmentOptionCategoryViewModel>();
         }

        public async Task<List<InvestmentOptionSubCategoryViewModel>> GetAllInvestmentSubCategories()
        {
            var response = await httpClient.GetAsync($"{ApiRoutes.Investments.Base}{ApiRoutes.Investments.GetAllInvestmentSubCategories}");
            
            if (!response.IsSuccessStatusCode)
                return new List<InvestmentOptionSubCategoryViewModel>();

            return await response
                        .Content
                        .ReadFromJsonAsync<List<InvestmentOptionSubCategoryViewModel>>() 
                        ?? new List<InvestmentOptionSubCategoryViewModel>();
        }


        public async Task<bool?> Add(InvestmentViewModel investmentViewModel)
        {
            bool isSuccess = false;
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{ApiRoutes.Investments.Base}{ApiRoutes.Investments.AddInvestment}", investmentViewModel);

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

        public async Task<List<InvestmentViewModel>> GetAllInvestmentDetails()
        {
            var response = await httpClient.GetAsync($"{ApiRoutes.Investments.Base}{ApiRoutes.Investments.GetAllInvestments}");
            if (!response.IsSuccessStatusCode)
                return new List<InvestmentViewModel>();

            return await response
                        .Content
                        .ReadFromJsonAsync<List<InvestmentViewModel>>()
                        ?? new List<InvestmentViewModel>();
        }
    }
}
