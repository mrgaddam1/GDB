using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class FoodPackingType
{
    public int FoodPackingTypeId { get; set; }

    public string FoodPackingTypeDescription { get; set; } = null!;
}
