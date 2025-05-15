using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class VendorAddress
{
    public int VendorAddressId { get; set; }

    public int VendorId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? Postcode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
