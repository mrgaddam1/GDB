using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class OrderTypeItem
{
    public int OrderItemId { get; set; }

    public int UserId { get; set; }

    public int OrderTypeId { get; set; }

    public string ItemDescription { get; set; } = null!;

    public int ProductId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
