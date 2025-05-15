using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class AccountsViewModel
    {
        public int WeekId { get; set; }
        public decimal TotalProfits { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
