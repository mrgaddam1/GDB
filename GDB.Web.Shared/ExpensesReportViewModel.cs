using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class ExpensesReportViewModel
    {
        public int WeekId { get; set; }
        public string GroceryDescription { get; set; }
        public string QuantityDescription { get; set; }
        public string StoreName { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpensesDate { get; set; }

    }
}
