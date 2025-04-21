using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class ExpensesViewModel
    {
        public int ExpensesId { get; set; }
        public int? UserId { get; set; }
        public string? GroceryDescription { get; set; }
        public string? StoreName { get; set; }
        public decimal ExpensesAmount { get; set; }
        public DateTime? ExpensesDate { get; set; }
        public int GroceryId { get; set; }
        public int StoreId { get; set; }
        public int? WeekNumber{ get; set; }
        public string? QuantityDescription { get; set; }

    }
}
