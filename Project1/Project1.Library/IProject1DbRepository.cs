using System.Collections.Generic;
using Project1.Library;

namespace Project1.Library
{
    public interface IProject1DbRepository
    {
        void Add(Address address);
        void Add(Customer customer);
        void Add(Ingredient obj);
        void Add(Order order);
        void Add(OrderItem obj);
        void Add(Pizza pizza);
        void Add(PizzaIngredient PizzaIngredient);
        void Add(Store obj);
        void Add(StoreItem StoreItem);
        Address GetAddressById(int id);
        IEnumerable<Address> GetAllAddresses();
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Ingredient> GetAllIngredients();
        IEnumerable<Ingredient> GetAllIngredientsWithPizzas();
        IEnumerable<Ingredient> GetAllIngredientsWithStores();
        IEnumerable<OrderItem> GetAllOrderItems();
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersWithPizzas();
        IEnumerable<PizzaIngredient> GetAllPizzaIngredients();
        IEnumerable<Pizza> GetAllPizzas();
        IEnumerable<Pizza> GetAllPizzasWithIngredients();
        IEnumerable<Pizza> GetAllPizzasWithOrders();
        IEnumerable<StoreItem> GetAllStoreItems();
        IEnumerable<Store> GetAllStores();
        IEnumerable<Store> GetAllStoresWithIngredients();
        Customer GetCustomerById(int id);
        Customer GetCustomerWithAddressById(int id);
        Customer GetCustomerWithDetailsById(int id);
        IEnumerable<Customer> GetCustomersByFirstName(string name);
        IEnumerable<Customer> GetCustomersByFullName(string name);
        Ingredient GetIngredientById(int id);
        Order GetOrderById(int id);
        OrderItem GetOrderItemById(int id);
        Order GetOrderWithDetailsById(int id);
        IEnumerable<Order> GetOrderOfCustomer(Customer cust);
        IEnumerable<Order> GetOrderOfCustomer(int custId);
        IEnumerable<Order> GetOrderOfLocation(int storeId);
        IEnumerable<Order> GetOrderOfLocation(Store store);
        IEnumerable<Order> GetOrdersSortedCheapest();
        IEnumerable<Order> GetOrdersSortedEarliest();
        IEnumerable<Order> GetOrdersSortedExpensive();
        IEnumerable<Order> GetOrdersSortedLatest();
        Pizza GetPizzaById(int id);
        PizzaIngredient GetPizzaIngredientById(int id);
        Store GetStoreById(int id);
        Store GetStoreWithAddressById(int id);
        Store GetStoreWithDetailsById(int id);
        StoreItem GetStoreItemById(int id);
        IEnumerable<Store> GetStoresByName(string name);
        Order SuggestOrder(int custId);
        void Update(Address obj);
        void Update(Customer customer);
        void Update(Store store);
        void Update(StoreItem obj);
        void Update(Pizza obj);
    }
}