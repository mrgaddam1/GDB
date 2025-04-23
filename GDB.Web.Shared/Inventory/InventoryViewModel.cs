using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared.Inventory
{
    public class InventoryViewModel
    {
        public int InventoryId { get; set; }
        public int WeekId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }       
    }
}
