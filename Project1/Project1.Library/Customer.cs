using System;
using System.Collections.Generic;

namespace Project1.Library
{
    public class Customer : IDClass
    {
        private string _firstname;
        private string _lastname;
        public string Email { get; set; } //Nullable.
        public Address Address { get; set; } //Nullable. Value if customer wants to set a default location to deliver order to.

        public string FirstName
        {
            get => _firstname;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Customer's first name not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Customer's first name must not be empty.", nameof(value));
                }
                else
                {
                    _firstname = value;
                }
            }
        }

        public string LastName
        {
            get => _lastname;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Customer's larst name not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Customer's last name must not be empty.", nameof(value));
                }
                else
                {
                    _lastname = value;
                }
            }
        }

        private Store _store; //customer has a default pizza store

        public Store Store
        {
            get => _store;
            set
            {
                _store = value ?? throw new ArgumentNullException(nameof(value), "Customer's store must not be null.");
            }
        }


        public ICollection<Order> Orders { get; set; } //the orders by this customer. Reverse navigation property

    }

}
