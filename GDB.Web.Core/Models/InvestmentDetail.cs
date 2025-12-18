using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class InvestmentDetail
{
    public int InvestmentId { get; set; }

    public int UserId { get; set; }

    public decimal InvestedAmount { get; set; }

    public DateTime InvestedDate { get; set; }

    public int InvestmentOptionId { get; set; }

    public int? InvestmentSubCategoryId { get; set; }

    public string? Descrpition { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsInvestmentComplete { get; set; }
}
