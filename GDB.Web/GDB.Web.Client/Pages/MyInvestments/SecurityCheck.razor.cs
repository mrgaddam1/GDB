using Blazored.LocalStorage;
using GDB.Web.BLL.Interface;
using GDB.Web.Common.Helpers;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace GDB.Web.Client.Pages.MyInvestments
{
    public partial class SecurityCheck
    {
        private SecurityCheckViewModel securityCheckViewModel = new SecurityCheckViewModel();
        [Inject] public IInvestmentService investmentService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] ILocalStorageService LocalStorage { get; set; }

        protected bool isSuccess = false;
        protected string? submitMessage;
        private async void HandleValidSubmit()
        {
            var result = await investmentService.VerifySecurityChecks(securityCheckViewModel);
            if (result.SecurityStatus == DbContextUtils.InvestmentDetail_SuccessStatus)
            {
                isSuccess = true;
                await LocalStorage.SetItemAsync("12DigitsPasscode", result.Security12DigitsPasscode);
                NavigationManager.NavigateTo("/myInvestments/investmentDetails/list");
            }
            else if (result.SecurityStatus == DbContextUtils.InvestmentDetail_Data_DoesNot_Exists)
            {
                submitMessage =  DbContextUtils.InvestmentDetail_Data_DoesNot_Exists + " - " + "With this name" + " - " +securityCheckViewModel.FullName;
            }
            else if (result.SecurityStatus == DbContextUtils.InvestmentDetail_PasswordCountExceeds_MoreThanThreeTimes)
            {
                submitMessage = DbContextUtils.InvestmentDetail_PasswordCountExceeds_MoreThanThreeTimes;
            }
            else if (result.SecurityStatus == DbContextUtils.InvestmentDetail_Security_Options_Wrong)
            {
                submitMessage = DbContextUtils.InvestmentDetail_Security_Options_Wrong;
            }
           
        }
    }
}
