using GDB.Web.Shared;

namespace GDB.Web.BLL.Interface
{
    public interface IInvestmentService 
    {
        Task<List<InvestmentOptionCategoryViewModel>>? GetAllInvestmentCategories();
        Task<List<InvestmentOptionSubCategoryViewModel>>? GetAllInvestmentSubCategories();
        Task<List<InvestmentViewModel>> GetAllInvestmentDetails(string passCode);
        Task<bool?> Add(InvestmentViewModel investmentViewModel);
        Task<bool?> Update(InvestmentViewModel investmentViewModel);
        Task<SecurityCheckViewModel> VerifySecurityChecks(SecurityCheckViewModel securityCheckViewModel);
        Task<bool> AddInvestmentSummary(InvestmentSummaryViewModel investmentSummaryViewModel);
    }
}
