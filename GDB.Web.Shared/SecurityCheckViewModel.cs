using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace GDB.Web.Shared
{
    public class SecurityCheckViewModel
    {
      
        [StringLength(50, MinimumLength = 16, ErrorMessage = "Full Name must be between 16 and 50 characters.")]
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Mobile Number must be between 12 and 12 Numbers.")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        public string MobileNumber { get; set; }

        [StringLength(12, MinimumLength = 12, 
         ErrorMessage = "Security 12 Digits Passcode must be 12 charcters long.")]
        [Required(ErrorMessage = "Security 12 Digits Passcode is required.")]
        public string Security12DigitsPasscode { get; set; }


        [StringLength(6, MinimumLength = 6, ErrorMessage = "Security 6 Digits Pincode must be 6 charcters long.")]
        [Required(ErrorMessage = "Security 6 Digits Pincode is required.")]
        public string Security6DigitsPincode { get; set; }
 
        public string? SecurityStatus { get; set; }
    }
}
