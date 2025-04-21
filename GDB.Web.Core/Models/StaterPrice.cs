using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class StaterPrice
{
    public int StaterItemId { get; set; }

    public int StaterId { get; set; }

    public decimal StaterPrice1 { get; set; }
}
