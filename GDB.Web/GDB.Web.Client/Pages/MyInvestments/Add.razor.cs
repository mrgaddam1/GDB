using GDB.Web.BLL.Implementation;
using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Utilis.Expenses;
using GDB.Web.Core.Models;
using GDB.Web.DataAccess.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace GDB.Web.Client.Pages.MyInvestments
{
    public partial class Add : ComponentBase
    {
        [Inject] public IInvestmentService investmentService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }   
        private List<InvestmentOptionCategoryViewModel>?  investmentOptionCategoryViewModel = null;
        private List<InvestmentOptionSubCategoryViewModel>? investmentOptionSubCategoryViewModel = null;
        private InvestmentViewModel investmentViewModel = new InvestmentViewModel();

        private string[] errorMessages;

        private string successMessage = string.Empty;
        public string InvestmentCategoryInitialText { get; set; } = "---Select Investment Category---";
        public string InvestmentSubCategoryInitialText { get; set; } = "---Select Investment Sub Category---";

        protected override async Task OnInitializedAsync()
        {
            investmentViewModel.InvestedDate = DateTime.Now;
            investmentOptionCategoryViewModel = await investmentService.GetAllInvestmentCategories();           
        }

        private async void HandleValidSubmit()
        {
            errorMessages = Validations(investmentViewModel);
            string validations = string.Join(" ", errorMessages);
            if ((validations == null) || (validations == ""))
            {
                var result = await investmentService.Add(investmentViewModel);
                if (result == true)
                {
                    successMessage = "Investment added successfully.";
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
        private void GoBack()
        {
            NavigationManager.NavigateTo("/customers/list");
        }
        private async Task OnInvestmentCategorySelectionIndexChanged(ChangeEventArgs e)
        {
            int selectedInvestmentCategoryId= Convert.ToInt32(e.Value.ToString());
            var data = await investmentService.GetAllInvestmentSubCategories();
            investmentOptionSubCategoryViewModel = data.Where(x => x.InvestmentOptionCategoryId == selectedInvestmentCategoryId).ToList();
            investmentViewModel.InvestmentCategoryId = selectedInvestmentCategoryId;
            StateHasChanged();
        }
        private async Task OnInvestmentSubCategorySelectionIndexChanged(ChangeEventArgs e)
        {
            investmentViewModel.InvestmentSubCategoryId = Convert.ToInt32(e.Value.ToString());
            StateHasChanged();
        }
        private string[] Validations(InvestmentViewModel investmentViewModel)
        {
            string[] result;
            string validationMessage = "";
            if ((investmentViewModel.InvestedAmount == null) || (investmentViewModel.InvestedAmount == 0))
            {
                validationMessage = "Please enter Invested Amount.";
            }
            if (investmentViewModel.InvestedDate == null)
            {
                validationMessage = validationMessage != null ? validationMessage +
                                    ", " + Environment.NewLine + ExpesesMessages.Expenses_Date_ValidationMessage
                                    : ExpesesMessages.Expenses_Date_ValidationMessage;
            }
            if ((investmentViewModel.InvestmentCategoryId == 0) || (investmentViewModel.InvestmentCategoryId == null))
            {
                validationMessage = validationMessage != null
                                    ? validationMessage + ", " + Environment.NewLine + ExpesesMessages.Expenses_Store_ValidationMessage
                                    : ExpesesMessages.Expenses_Store_ValidationMessage;
            }
            if ((investmentViewModel.InvestmentSubCategoryId == 0) || (investmentViewModel.InvestmentSubCategoryId == null))
            {
                validationMessage = validationMessage != null
                                    ? validationMessage + ", " + Environment.NewLine + ExpesesMessages.Expenses_Store_ValidationMessage
                                    : ExpesesMessages.Expenses_Store_ValidationMessage;
            }
            if (investmentViewModel.Descrpition == null)
            {
                validationMessage = validationMessage != null ? validationMessage +
                                    ", " + Environment.NewLine + ExpesesMessages.Expenses_Date_ValidationMessage
                                    : ExpesesMessages.Expenses_Date_ValidationMessage;
            }

            return result = validationMessage.Split(',').Select(s => s.Trim()).ToArray();
        }
   }
}
