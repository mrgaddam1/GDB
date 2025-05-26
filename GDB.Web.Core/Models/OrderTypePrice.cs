using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class OrderTypePrice
{
    public int OrderTypePriceId { get; set; }

    public int OrderTypeId { get; set; }

    public decimal OrderTypePrice1 { get; set; }
}
