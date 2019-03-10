using Project1.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebApp.ViewModels
{
    public class AddressViewModel
    {
        [Display(Name = "Address ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Street { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(255)]
        public string State { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }

        [DataType(DataType.PostalCode)]
        [MaxLength(255)]
        public string Zipcode { get; set; }


        public Store Store { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Order> Orders { get; set; }


        public AddressViewModel() { }

        public AddressViewModel(Address address)
        {
            Id = address.Id;
            Street = address.Street;
            City = address.City;
            State = address.State;
            Country = address.Country;
            Zipcode = address.Zipcode;

            Store = address.Store;
            Customers = address.Customers;
            Orders = address.Orders;
        }

    }
}
