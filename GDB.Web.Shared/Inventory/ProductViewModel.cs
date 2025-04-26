using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared.Inventory
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int VendorId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice{ get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string VendorName { get; set; }
        public DateTime PurchasedDate { get; set; }

    }
}
