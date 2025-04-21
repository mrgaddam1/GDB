using GDB.Web.Core.Models;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.BLL.Interface
{
    public interface ICustomerService
    {
        Task<T?> GetAllCustomers<T>();
        Task<bool?> Add(CustomerViewModel customerViewModel);
        Task<bool?> Update(CustomerViewModel customerViewModel);
        Task<T?> GetAllCustomerVisits<T>();
        Task<T?> GetAllCustomerVisitsBy_Weekly<T>();
        Task<T?> GetAllCustomerVisitsBy_BIWeekly<T>();
        Task<T?> GetAllCustomerVisitsBy_Monthly<T>();
        Task<T?> GetAllCustomerVisitsBy_Quarterly<T>();
        Task<T?> GetAllCustomerVisitsBy_HalyYearly<T>();
        Task<T?> GetAllCustomerVisitsBy_Yearly<T>();
    }
}
