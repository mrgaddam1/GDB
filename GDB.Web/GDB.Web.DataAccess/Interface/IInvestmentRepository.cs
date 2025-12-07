using GDB.Web.Shared;

namespace GDB.Web.DataAccess.Interface
{
    public interface IInvestmentRepository
    {
        Task<List<InvestmentOptionCategoryViewModel>> GetAllInvestmentCategories();
        Task<List<InvestmentOptionSubCategoryViewModel>> GetAllInvestmentSubCategories();
        Task<List<InvestmentViewModel>> GetAllInvestmentDetails();
        Task<bool> AddInvestment(InvestmentViewModel investmentViewModel);
        Task<bool> VerifySecurityChecks(SecurityCheckViewModel securityCheckViewModel);
        Task<bool> AddInvestmentSummary(InvestmentSummaryViewModel investmentSummaryViewModel);
    }
}
