using GDB.Web.BLL.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace GDB.Web.Client.Pages.MyInvestments.InvestmentSummary
{
    public partial class AddInvestmentSummary : ComponentBase
    {
        [Inject] public IInvestmentService? investmentService { get; set; }
        [Inject] public NavigationManager? NavigationManager { get; set; }

        private List<InvestmentOptionCategoryViewModel>? investmentOptionCategoryViewModel = null;

        private List<InvestmentOptionSubCategoryViewModel>? investmentOptionSubCategoryViewModel = null;

        public string InvestmentCategoryInitialText { get; set; } = "---Select Investment Category---";
        public string InvestmentSubCategoryInitialText { get; set; } = "---Select Investment Sub Category---";

        private string successMessage = string.Empty;

        private string[] errorMessages;

        InvestmentSummaryViewModel investmentSummaryViewModel = new InvestmentSummaryViewModel();


        protected override async Task OnInitializedAsync()
        {
            investmentOptionCategoryViewModel = await investmentService.GetAllInvestmentCategories();
        }
        private void GoBack()
        {
            NavigationManager.NavigateTo("/my-investments/list");
        }
        private async Task OnInvestmentCategorySelectionIndexChanged(ChangeEventArgs e)
        {
            int selectedInvestmentCategoryId = Convert.ToInt32(e.Value.ToString());
            var data = await investmentService.GetAllInvestmentSubCategories();
            investmentOptionSubCategoryViewModel = data.Where(x => x.InvestmentOptionCategoryId == selectedInvestmentCategoryId).ToList();
            investmentSummaryViewModel.InvestmentOptionCategoryId = selectedInvestmentCategoryId;
            StateHasChanged();
        }
        private async Task OnInvestmentSubCategorySelectionIndexChanged(ChangeEventArgs e)
        {
            investmentSummaryViewModel.InvestmentSubCategoryId = Convert.ToInt32(e.Value.ToString());
            StateHasChanged();
        }
        private async void HandleValidSubmit()
        {
        
              var result = await investmentService.AddInvestmentSummary(investmentSummaryViewModel);
                if (result == true)
                {
                    successMessage = "Investment Summary added successfully.";
                    StateHasChanged();
                    await Task.Delay(2000);
                    //GoBack();
                }
                else
                {
                    errorMessages = new string[] { "An error occurred while adding the investment. Please try again." };
                    StateHasChanged();
                }
            }
        }
    }

