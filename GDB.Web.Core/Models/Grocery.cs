using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Grocery
{
    public int GroceryId { get; set; }

    public string GroceryDescription { get; set; } = null!;
}
