using GDB.Web.BLL.Interface;
using GDB.Web.Client.Infrastructure.Utilis.Orders;
using GDB.Web.Shared;
using Microsoft.AspNetCore.Components;

 
namespace GDB.Web.Client.Pages.Orders;

public partial class AddOrder : ComponentBase
{
    [Inject] public ICustomerService? CustomerService { get; set; }
    [Inject] public IOrdersTypeService OrdersTypeService { get; set; }
    [Inject] public IStaterService StaterService { get; set; }
    [Inject] public IOrderService orderService { get; set; }
    [Inject] public IPaymentTypeService PaymentTypeService { get; set; }
    [Inject] public HttpClient HttpClient { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    //[Inject] public ILogger Logger { get; set; }

    private bool isChecked = false;

    private bool checkAmountPaidOrNot
    {
        get => order.AmountPaid ?? false; // Default to false if null
        set => order.AmountPaid = value;  // Update original property
    }

    private bool checkStaterSelectedOrNot
    {
        get => order.StaterSelectedOrNot ?? false; // Default to false if null
        set => order.StaterSelectedOrNot = value;  // Update original property
    }

    private OrdersViewModel order = new();
    public string[] errorMessages;
    private string successMessage = string.Empty;

    public string CustomerInitialText { get; set; } = "---Select Customer---";
    public string StatersInitialText { get; set; } = "---Select Starter---";
    public string StaterQuantityInitialText { get; set; } = "---Select Quantity Type---";
    public string OrderTypesInitialText { get; set; } = "---Select Order Type---";
    public string QuantityInitialText { get; set; } = "---Select Quantity---";
    public string PaymentTypeInitialText { get; set; } = "---Select Payment Type---";

    public IEnumerable<CustomerViewModel> customersViewModelData;
    public IEnumerable<OrderTypesViewModel> orderTypesViewModelData;
    public IEnumerable<StatersViewModel> statersViewModelData;
    public IEnumerable<PaymentTypeViewModel> paymentTypeViewModel;
    public List<StaterQuantityViewModel> staterQuantitylist = new List<StaterQuantityViewModel>();

    protected override async Task OnInitializedAsync()
    {
        await BindData();
    }
    private async Task BindData()
    {
        customersViewModelData = await CustomerService.GetAllCustomers<List<CustomerViewModel>>();
        orderTypesViewModelData = await OrdersTypeService.GetAllOrderTypes<List<OrderTypesViewModel>>();
        statersViewModelData = await StaterService.GetAllStaters<List<StatersViewModel>>();
        paymentTypeViewModel = await PaymentTypeService.GetAllPaymentTypes<List<PaymentTypeViewModel>>();
        order.WeekId = await orderService.GetMaxWeekId<int>();
        GetStaterQuantityDetails();
    }

    private async void HandleValidSubmit()
    {
        successMessage = "";
        errorMessages = Validations(order);
        string validations = string.Join(" ", errorMessages);
        if ((validations == null) || (validations == ""))
        {
            order.Amount = order.TotalPrice;
            var response = await orderService.Add(order);
            if (response.Value)
            {
                Success();
                await BindData();
            }
            else
            {
                Error();
            }
        }
        validations = "";
    }
    private void Success()
    {
        //Logger.LogInformation(OrderUtils.Order_SuccessMessage);
        successMessage = OrderUtils.Order_SuccessMessage;
        Reset();
    }
    private void Error()
    {
        //Logger.LogInformation(OrderUtils.Order_ErrorMessage);
        successMessage = OrderUtils.Order_ErrorMessage;
    }

    private async Task OnStaterSelectionIndexChanged(ChangeEventArgs e)
    {
        order.StaterId = Convert.ToInt32(e.Value.ToString());
        order.SelectedStaterPrice = await StaterService.GetStaterPriceByStater(Convert.ToInt32(e.Value.ToString()));
        StateHasChanged();
    }
    private async Task OnCustomerIndexChanged(ChangeEventArgs e)
    {
        order.CustomerId = Convert.ToInt32(e.Value.ToString());
        StateHasChanged();
    }

    private async Task OnOrderTypeSelectionIndexChanged(ChangeEventArgs e)
    {
        order.OrderTypeId = Convert.ToInt32(e.Value.ToString());
        order.Amount = await OrdersTypeService.GetOrderTypePriceByOrderType(Convert.ToInt32(e.Value.ToString()));
        var orderTypesData = await OrdersTypeService.GetAllOrderTypes<List<OrderTypesViewModel>>();
        if (orderTypesData != null)
        {
            var orderType = orderTypesData.FirstOrDefault(x => x.OrderTypeId == order.OrderTypeId);
            if (orderType != null)
            {
                order.FoodPackingTypeId = orderType.FoodPackingTypeId;
            }
        }
        StateHasChanged();
    }

    private async Task OnStaterQuantitySelectionIndexChanged(ChangeEventArgs e)
    {
        order.TotalStaterPrice = Convert.ToDecimal(order.SelectedStaterPrice) *
                                    Convert.ToDecimal(e.Value.ToString());
        order.StaterQuantity = Convert.ToInt32(e.Value.ToString());
        StateHasChanged();
    }

    private async Task OnPaymentTypeSelectionIndexChanged(ChangeEventArgs e)
    {
        order.PaymentTypeId = Convert.ToInt32(e.Value.ToString());
        StateHasChanged();
    }
    private async Task OnQuantitySelectionIndexChanged(ChangeEventArgs e)
    {
        order.Quantity = Convert.ToInt32(e.Value.ToString());
        order.TotalPrice = (Convert.ToDecimal(e.Value.ToString()) *
                            Convert.ToDecimal(order.Amount) + Convert.ToDecimal(order.TotalStaterPrice == null ? 0 : order.TotalStaterPrice)
                            );


        StateHasChanged();
    }

    private async void Reset()
    {
        order = new OrdersViewModel();
        order.WeekId = await orderService.GetMaxWeekId<int>();
        StateHasChanged();
    }
    private string[] Validations(OrdersViewModel ordersViewModel)
    {
        string[] result;
        string validationMessage = "";

        if (ordersViewModel.CustomerId == 0)
        {
            validationMessage = OrderUtils.Order_Customer_ValidationMessage;
        }
        if ((ordersViewModel.OrderTypeId == 0) || (ordersViewModel.OrderTypeId == null))
        {
            validationMessage = validationMessage != null ? validationMessage +
                                ", " + Environment.NewLine + OrderUtils.Order_OrderType_ValidationMessage
                                : OrderUtils.Order_OrderType_ValidationMessage;
        }
        if ((ordersViewModel.OrderTypeId != 0) || (ordersViewModel.OrderTypeId != null))
        {
            if (ordersViewModel.Amount == 0 || ordersViewModel.Amount == null)
            {
                validationMessage = validationMessage != null ? validationMessage +
                                    ", " + Environment.NewLine + "Plese select valid Order Type to get Amount"
                                    : "Plese select valid Order Type to get Amount";
            }
        }

        if ((ordersViewModel.Quantity == 0) || (ordersViewModel.Quantity == null))
        {
            validationMessage = validationMessage != null
                                ? validationMessage + ", " + Environment.NewLine + OrderUtils.Order_Quantity_ValidationMessage
                                : OrderUtils.Order_Quantity_ValidationMessage;
        }
        if (ordersViewModel.OrderDate == null)
        {
            validationMessage = validationMessage != null
                                ? validationMessage + ", " + Environment.NewLine + OrderUtils.Order_OrderDate_ValidationMessage
                                : OrderUtils.Order_OrderDate_ValidationMessage;
        }
        return result = validationMessage.Split(',').Select(s => s.Trim()).ToArray();
    }

    protected void GetStaterQuantityDetails()
    {

        var staterQuantity = new StaterQuantityViewModel
        {
            StaterQuantityId = 1,
            StaterQuantity = 1
        };
        staterQuantitylist.Add(staterQuantity);
        var staterQuantity1 = new StaterQuantityViewModel
        {
            StaterQuantityId = 2,
            StaterQuantity = 2
        };

        staterQuantitylist.Add(staterQuantity1);
        var staterQuantity2 = new StaterQuantityViewModel
        {
            StaterQuantityId = 3,
            StaterQuantity = 3
        };
        staterQuantitylist.Add(staterQuantity2);
        var staterQuantity3 = new StaterQuantityViewModel
        {
            StaterQuantityId = 4,
            StaterQuantity = 4
        };
        staterQuantitylist.Add(staterQuantity3);
        var staterQuantity4 = new StaterQuantityViewModel
        {
            StaterQuantityId = 5,
            StaterQuantity = 5
        };
        staterQuantitylist.Add(staterQuantity4);
        var staterQuantity5 = new StaterQuantityViewModel
        {
            StaterQuantityId = 6,
            StaterQuantity = 10
        };
        staterQuantitylist.Add(staterQuantity5);
        var staterQuantity6 = new StaterQuantityViewModel
        {
            StaterQuantityId = 7,
            StaterQuantity = 15
        };
        staterQuantitylist.Add(staterQuantity6);
        var staterQuantity7 = new StaterQuantityViewModel
        {
            StaterQuantityId = 8,
            StaterQuantity = 20
        };
        staterQuantitylist.Add(staterQuantity7);
        var staterQuantity8 = new StaterQuantityViewModel
        {
            StaterQuantityId = 9,
            StaterQuantity = 25
        };
        staterQuantitylist.Add(staterQuantity8);
        var staterQuantity9 = new StaterQuantityViewModel
        {
            StaterQuantityId = 10,
            StaterQuantity = 30
        };
        staterQuantitylist.Add(staterQuantity9);
    }

    public class StaterQuantityViewModel
    {
        public int StaterQuantityId { get; set; }
        public int StaterQuantity { get; set; }
    }

    private void GoBack()
    {
        NavigationManager.NavigateTo("/orders/ordersList");
    }

}
