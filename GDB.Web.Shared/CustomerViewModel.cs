using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNumber { get; set; }
        public int? locationId { get; set; }
        public string? LocationName { get; set; }
        public string? AdvertiseSource { get; set; }
        public int? AdvertiseSourceId { get; set; }
        public string? RefferedBy { get; set; }

    }
}
