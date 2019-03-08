using Microsoft.EntityFrameworkCore;
using Project1.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.DataAccess
{
    public class Project1DbRepository : IRepository
    {
        private readonly Project1DbContext _dbContext;

        public Project1DbRepository(Project1DbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return _dbContext.Address.ToList();
        }

        public Address GetAddressById(int id)
        {
            return _dbContext.Address.Find(id); //will return null if obj with id is not in db
        }

        public void Add(Address address)
        {
            if (address is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null address");
            }
            else
            {
                if (GetAddressById(address.Id) != null) //if given address is already in db
                {
                    throw new ArgumentOutOfRangeException("Address with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.Address.Add(address); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _dbContext.Customer.ToList(); 
        }


        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customer.Find(id);
        }

        public IEnumerable<Customer> GetCustomersByFirstName(string name)
        {
            if (name is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot search null First Name");
            }
            else
            {
                return _dbContext.Customer.Where(cust => cust.FirstName == name).ToList();
            }
        }

        public IEnumerable<Customer> GetCustomersByFullName(string name)
        {
            if (name is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot search null Full Name");
            }
            else
            {
                return _dbContext.Customer.Where(cust => (cust.FirstName + " " + cust.LastName) == name).ToList();
            }

        }

        public void Add(Customer customer)
        {
            if (customer is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null customer");
            }
            else
            {
                if (GetCustomerById(customer.Id) != null) //if given customer is already in db
                {
                    throw new ArgumentOutOfRangeException("Customer with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.Customer.Add(customer); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return _dbContext.Ingredient.ToList();
        }

        public IEnumerable<Ingredient> GetAllIngredientsWithStores()
        {
            return _dbContext.Ingredient.Include(i => i.StoreItems).ThenInclude(si => si.Store).ToList();
        }

        public IEnumerable<Ingredient> GetAllIngredientsWithPizzas()
        {
            return _dbContext.Ingredient.Include(i => i.PizzaIngredients).ThenInclude(pi => pi.Pizza).ToList();
        }


        public Ingredient GetIngredientById(int id)
        {
            return _dbContext.Ingredient.Find(id); 
        }

        public void Add(Ingredient obj)
        {
            if (obj is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null Ingredient");
            }
            else
            {
                if (GetIngredientById(obj.Id) != null) //if given ingredient is already in db
                {
                    throw new ArgumentOutOfRangeException("Ingredient with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.Ingredient.Add(obj); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        
        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Order.ToList(); 
        }

        public IEnumerable<Order> GetAllOrdersWithPizzas()
        {
            return _dbContext.Order.Include(o => o.OrderItems).ThenInclude(oi => oi.Pizza).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _dbContext.Order.Find(id);
        }

        public IEnumerable<Order> GetOrderOfCustomer(int custId)
        {
            return _dbContext.Order.Where(ord => ord.OrderedBy.Id == custId);
        }

        public IEnumerable<Order> GetOrderOfCustomer(Customer cust)
        {
            return _dbContext.Order.Where(ord => ord.OrderedBy.Id == cust.Id);
        }

        public IEnumerable<Order> GetOrderOfLocation(int storeId)
        {
            return _dbContext.Order.Where(ord => ord.OrderedAt.Id == storeId);
        }

        public IEnumerable<Order> GetOrderOfLocation(Store store)
        {
            return _dbContext.Order.Where(ord => ord.OrderedAt.Id == store.Id);
        }

        public IEnumerable<Order> GetOrdersSortedEarliest()
        {
            return _dbContext.Order.OrderBy(o => o.OrderTime);
        }

        public IEnumerable<Order> GetOrdersSortedLatest()
        {
            return _dbContext.Order.OrderByDescending(o => o.OrderTime);
        }

        public IEnumerable<Order> GetOrdersSortedExpensive()
        {
            return _dbContext.Order.OrderByDescending(o => o.TotalPrice);            
        }

        public IEnumerable<Order> GetOrdersSortedCheapest()
        {
            return _dbContext.Order.OrderBy(o => o.TotalPrice);
        }
       
        //public  GetOrdersStatistics()
        //{
        //    throw new NotImplementedException();
        //}

        public Order SuggestOrder(int custId)
        {
            throw new NotImplementedException();
        }


        public void Add(Order order)
        {
            if (order is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null order");
            }
            else
            {
                if (GetOrderById(order.Id) != null) //if given order is already in db
                {
                    throw new ArgumentOutOfRangeException("Order with given id already exists.");
                }
                else
                {
                    try
                    {
                        //need to check 2hr time constraint for this customer at this store
                        foreach (var orders in GetAllOrders().Where(o => o.OrderedBy == order.OrderedBy & o.OrderedAt == order.OrderedAt).ToList())
                        {
                            if (order.OrderTime.Subtract(orders.OrderTime) <= new TimeSpan(2, 0, 0))
                            {
                                //log it

                                throw new Exception("A customer cannot place another order at the same store within two hours.");
                            }
                        }


                        _dbContext.Order.Add(order); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            return _dbContext.OrderItem.ToList();
        }

        public OrderItem GetOrderItemById(int id)
        {
            return _dbContext.OrderItem.Find(id); 
        }


        public void Add(OrderItem obj)
        {
            if (obj is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null Order Item");
            }
            else
            {
                if (GetOrderItemById(obj.Id) != null) //if given Order Item is already in db
                {
                    throw new ArgumentOutOfRangeException("Order Item with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.OrderItem.Add(obj); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }     


        public IEnumerable<Pizza> GetAllPizzas()
        {
            return _dbContext.Pizza.ToList(); 
        }

        public IEnumerable<Pizza> GetAllPizzasWithIngredients()
        {
            return _dbContext.Pizza.Include(p => p.PizzaIngredients).ThenInclude(pi => pi.Ingredient).ToList();
        }

        public IEnumerable<Pizza> GetAllPizzasWithOrders()
        {
            return _dbContext.Pizza.Include(p => p.OrderItems).ThenInclude(oi => oi.Order).ToList();
        }

        public Pizza GetPizzaById(int id)
        {
            return _dbContext.Pizza.Find(id);
        }


        public void Add(Pizza pizza)
        {
            if (pizza is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null pizza");
            }
            else
            {
                if (GetPizzaById(pizza.Id) != null) //if given pizza is already in db
                {
                    throw new ArgumentOutOfRangeException("Pizza with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.Pizza.Add(pizza); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public IEnumerable<PizzaIngredient> GetAllPizzaIngredients()
        {
            return _dbContext.PizzaIngredient.ToList();
        }

        public PizzaIngredient GetPizzaIngredientById(int id)
        {
            return _dbContext.PizzaIngredient.Find(id);
        }

        public void Add(PizzaIngredient PizzaIngredient)
        {
            if (PizzaIngredient is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null Pizza Ingredient");
            }
            else
            {
                if (GetPizzaIngredientById(PizzaIngredient.Id) != null) //if given Pizza Ingredient is already in db
                {
                    throw new ArgumentOutOfRangeException("Pizza Ingredient with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.PizzaIngredient.Add(PizzaIngredient); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.savechanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _dbContext.Store.ToList();
        }

        public IEnumerable<Store> GetAllStoresWithIngredients()
        {
            return _dbContext.Store.Include(o => o.StoreItems).ThenInclude(si => si.Ingredient).ToList();
        }

        public Store GetStoreById(int id)
        {
            return _dbContext.Store.Find(id);
        }

        public IEnumerable<Store> GetStoresByName(string name)
        {
            if (name is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot search null store name");
            }
            else
            {
                return _dbContext.Store.Where(store => store.Name == name).ToList();
            }
        }

        public void Add(Store obj)
        {
            if (obj is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null store");
            }
            else
            {
                if (GetStoreById(obj.Id) != null) //if given store is already in db
                {
                    throw new ArgumentOutOfRangeException("Store with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.Store.Add(obj); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public IEnumerable<StoreItem> GetAllStoreItems()
        {
            return _dbContext.StoreItem.ToList();
        }


        public StoreItem GetStoreItemById(int id)
        {
            return _dbContext.StoreItem.Find(id);
        }

        public void Add(StoreItem StoreItem)
        {
            if (StoreItem is null)
            {
                //log it!

                throw new ArgumentNullException("Cannot add null Store Item");
            }
            else
            {
                if (GetStoreItemById(StoreItem.Id) != null) //if given Store Item is already in db
                {
                    throw new ArgumentOutOfRangeException("Store Item with given id already exists.");
                }
                else
                {
                    try
                    {
                        _dbContext.StoreItem.Add(StoreItem); //add to local _dbContext
                        _dbContext.SaveChanges();  //run _dbContext.SaveChanges() to run the appropriate insert, adding it to db
                    }
                    catch (DbUpdateException)
                    {
                        //log it!

                        throw;
                    }
                }
            }
        }

        public void Update(Address obj) //address should be updatable
        {
            if (obj is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot update null address");
            }
            else
            {
                var existing = GetAddressById(obj.Id);
                if (existing != null) //if given address is actually in db
                {
                    //update local values
                    _dbContext.Entry(existing).CurrentValues.SetValues(obj);

                    _dbContext.SaveChanges(); //update db's values
                }
                else
                {
                    //log it!
                    throw new ArgumentOutOfRangeException("Address with given id does not exist");
                }
            }
        }

        public void Update(Customer customer) //customer information should be updatable 
        {
            if (customer is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot update null customer");
            }
            else
            {
                var existingCust = GetCustomerById(customer.Id);
                if (existingCust != null) //if given customer is actually in db
                {
                    //update local values
                    _dbContext.Entry(existingCust).CurrentValues.SetValues(customer);


                    _dbContext.SaveChanges(); //update db's values
                }
                else
                {
                    //log it!
                    throw new ArgumentOutOfRangeException("Customer with given id does not exist");
                }
            }
        }

        public void Update(Store store) //store information should be updatable
        {
            if (store is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot update null store");
            }
            else
            {
                var existingStore = GetStoreById(store.Id);
                if (existingStore != null) //if given store is actually in db
                {
                    //update local values
                    _dbContext.Entry(existingStore).CurrentValues.SetValues(store);

                    _dbContext.SaveChanges(); //update db's values
                }
                else
                {
                    //log it!
                    throw new ArgumentOutOfRangeException("Store with given id does not exist");
                }
            }
        }

        public void Update(StoreItem obj) //store item (inventory) should be updatable
        {
            if (obj is null)
            {
                //log it!
                throw new ArgumentNullException("Cannot update null store item");
            }
            else
            {
                var existing = GetStoreItemById(obj.Id);
                if (existing != null) //if given pizza is actually in db
                {
                    //update local values
                    _dbContext.Entry(existing).CurrentValues.SetValues(obj);

                    _dbContext.SaveChanges(); //update db's values
                }
                else
                {
                    //log it!
                    throw new ArgumentOutOfRangeException("Store item with given id does not exist");
                }
            }
        }

        //no updates for order, orderitem, pizza, pizzaingredient, or ingredient to prevent rewriting important order history?

        //no deletes (for now at least)

    }
}
