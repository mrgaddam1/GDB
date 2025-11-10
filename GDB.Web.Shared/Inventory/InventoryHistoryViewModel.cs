using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared.Inventory
{
    public class InventoryHistoryViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int WeekId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ProductName { get; set; }
        public DateTime PurchasedDate { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
