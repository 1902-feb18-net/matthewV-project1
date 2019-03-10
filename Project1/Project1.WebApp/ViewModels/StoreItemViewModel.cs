using Project1.Library;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.ViewModels
{
    public class StoreItemViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public Store Store { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}
