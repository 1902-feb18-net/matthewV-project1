using Ardalis.GuardClauses;
using System;

namespace Project1.Library
{
    public class OrderItem : IDClass
    {
        private int _quantity; //the number of the pizza in the order
        private Pizza _pizza;
        private Order _order;

        public int Quantity
        {
            get => _quantity;
            private set
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


        public Pizza Pizza
        {
            get => _pizza;
            private set
            {
                Guard.Against.Null(value, nameof(value));
                _pizza = value;
            }
        }
    
        public Order Order
        {
            get => _order;
            private set
            {
                Guard.Against.Null(value, nameof(value));
                _order = value;
            }
        }

        public OrderItem(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);

            Quantity = quantity;
        }
    }
}
