using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class Order : IDClass
    {
        public DateTime OrderTime { get; set; }
        public decimal TotalPrice { get; set; }  //calculated from order items' quantity and pizza price upon creation, and then stored
        public Address Address { get; set; } //Nullable. Null value means carry out, otherwise delivered to that address.


        public Store OrderedAt { get; set; }
        public Customer OrderedBy { get; set; }


        public Order(DateTime orderTime, decimal totalPrice, Address address, Store orderdAt, Customer orderedBy)
        {
            OrderTime = orderTime;
            TotalPrice = totalPrice;
            Address = address;
            OrderedAt = orderdAt;
            OrderedBy = orderedBy;
        }


        public ICollection<OrderItem> Items { get; set; } //reverse navigation property

    }
}
