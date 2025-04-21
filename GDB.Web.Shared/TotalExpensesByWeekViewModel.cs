using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class TotalExpensesByWeekViewModel
    {
        public int WeekId { get; set; }
        public decimal ExpensesAmount { get; set; }
    }
}
