using System.ComponentModel.DataAnnotations;
namespace GDB.Web.Shared
{
    public class InvestmentSummaryViewModel
    {
        public int? InvestmentSummaryId { get; set; }

        [Required(ErrorMessage = "Please select Investment Category.")]
        public int InvestmentOptionCategoryId { get; set; }

        [Required(ErrorMessage = "Please select Investment Sub Category.")]
        public int InvestmentSubCategoryId { get; set; }
        public string Descrpition { get; set; } 
    }
}
