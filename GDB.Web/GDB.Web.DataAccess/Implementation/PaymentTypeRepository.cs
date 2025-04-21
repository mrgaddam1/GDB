using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.DataAccess.Implementation
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly ILogger<PaymentTypeRepository> logger;
        private GDBContext DbContext { get; set; }

        public PaymentTypeRepository(GDBContext _DbContext)
        {
            DbContext = _DbContext;
        }        

        public async Task<List<PaymentTypeViewModel>> GetAll()
        {
            var paymentTypeData = new List<PaymentTypeViewModel>();
            paymentTypeData = await (from p in DbContext.PaymentTypes.AsNoTracking()
                                     select new PaymentTypeViewModel
                                     {
                                        PaymentTypeId = p.PaymentTypeId,
                                        PaymentTypeDescription = p.PaymentTypeDescription,
                                     }).OrderBy(x => x.PaymentTypeDescription).ToListAsync();

            return paymentTypeData;
        }
    }
}
