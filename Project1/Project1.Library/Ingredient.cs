using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class Ingredient : IDClass
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _name = value;
                //during creation, check for uniqueness?
            }
        }

        public Ingredient()
        { }

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
