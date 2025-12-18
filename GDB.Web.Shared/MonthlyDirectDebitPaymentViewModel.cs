namespace GDB.Web.Shared
{
    public class MonthlyDirectDebitPaymentViewModel
    {
        public int DeductionId { get; set; }
        public int DirectDebitId { get; set; }
        public bool IsItDirectDebit { get; set; }
        public string DirectDebitMonthName { get; set; }
        public DateTime DirectDebitDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string DirectDebitName { get; set; }
        public string DirectDebitDescription {  get; set; }
    }
}
