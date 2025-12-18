using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class MonthlyDirectDebit
{
    public int DirectDebitId { get; set; }

    public string DirectDebitName { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? DirectDebitDescription { get; set; }
}
