using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class InventoryHistory
{
    public int InventoryHistoryId { get; set; }

    public int InventoryId { get; set; }

    public string UserId { get; set; } = null!;

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int NoofItems { get; set; }

    public DateTime? CreatedDate { get; set; }
}
