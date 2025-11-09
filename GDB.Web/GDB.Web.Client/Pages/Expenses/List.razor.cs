using GDB.Web.BLL.Interface;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GDB.Web.Client.Pages.Expenses
{
    public partial class List : ComponentBase
    {
        [Inject] public IExpensesService ExpensesService { get; set; }
        [Inject] public IOrderService OrderService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        public List<ExpensesViewModel> expensesViewModel = null;
        public int CurrentWeekId { get; set; }

        Radzen.DataGridGridLines GridLines = Radzen.DataGridGridLines.Both;

        protected override async Task OnInitializedAsync()
        {
            await GetAllExpenses();
        }

        private async Task<List<ExpensesViewModel>> GetAllExpenses()
        {
            try
            {              
                expensesViewModel = await ExpensesService.GetAllExpenses();
                if (expensesViewModel == null || !expensesViewModel.Any())
                {
                    return new List<ExpensesViewModel>();
                }
                CurrentWeekId = await OrderService.GetMaxWeekId<int>();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return expensesViewModel ?? new List<ExpensesViewModel>();
        }


        private void AddNewExpenses()
        {
            NavigationManager.NavigateTo("/expenses/add");
        }
        void EditRow(ExpensesViewModel expensesViewModel)
        {
            NavigationManager.NavigateTo("/expenses/updateExpenses" + "/" + Convert.ToString(expensesViewModel.ExpensesId));
        }

        void OnCellRender(DataGridCellRenderEventArgs<ExpensesViewModel> args)
        {
            if (args.Column.Property == "StoreName")
            {
                if (args.Data.StoreName == "Icenland")
                {
                    args.Attributes["style"] = "color:white;font-weight:bold;background-color:deepskyblue;";
                }
                else if (args.Data.StoreName == "Kent Cash and Carry - Near Nissan Local")
                {
                    args.Attributes["style"] = "color:#842029;font-weight:bold;background-color:#d7d1fa !important;";
                }
                else if (args.Data.StoreName == "London Cash and Carry - Chatham")
                {
                    args.Attributes["style"] = "color:black;font-weight:bold;background-color:#d3d9de !important;";
                }
                else if (args.Data.StoreName == "London Cash and Carry - Gillingham")
                {
                    args.Attributes["style"] = "color:#FFFFFF;font-weight:bold;background-color:rgb(255, 165, 0);";
                }
                else if (args.Data.StoreName == "New Season Cash and Carry Near by EE")
                {
                    args.Attributes["style"] = "color:#842029;background-color:#FFFFE0 !important;";
                }
                else if (args.Data.StoreName == "Kent Cash and Carry - Near PostOffice")
                {
                    args.Attributes["style"] = "color:#842029;background-color:lightcoral; !important;";
                }
                else if (args.Data.StoreName == "Nissan Local")
                {
                    args.Attributes["style"] = "color:#842029;background-color:#FF7F7F !important;";
                }
                else if (args.Data.StoreName == "Royal Stores")
                {
                    args.Attributes["style"] = "color:#FFFFFF;font-weight:bold;background-color:lightblue;";
                }
                else if (args.Data.StoreName == "General")
                {
                    args.Attributes["style"] = "color:#FFFFFF;font-weight:bold;background-color:lightsalmon;";
                }
            }
        }
    }
}
