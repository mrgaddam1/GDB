using GDB.Web.Core.Models;
using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface ICustomerRepository
    {
        Task<List<CustomerViewModel>> GetAll();
        Task<bool> Add(CustomerViewModel customerViewModel);
        Task<bool> Update(CustomerViewModel customerViewModel);

        Task<List<CustomerVisits>> GetAllCustomerVisits();
        Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Weekly();
        Task<List<CustomerVisits>> GetAllCustomerVisitsBy_BIWeekly();
        Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Monthly();
        Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Quarterly();
        Task<List<CustomerVisits>> GetAllCustomerVisitsBy_HalyYearly();
        Task<List<CustomerVisits>> GetAllCustomerVisitsBy_Yearly();

    }
}
