using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class InvestmentSummary
{
    public int InvestmentSummaryId { get; set; }

    public int UserId { get; set; }

    public int InvestmentOptionCategoryId { get; set; }

    public int InvestmentSubCategoryId { get; set; }

    public string? Descrpition { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
