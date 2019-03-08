using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class PizzaIngredient : IDClass
    {
        private int _quantity; //the number of the ingredient in the pizza
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
        public Pizza Pizza { get; set; }

        public PizzaIngredient(int quantity, Ingredient ingredient, Pizza pizza)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);
            _quantity = quantity;
            Ingredient = ingredient;
            Pizza = pizza;
        }
    }
}
