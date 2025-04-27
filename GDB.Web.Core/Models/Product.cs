using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public int SubcategoryId { get; set; }

    public int UserId { get; set; }

    public int VendorId { get; set; }

    public string ProductName { get; set; } = null!;

    public int? FoodPackingTypeId { get; set; }

    public int Quantity { get; set; }

    public decimal ProductPrice { get; set; }

    public DateTime PurchasedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? Modifieddate { get; set; }
}
