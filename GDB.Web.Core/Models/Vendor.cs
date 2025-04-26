using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string VendorName { get; set; } = null!;
}
