using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class Pizza : IDClass
    {
        private string _name; 
        public decimal Price { get; set; } //price for one of this pizza

        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Pizza's name not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Pizza's name must not be empty.", nameof(value));
                }
                else
                {
                    _name = value;
                }
            }
        }

        //reverse navigation properties
        ICollection<OrderItem> OrderItems { get; set; }
        ICollection<PizzaIngredient> Ingredients { get; set; } 
    }
}
