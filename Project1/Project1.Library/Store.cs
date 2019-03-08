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
        List<StoreItem> Inventory { get; set; } = new List<StoreItem>();

        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Store's name not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Store's first name must not be empty.", nameof(value));
                }
                else
                {
                    _name = value;
                }
            }
        }

        public Address Address {
            get => _address;
            set
            {
                _address = value ?? throw new ArgumentNullException(nameof(value), "Store's address must not be null.");
            }
        }


        public Store(string name, Address address)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.Null(address, nameof(address));

            Name = name;
            Address = address;
        }


        ICollection<Order> Orders { get; set; } //the orders to this store. Reverse navigation property
    }
}
