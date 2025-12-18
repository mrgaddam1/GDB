using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class MonthlyDirectDebitPayment
{
    public int DeductionId { get; set; }

    public int UserId { get; set; }

    public int DirectDebitId { get; set; }

    public bool IsItDirectDebit { get; set; }

    public string DirectDebitMonthName { get; set; } = null!;

    public DateTime DirectDebitDate { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DirectDebitStatus { get; set; }
}
