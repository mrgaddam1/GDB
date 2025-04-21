using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Location
{
    public int LocationId { get; set; }
    public string? LocationDescription { get; set; }
    public int? UserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
