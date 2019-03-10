using Project1.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.ViewModels
{
    public class IngredientViewModel
    {
        [Display(Name = "Ingredient ID")]
        public int Id { get; set; }

        [Display(Name = "Ingredient Name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }


        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }
        public ICollection<StoreItem> StoreItems { get; set; }


        public IngredientViewModel()
        {
        }

        public IngredientViewModel(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
            PizzaIngredients = ingredient.PizzaIngredients;
            StoreItems = ingredient.StoreItems;
        }
    }
}
