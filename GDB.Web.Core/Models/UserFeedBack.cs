using System;
using System.Collections.Generic;

namespace GDB.Web.Core.Models;

public partial class UserFeedBack
{
    public int Fid { get; set; }

    public int? UserId { get; set; }

    public string? Feedback { get; set; }
}
