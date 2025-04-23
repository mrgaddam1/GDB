using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared.Inventory
{
    public class SubCategoryViewModel
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryDescription { get; set; }
        public int UserId { get; set; }
    }
}
