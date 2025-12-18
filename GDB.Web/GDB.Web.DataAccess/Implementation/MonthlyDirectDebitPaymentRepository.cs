
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GDB.Web.DataAccess.Implementation
{
    public class MonthlyDirectDebitPaymentRepository : IMonthlyDirectDebitPaymentRepository
    {
        private readonly ILogger<MonthlyDirectDebitPaymentRepository> logger;
        private GDBContext DbContext { get; set; }

        public MonthlyDirectDebitPaymentRepository(GDBContext _DbContext, ILogger<MonthlyDirectDebitPaymentRepository> _logger)
        {
            DbContext = _DbContext;
            logger = _logger;
        }
        public async Task<bool> AddMonthlyDirectDebitPaymentsAsync(MonthlyDirectDebitPaymentViewModel monthlyDirectDebitPayments)
        {
            bool directDebitPaymentStatus = false;

            try
            {
                var entity = new MonthlyDirectDebitPayment
                {
                    DirectDebitId = monthlyDirectDebitPayments.DirectDebitId,
                    IsItDirectDebit = monthlyDirectDebitPayments.IsItDirectDebit,
                    DirectDebitMonthName = monthlyDirectDebitPayments.DirectDebitDate.Date.Month.ToString(),
                    DirectDebitDate = monthlyDirectDebitPayments.DirectDebitDate,
                    Amount = monthlyDirectDebitPayments.Amount,
                    Description = monthlyDirectDebitPayments.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = 1, // To be replaced with actual user id    
                    DirectDebitStatus = "Active",


                };
                DbContext.MonthlyDirectDebitPayments.Add(entity);
                DbContext.SaveChanges();
                return directDebitPaymentStatus = true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return directDebitPaymentStatus;
            }
        }

        public async Task<bool> DeleteMonthlyDirectDebitPaymentsAsync(int deductionId)
        {
            bool directDebitPaymentStatus = false;
            try
            {
                var data = await DbContext.MonthlyDirectDebitPayments.Where(x => x.DeductionId == deductionId).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.UserId = 1;
                    data.DirectDebitStatus = "InActive";
                    data.DeletedDate = DateTime.UtcNow;
                    DbContext.MonthlyDirectDebitPayments.Update(data);
                    await DbContext.SaveChangesAsync();
                    directDebitPaymentStatus = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return directDebitPaymentStatus;
            }
            return directDebitPaymentStatus;
        }

        public async Task<List<MonthlyDirectDebitPaymentViewModel>> GetMonthlyDirectDebitPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MonthlyDirectDebitViewModel>> GetMonthlyDirectDebitsAsync()
        {
            var monthlyDirectDebitViewModel = new List<MonthlyDirectDebitViewModel>();
            try
            {
                monthlyDirectDebitViewModel = await DbContext.MonthlyDirectDebits.Select(dd => new MonthlyDirectDebitViewModel
                {
                    DirectDebitId = dd.DirectDebitId,
                    DirectDebitName = dd.DirectDebitName,
                    DirectDebitDescription = dd.DirectDebitDescription,
                    Amount = dd.Amount
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                monthlyDirectDebitViewModel = new List<MonthlyDirectDebitViewModel>();
            }
            return monthlyDirectDebitViewModel;
        }

        public async Task<bool> UpdateMonthlyDirectDebitPaymentsAsync(MonthlyDirectDebitPaymentViewModel monthlyDirectDebitPayments)
        {
            bool directDebitPaymentStatus = false;
            try
            {
                var data = await DbContext.MonthlyDirectDebitPayments.Where(x => x.DeductionId == monthlyDirectDebitPayments.DeductionId).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.DirectDebitId = monthlyDirectDebitPayments.DirectDebitId;
                    data.IsItDirectDebit = monthlyDirectDebitPayments.IsItDirectDebit;
                    data.DirectDebitMonthName = monthlyDirectDebitPayments.DirectDebitDate.Date.Month.ToString();
                    data.DirectDebitDate = monthlyDirectDebitPayments.DirectDebitDate;
                    data.Amount = monthlyDirectDebitPayments.Amount;
                    data.Description = monthlyDirectDebitPayments.Description;
                    data.UserId = 1; 
                    data.DirectDebitStatus = "Active";
                    data.ModifiedDate = DateTime.UtcNow;
                    DbContext.MonthlyDirectDebitPayments.Update(data);
                    await DbContext.SaveChangesAsync();
                    directDebitPaymentStatus = true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "An error occured while processing the request.");
                return directDebitPaymentStatus;
            }
            return directDebitPaymentStatus;
        }
    }
}
