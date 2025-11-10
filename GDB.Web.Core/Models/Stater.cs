using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class Stater
{
    public int StaterId { get; set; }

    public string? StaterDescription { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }
}
