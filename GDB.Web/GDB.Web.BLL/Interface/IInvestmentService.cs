using GDB.Web.Shared;

namespace GDB.Web.BLL.Interface
{
    public interface IInvestmentService 
    {
        Task<List<InvestmentOptionCategoryViewModel>>? GetAllInvestmentCategories();
        Task<List<InvestmentOptionSubCategoryViewModel>>? GetAllInvestmentSubCategories();
        Task<List<InvestmentViewModel>> GetAllInvestmentDetails();
        Task<bool?> Add(InvestmentViewModel investmentViewModel);

    }
}
