using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class OrderGroup
{
    public int OrderGroupId { get; set; }

    public int UserId { get; set; }

    public int? WeekId { get; set; }

    public int? CustomerId { get; set; }

    public int? StaterId { get; set; }

    public int? OrderTypeId { get; set; }

    public int? FoodPackingTypeId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? Quantity { get; set; }

    public decimal? Amount { get; set; }

    public int? PaymentTypeId { get; set; }

    public bool? AmountPaid { get; set; }

    public DateTime? AmountPaidDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
