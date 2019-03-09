using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class PizzaIngredient : IDClass
    {
        private int _quantity; //the number of the ingredient in the pizza
        private Pizza _pizza;
        private Ingredient _ingredient;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pizza ingredient's quantity must not be negative.");
                }
                else if (value == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pizza ingredient's quantity must not be zero.");
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

        public Pizza Pizza
        {
            get => _pizza;
            set
            {
                Guard.Against.Null(value, nameof(value));
                _pizza = value;
            }
        }

        public PizzaIngredient(int quantity)
        {
            Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);

            Quantity = quantity;
        }
    }
}
