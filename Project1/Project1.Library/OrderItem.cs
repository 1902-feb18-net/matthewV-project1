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


        public Pizza Pizza
        {
            get => _pizza;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _pizza = value;
            }
        }
    
        public Order Order
        {
            get => _order;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _order = value;
            }
        }

        public OrderItem(int quantity, Pizza pizza, Order order)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);
            Guard.Against.Null(pizza, nameof(pizza));
            Guard.Against.Null(order, nameof(order));

            Quantity = quantity;
            Pizza = pizza;
            Order = order;
        }
    }
}
