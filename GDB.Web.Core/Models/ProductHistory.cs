using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class ProductHistory
{
    public int ProductHistoryId { get; set; }

    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public DateTime? PurchasedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
