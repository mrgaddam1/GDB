using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class InvestmentSecurityCheck
{
    public Guid SecurityInvestmentId { get; set; }

    public string FullName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string Security12DigitsPasscode { get; set; } = null!;

    public string Security6DigitsPincode { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int? PasswordCount { get; set; }
}
