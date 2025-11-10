namespace GDB.Web.Shared
{
    public class InvestmentViewModel
    {
        public int InvestmentId { get; set; }
        public int UserId { get; set; } 
        public decimal InvestedAmount { get; set; }
        public DateTime InvestedDate { get; set; }
        public int InvestmentCategoryId { get; set; }
        public int InvestmentSubCategoryId { get; set; }
        public string Descrpition{ get; set; } = string.Empty;
        public string? InvestmentOptionCategoryDescription { get; set; }
        public string? InvestmentSubCategoryDescription { get; set; }

    }
}
