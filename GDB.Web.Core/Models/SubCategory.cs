using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class SubCategory
{
    public int SubCategoryId { get; set; }

    public int CategoryId { get; set; }

    public int? UserId { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? Modifieddate { get; set; }
}
