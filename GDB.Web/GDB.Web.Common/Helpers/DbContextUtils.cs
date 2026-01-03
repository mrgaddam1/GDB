using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Common.Helpers
{
    public static class DbContextUtils
    {

        //investment detail
        public const string InvestmentDetail_Data_DoesNot_Exists = "Security check failed: no security data found.";

        public const string InvestmentDetail_PasswordCountExceeds_MoreThanThreeTimes = "Security check failed: maximum password attempts exceeded.";

        public const string InvestmentDetail_Security_Options_Wrong = "One or more security options (mobile number or 12‑digit code, or 6‑digit code) failed verification.";

        public const string InvestmentDetail_SuccessStatus = "Security check passed successfully.";

    }

}

