using Project1.Library;
using System;
using Xunit;

namespace Project1.XUnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public void PizzaStoreCannotAssignNullName()
        {
            //Arrange
            Store newPizzaStore = new Store();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newPizzaStore.Name = null);
        }

        [Fact]
        public void PizzaStoreCannotAssignEmptyName()
        {
            //Arrange
            Store newPizzaStore = new Store();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => newPizzaStore.Name = "");
        }

        [Fact]
        public void PizzaStoreCannotAssignAllWhitespaceName()
        {
            //Arrange
            Store newPizzaStore = new Store();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => newPizzaStore.Name = " ");
        }


        [Fact]
        public void PizzaStoreCannotAssignNullAddress()
        {
            //Arrange
            Store newPizzaStore = new Store();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newPizzaStore.Address = null);
        }



        [Fact]
        public void CustomerCannotAssignNullStore()
        {
            //Arrange
            Customer newCustomer = new Customer();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newCustomer.Store = null);
        }

        [Fact]
        public void CustomerCannotAssignNullFirstName()
        {
            //Arrange
            Customer newCustomer = new Customer();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newCustomer.FirstName = null);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void PizzaStoreCannotAssignEmptyFirstName(string assign)
        {
            //Arrange
            Customer newCustomer = new Customer();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => newCustomer.FirstName = assign);
        }

        [Fact]
        public void CustomerCannotAssignNullLastName()
        {
            //Arrange
            Customer newCustomer = new Customer();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newCustomer.LastName = null);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void PizzaStoreCannotAssignEmptyLastName(string assign)
        {
            //Arrange
            Customer newCustomer = new Customer();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => newCustomer.LastName = assign);
        }


        [Fact]
        public void AddressCannotAssignNullStreet()
        {
            //Arrange
            Address address = new Address();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => address.Street = null);
        }

        [Fact]
        public void AddressCannotAssignEmptyStreet()
        {
            //Arrange
            Address address = new Address();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => address.Street = "");
        }

        [Fact]
        public void AddressCannotAssignAllWhitespaceStreet()
        {
            //Arrange
            Address address = new Address();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => address.Street = " ");
        }


        [Fact]
        public void AddressCannotAssignNullCountry()
        {
            //Arrange
            Address address = new Address();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => address.Country = null);
        }

        [Fact]
        public void AddressCannotAssignEmptyCountry()
        {
            //Arrange
            Address address = new Address();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => address.Country = "");
        }

        [Fact]
        public void AddressCannotAssignAllWhitespaceCountry()
        {
            //Arrange
            Address address = new Address();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => address.Country = " ");
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-1)]
        [InlineData(0)]
        public void PizzaCannotAssignNegativeOrZeroPrice(int amount)
        {
            //Arrange
            Pizza cheesePizza = new Pizza();

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => cheesePizza.Price = amount);
        }


        [Theory]
        [InlineData(-10)]
        [InlineData(-1)]
        [InlineData(0)]
        public void PizzaConstructorCannotPassNegativeOrZeroPrice(int amount)
        {                    
            //Arrange, Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Pizza("cheese", amount));
        }

        [Fact]
        public void PizzaCannotAssignNullName()
        {
            //Arrange
            Pizza newPizza = new Pizza();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newPizza.Name = null);
        }

        [Fact]
        public void PizzaConstructorCannotPassNullName()
        {
            //Arrange, Act and Assert
            Assert.Throws<ArgumentNullException>(() => new Pizza(null, 1));
        }

        [Fact]
        public void IngredientCannotAssignNullName()
        {
            //Arrange
            Ingredient newIngredient = new Ingredient();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => newIngredient.Name = null);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void IngredientCannotAssignEmptyOrWhitespaceLastName(string assign)
        {
            //Arrange
            Ingredient newIngredient = new Ingredient();

            //Act and Assert
            Assert.Throws<ArgumentException>(() => newIngredient.Name = assign);
        }

    }
}
