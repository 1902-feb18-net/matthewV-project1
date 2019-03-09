using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class Order : IDClass
    {
        public DateTime OrderTime { get; private set; }
        public decimal TotalPrice { get; set; }  //calculated from order items' quantity and pizza price upon creation, and then stored
        public Address Address { get; private set; } //Nullable. Null value means carry out, otherwise delivered to that address.

        private Store _orderedAt;
        private Customer _orderedBy;

        public Store OrderedAt
        {
            get => _orderedAt;
            private set
            {
                Guard.Against.Null(value, nameof(value));
                _orderedAt = value;
            }
        }

        public Customer OrderedBy 
        {
            get => _orderedBy;
            private set
            {
                Guard.Against.Null(value, nameof(value));
                _orderedBy = value;
            }
        }


        public Order(DateTime orderTime, decimal totalPrice)
        {
            OrderTime = orderTime;
            TotalPrice = totalPrice;            
        }


        public ICollection<OrderItem> OrderItems { get; set; } //reverse navigation property

    }
}
