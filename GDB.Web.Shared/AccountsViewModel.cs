namespace GDB.Web.Shared
{
    public class AccountsViewModel
    {
        public int WeekId { get; set; }
        public decimal TotalProfits { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit { get; set; }
        public int NumberOfOrders { get; set; }
        //public string OrderMonthName { get; set; }
        //public string ExpensesMonthName { get; set; }
        //public string ExpensesYear { get; set; }
        //public string OrderYear { get; set; }
    }
}
