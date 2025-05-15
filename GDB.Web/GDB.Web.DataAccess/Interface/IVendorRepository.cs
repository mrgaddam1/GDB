using GDB.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Interface
{
    public interface IVendorRepository
    {
        Task<List<VendorViewModel>> GetAll();
        Task<bool> Add(VendorViewModel vendorViewModel);
        Task<bool> Update(VendorViewModel vendorViewModel);
        Task<bool> Delete(int vendorId);
    }
}
