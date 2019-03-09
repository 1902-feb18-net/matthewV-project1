using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Library
{
    public class Store : IDClass
    {
        private string _name;
        private Address _address; //Physical location of the store

        public string Name
        {
            get => _name;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _name = value;            
            }
        }

        public Address Address {
            get => _address;
            set
            {
                _address = value ?? throw new ArgumentNullException(nameof(value), "Store's address must not be null.");
            }
        }


        public Store(string name)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        //reverse navigation properties
        public ICollection<Customer> Customers { get; set; } //the customers with this as their default store
        public ICollection<StoreItem> StoreItems { get; set; } //the inventory of this store. 
        public ICollection<Order> Orders { get; set; } //the orders to this store.
    }
}
