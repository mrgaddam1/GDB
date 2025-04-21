using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class StaterService(HttpClient _httpClient) : IStaterService
    {
        public HttpClient httpClient { get; set; } = _httpClient;
   
        public async Task<T?> GetAllStaters<T>()
        {
            var response = await httpClient.GetAsync("api/Staters/GetAllStaters");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<decimal> GetStaterPriceByStater(int staterId)
        {
            var response = await httpClient.GetAsync("api/Staters/GetStaterPriceByStater/" + staterId);
            return await ApiStatusCodeHandler.HandleResponse<decimal>(response);
        }
    }
}
