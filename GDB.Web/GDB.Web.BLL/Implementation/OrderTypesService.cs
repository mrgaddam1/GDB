using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class OrderTypesService(HttpClient _httpClient) : IOrdersTypeService
    {
        public HttpClient httpClient { get; set; } = _httpClient;
        private string errorMessage;

        public async Task<T?> GetAllOrderTypes<T>()
        {
            var response = await httpClient.GetAsync("api/OrderTypes/GetAllOrderTypes");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

        public async Task<decimal?> GetOrderTypePriceByOrderType(int orderTypeId)
        {
            var response = await httpClient.GetAsync("api/OrderTypes/GetOrderTypeItemPriceByOrderType/" + orderTypeId);
            return await ApiStatusCodeHandler.HandleResponse<decimal>(response);
        }

        
    }
}
