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
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Street must not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Street must not be empty.", nameof(value));
                }
                else
                {
                    _street = value;
                }
            }
        }

        public string City { get; set; } //Nullable for rural areas?

        public string State { get; set; } //Nullable (Non US country)

        public string Country
        {
            get => _country;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Country must not be null.");
                }
                else if (value.Length == 0)
                {
                    throw new ArgumentException("Country must not be empty.", nameof(value));
                }
                else
                {
                    _country = value;
                }
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


    }
}
