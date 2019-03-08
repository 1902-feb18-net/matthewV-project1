using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class StoreItem : IDClass
    {
        private int _quantity; //the number of the ingredient in the store's inventory
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



        public Ingredient Ingredient { get; set; }
        public Store Store { get; set; }
    }
}
