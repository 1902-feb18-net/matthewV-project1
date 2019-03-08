using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace Project1.Library
{
    public class Pizza : IDClass
    {
        private string _name;
        private decimal _price; //price for one of this pizza. Must be positive.
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pizza's price must not be negative.");
                }
                else if (value == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Pizza's price must not be zero.");
                }
                else
                {
                    _price = value;
                }
            }
        } 

        public string Name
        {
            get => _name;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value)); //cannot be null, empty or white space
                _name = value;

            }
        }

        public Pizza(string name, decimal price)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Pizza's price must not be negative.");
            }
            else if (price == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Pizza's price must not be zero.");
            }
            else
            {
                Price = price;
            }

            Name = name;
        }


        //reverse navigation properties
        ICollection<OrderItem> OrderItems { get; set; }
        ICollection<PizzaIngredient> Ingredients { get; set; }
    }
}
