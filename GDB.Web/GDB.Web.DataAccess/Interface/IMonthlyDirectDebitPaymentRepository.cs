using GDB.Web.Shared;

namespace GDB.Web.DataAccess.Interface
{
    public interface IMonthlyDirectDebitPaymentRepository
    {
        public Task<List<MonthlyDirectDebitPaymentViewModel>> GetMonthlyDirectDebitPaymentsAsync();
        public Task<List<MonthlyDirectDebitViewModel>> GetMonthlyDirectDebitsAsync();
        public Task<bool> AddMonthlyDirectDebitPaymentsAsync(MonthlyDirectDebitPaymentViewModel monthlyDirectDebitPayments);
        public Task<bool> DeleteMonthlyDirectDebitPaymentsAsync(int deductionId);
        public Task<bool> UpdateMonthlyDirectDebitPaymentsAsync(MonthlyDirectDebitPaymentViewModel monthlyDirectDebitPayments);
    }
}
