using Ardalis.GuardClauses;
using System;

namespace Project1.Library
{
    public class OrderItem : IDClass
    {
        private int _quantity; //the number of the pizza in the order
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Order item's quantity must not be negative.");
                }
                else if (value == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Order item's quantity must not be zero.");
                }
                else
                {
                    _quantity = value;
                }
            }
        }


        public Pizza Pizza { get; set; }
        public Order Order { get; set; }


        public OrderItem(int quantity, Pizza pizza, Order order)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);
            _quantity = quantity;
            Pizza = pizza;
            Order = order;
        }
    }
}
