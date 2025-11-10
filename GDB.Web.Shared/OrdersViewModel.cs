using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDB.Web.Shared
{
    public class OrdersViewModel
    {   
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNumber { get; set; }
        public string? OrderTypeName { get; set; }
        public string? Location { get; set; }
        public string? AdvertisementDescription { get; set; }       
        public decimal Amount { get; set; }
        public bool? AmountPaid { get; set; }
        public DateTime? AmountPaidDate { get; set; }
        public int? StaterId { get; set; }
        public decimal? SelectedStaterPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderTypeId { get; set; }      
        public int Quantity{ get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? PaymentType { get; set; }
        public string? StarterDescription { get; set; }
        public int WeekId { get; set; }
        public string? CustomerName { get; set; }
        public bool? StaterSelectedOrNot { get; set; }
        public decimal? TotalStaterPrice { get; set; }
        public int FoodPackingTypeId { get; set; }
        public string? FoodPackingTypeDescription { get; set; }
        public int? StaterQuantity { get; set; }
    }
}
