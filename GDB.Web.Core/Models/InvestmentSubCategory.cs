using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class InvestmentSubCategory
{
    public int InvestmentSubCategoryId { get; set; }

    public int InvestmentOptionId { get; set; }

    public string? InvestmentSubCategoryDescription { get; set; }
}
