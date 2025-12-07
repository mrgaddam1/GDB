using GDB.Web.BLL.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace GDB.Web.Client.Pages.MyInvestments
{
    public partial class SecurityCheck
    {
        private SecurityCheckViewModel securityCheckViewModel = new SecurityCheckViewModel();
        [Inject] public IInvestmentService investmentService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        protected bool isSuccess;
        protected string? submitMessage;
        private async void HandleValidSubmit()
        {
            var result = await investmentService.VerifySecurityChecks(securityCheckViewModel);   
            if(result)
            {

                isSuccess = result;
                NavigationManager.NavigateTo("/myInvestments/investmentDetails/list");
            }
            else
            {
                submitMessage = "We are sorry...! We are not able to process your request.";
            }

        }
    }
}
