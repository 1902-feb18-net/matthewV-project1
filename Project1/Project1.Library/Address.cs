using Ardalis.GuardClauses;
using System;

namespace Project1.Library
{
    public class Address
    {
        private string _street;
        private string _country;
        private string _zipcode;

        public string Street
        {
            get => _street;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _street = value;
               
            }
        }

        public string City { get; set; } //Nullable for rural areas?

        public string State { get; set; } //Nullable (Non US country)

        public string Country
        {
            get => _country;
            set
            {
                Guard.Against.NullOrWhiteSpace(value, nameof(value));
                _country = value;
               
            }
        }

        public string Zipcode
        {
            get => _zipcode;
            set
            {
                //validation for zipcode values?

                _zipcode = value;
            }
        } 


        public Address(string street, string city, string state, string country, string zipcode)
        {
            Guard.Against.NullOrWhiteSpace(street, nameof(street));
            Guard.Against.NullOrWhiteSpace(country, nameof(country));

            Street = street;
            City = city;
            State = state;
            Country = country;
            Zipcode = zipcode;

        }


    }
}
