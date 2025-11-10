using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class StatersViewModel
    {
        public int StaterId { get; set; }   
        public string StarterDescription { get; set; }

        public decimal? StaterPrice { get; set; }
    }
}
