using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class OrderStater
{
    public int OrderStaterId { get; set; }

    public int WeekId { get; set; }

    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public int StaterId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? CreatedDate { get; set; }
}
