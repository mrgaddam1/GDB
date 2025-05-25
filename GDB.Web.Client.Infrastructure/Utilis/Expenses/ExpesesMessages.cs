using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Client.Infrastructure.Utilis.Expenses
{
    public static class ExpesesMessages
    {
        public static string Expeses_SuccessMessage = "Expenses details are added successfully.";

        public static string Expeses_ErrorMessage = "Unable to create new Expenses.";

        public static string Expenses_Update_SuccessMessage = "Expenses details are updated successfully.";

        public static string Expenses_Update_ErrorMessage = "Unable to update new Expenses.";



        public static string Expenses_Amount_ValidationMessage = "Amount: Please enter Expenses Amount.";

        public static string Expenses_Date_ValidationMessage = "Expenses Date: Please select Expenses Date.";

        public static string Expenses_Store_ValidationMessage = "Store: Please select Store.";

        public static string Expenses_Grocery_ValidationMessage = "Grocery: Please select Grocery.";
    }
}
