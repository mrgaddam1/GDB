using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Implementation
{
    public class PaymentTypeService(HttpClient _httpClient) : IPaymentTypeService
    {
        public HttpClient httpClient { get; set; } = _httpClient;
        private string errorMessage;

        public async Task<T?> GetAllPaymentTypes<T>()
        {
            var response = await httpClient.GetAsync("api/PaymentType/GetAllPaymentTypes");
            return await ApiStatusCodeHandler.HandleResponse<T>(response);
        }

    }
}
