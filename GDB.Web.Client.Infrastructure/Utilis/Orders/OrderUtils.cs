using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Client.Infrastructure.Utilis.Orders
{
    public static class OrderUtils
    {
        public static string Order_SuccessMessage = "Order details are added successfully.";

        public static string Order_ErrorMessage = "Unable to create new Orders.";

        public static string Order_Update_SuccessMessage = "Order details are updated successfully.";

        public static string Order_Update_ErrorMessage = "Unable to update Order details.";

        public static string Order_Customer_ValidationMessage = "Customer: Please select Customer.";

        public static string Order_OrderType_ValidationMessage = "Order Type: Please select Order Type.";

        public static string Order_Quantity_ValidationMessage = "Quantity: Please select Quantity.";

        public static string Order_OrderDate_ValidationMessage = "Order Date: Please select Order Date.";

        public static string Order_Update_PaymentType_ValidationMessage = "Payment Type: Please select Payment Type.";
        
        public static string Order_Update_AmountPaidOrNot_ValidationMessage = "Amount Paid or not: Please select Amount Paid or not.";
        
        public static string Order_Update_AmountPaidDate_ValidationMessage = "Amount Paid Date: Please select Amount Paid Date.";
     }
}
