using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;


namespace Project1.Library
{
    public class Order : IDClass
    {
        public DateTime OrderTime { get; set; }
        public decimal TotalPrice { get; set; }  //calculated from order items' quantity and pizza price upon creation, and then stored
        public Address Address { get; set; } //Nullable. Null value means carry out, otherwise delivered to that address.

        private Store _orderedAt;
        private Customer _orderedBy;

        public Store OrderedAt
        {
            get => _orderedAt;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _orderedAt = value;
            }
        }

        public Customer OrderedBy
        {
            get => _orderedBy;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _orderedBy = value;
            }
        }


        public Order() { }

        public Order(DateTime orderTime)
        {
            OrderTime = orderTime;
        }

        public Order(DateTime orderTime, decimal totalPrice)
        {
            OrderTime = orderTime;
            TotalPrice = totalPrice;
        }


        public ICollection<OrderItem> OrderItems { get; set; } //reverse navigation property

    }
}
