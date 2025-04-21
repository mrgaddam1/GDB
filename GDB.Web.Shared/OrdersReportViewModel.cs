using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class OrdersReportViewModel
    {
        public int WeekId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Location { get; set; }
        public string OrderTypeName { get; set; }  
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime AmountPaidDate { get; set; }
      
        
    }
}
