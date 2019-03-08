using Ardalis.GuardClauses;
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


        public Order(DateTime orderTime, decimal totalPrice, Address address, Store orderedAt, Customer orderedBy)
        {
            Guard.Against.Null(orderedBy, nameof(orderedBy));
            Guard.Against.Null(orderedAt, nameof(orderedAt));

            OrderTime = orderTime;
            TotalPrice = totalPrice;
            Address = address;
            OrderedAt = orderedAt;
            OrderedBy = orderedBy;
            
        }


        public ICollection<OrderItem> Items { get; set; } //reverse navigation property

    }
}
