using GDB.Web.BLL.Implementation;
using GDB.Web.BLL.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace GDB.Web.Client.Pages.MyInvestments
{
    public partial class List : ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IInvestmentService  investmentService{ get; set; }

        public List<InvestmentViewModel> investmentViewModel = null;
        Radzen.DataGridGridLines GridLines = Radzen.DataGridGridLines.Both;
        protected override async Task OnInitializedAsync()
        {
            await GetAllInvestments();
        }

        private async Task<List<InvestmentViewModel>> GetAllInvestments()
        {
            try
            {
                investmentViewModel = await investmentService.GetAllInvestmentDetails();
                if (investmentViewModel == null || !investmentViewModel.Any())
                {
                    return new List<InvestmentViewModel>();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return investmentViewModel?? new List<InvestmentViewModel>();
        }
        private void AddNewInvestment()
        {
            NavigationManager.NavigateTo("/myInvestments/addInvestment");
        }
        void EditRow(InvestmentViewModel investmentViewModel)
        {
            //NavigationManager.NavigateTo("/expenses/updateExpenses" + "/" + Convert.ToString(investmentViewModel.InvestmentId));
        }
    }
}
