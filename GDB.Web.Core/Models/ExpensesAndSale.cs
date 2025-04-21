using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class ExpensesAndSale
{
    public int Esid { get; set; }

    public int? UserId { get; set; }

    public int? WeekId { get; set; }

    public decimal TotalExpenses { get; set; }

    public decimal? TotalSales { get; set; }

    public DateTime? CreatedDate { get; set; }
}
