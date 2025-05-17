using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Grocery
{
    public int GroceryId { get; set; }

    public int? UserId { get; set; }

    public string GroceryDescription { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
