using Project1.Library;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebApp.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(255)]
        public string Email { get; set; }

        [Display(Name = "Default Delivery Address")]
        public Address Address { get; set; }

        [Display(Name = "Customer's Default Store")]
        [Required]
        public Store Store { get; set; }


        public ICollection<Order> Orders { get; set; }


        public CustomerViewModel()
        { }

        public CustomerViewModel(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            Address = customer.Address;
            Store = customer.Store;

            Orders = customer.Orders;
        }

        //List of all address and stores for choosing defaults during creation
        public List<Address> Addresses { get; set; }
        public List<Store> Stores { get; set; }
    }
}
