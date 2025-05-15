using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class VendorContact
{
    public int VendorContactId { get; set; }

    public int VendorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ContactNumber { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
