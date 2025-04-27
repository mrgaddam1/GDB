using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class OrderType
{
    public int OrderTypeId { get; set; }

    public string? OrderTypeName { get; set; }

    public int? FoodPackingTypeId { get; set; }
}
