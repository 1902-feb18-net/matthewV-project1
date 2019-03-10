using Project1.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.ViewModels
{
    public class PizzaIngredientViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Ingredient Amount")]
        public int Quantity { get; set; }


        public Pizza Pizza { get; set; }
        public Ingredient Ingredient { get; set; }


    }
}
