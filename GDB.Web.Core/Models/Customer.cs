using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MobileNumber { get; set; }

    public int? LocationId { get; set; }

    public int? AdvertiseSourceId { get; set; }

    public string? ReferredBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
