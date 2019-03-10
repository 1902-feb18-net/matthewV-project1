using Project1.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.ViewModels
{
    public class StoreViewModel
    {
        [Display(Name = "Store ID")]
        public int Id { get; set; }

        [Display(Name = "Store Name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Display(Name = "Store Address")]
        [Required]
        public Address Address { get; set; }


        public ICollection<Customer> Customers { get; set; } //the customers with this as their default store

        [Display(Name = "Store Inventory")]
        public ICollection<StoreItem> StoreItems { get; set; } //the inventory of this store. 

        [Display(Name = "Store Order History")]
        public ICollection<Order> Orders { get; set; } //the orders to this store.


        public StoreViewModel()
        { }

        public StoreViewModel(Store store)
        {
            Id = store.Id;
            Name = store.Name;
            Address = store.Address;

            Customers = store.Customers;
            StoreItems = store.StoreItems;
            Orders = store.Orders;
        }


        public List<Address> Addresses { get; set; } //list of all addresses for choosing during creation

    }
}
