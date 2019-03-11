using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.ViewModels
{
    public class OrderItemViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        public int? Quantity { get; set; }


    }
}
