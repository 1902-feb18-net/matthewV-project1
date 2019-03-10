using Project1.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Order ID")]
        public int Id { get; set; }

        [Display(Name = "Order Time")]
        [DataType(DataType.DateTime)]
        public DateTime OrderTime { get; set; }

        [Display(Name = "Total Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Store")]
        [Required]
        public Store OrderedAt { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public Customer OrderedBy { get; set; }

        [Display(Name = "Delivery Address")]
        public Address Address { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }


        public OrderViewModel(Order order)
        {
            Id = order.Id;
            OrderTime = order.OrderTime;
            TotalPrice = order.TotalPrice;
            OrderedAt = order.OrderedAt;
            OrderedBy = OrderedBy;
            Address = order.Address;

            OrderItems = order.OrderItems;
        }
    }
}
