using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class InvestmentOptionSubCategory
{
    public int InvestmentSubCategoryId { get; set; }

    public int InvestmentOptionCategoryId { get; set; }

    public string? InvestmentSubCategoryDescription { get; set; }
}
