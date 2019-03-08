using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class Ingredient
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Ingredient name not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Ingredient name must not be empty.", nameof(value));
                }
                else
                {
                    _name = value;
                }
                //during creation, check for uniqueness?
            }
        }

        public Ingredient(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            _name = name;
        }

        //reverse navigation properties
        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }
        public ICollection<StoreItem> StoreItems { get; set; }
    }
}
