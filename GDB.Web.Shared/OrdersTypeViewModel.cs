using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class OrderTypeViewModel
    {
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }
        public int? FoodPackingTypeId { get; set; }
        public string? FoodPackingTypeDescription { get; set; }
        public decimal Price { get; set; }
    }
}
