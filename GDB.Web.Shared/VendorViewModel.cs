using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class VendorViewModel
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }
        public int VendorAddressId { get; set; }
        public int VendorContactId { get; set; }

    }
}
