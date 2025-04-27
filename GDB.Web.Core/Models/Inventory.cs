using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int WeekId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int? AvailableQuantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
