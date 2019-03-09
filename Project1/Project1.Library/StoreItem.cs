using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class StoreItem : IDClass
    {
        private int _quantity; //the number of the ingredient in the store's inventory
        private Ingredient _ingredient;
        private Store _store;

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

        public Ingredient Ingredient
        {
            get => _ingredient;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _ingredient = value;
            }
        }

        public Store Store
        {
            get => _store;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _store = value;
            }
        }


        public StoreItem(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);

            Quantity = quantity;
        }
    }
}
