using Project1.Library;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.ViewModels
{
    public class PizzaViewModel
    {
        [Display(Name = "Pizza ID")]
        public int Id { get; set; }

        [Display(Name = "Pizza Name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Display(Name = "Price")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<PizzaIngredient> PizzaIngredients { get; set; }


        public List<PizzaIngredientViewModel> IngredientsAmount { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } //list of all ingredients for choosing during creation


        public PizzaViewModel()
        { }

        public PizzaViewModel(Pizza pizza)
        {
            Id = pizza.Id;
            Name = pizza.Name;
            Price = pizza.Price;

            OrderItems = pizza.OrderItems;
            PizzaIngredients = pizza.PizzaIngredients;
        }

        public bool Checked { get; set; }
    }
}
