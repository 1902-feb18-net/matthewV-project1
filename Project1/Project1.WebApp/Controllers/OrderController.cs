using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Library;
using Project1.WebApp.ViewModels;

namespace Project1.WebApp.Controllers
{
    public class OrderController : Controller
    {
        public IProject1DbRepository Repo { get; }
        private readonly ILogger<CustomerController> _logger;

        public OrderController(IProject1DbRepository repo, ILogger<CustomerController> logger)
        {
            Repo = repo;
            _logger = logger;
        }

        // GET: Order
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Order> orders = Repo.GetAllOrders();

                var viewModels = orders.Select(m => new OrderViewModel(m));
                return View(viewModels);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "DB Orders was empty.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var order = Repo.GetOrderWithDetailsById(id);

                var viewModels = new OrderViewModel(order);
                return View(viewModels);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, $"DB Order {id} was not found.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var viewModel = new OrderViewModel();

            try
            {
                viewModel.Addresses = Repo.GetAllAddresses().ToList();
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "DB Addresses was empty.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }

            try
            {
                viewModel.Stores = Repo.GetAllStores().ToList();
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "DB Stores was empty.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }

            try
            {
                viewModel.Customers = Repo.GetAllCustomers().ToList();
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "DB Customers was empty.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }

            try
            {
                viewModel.Pizzas = Repo.GetAllPizzas().Select(p => new PizzaViewModel(p)).ToList();
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "DB Customers was empty.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }

            return View(viewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel collection)
        {
            try
            {
                var order = new Order()
                {
                    Id = collection.Id,
                    OrderTime = DateTime.Now
                };

                try
                {
                    order.Address = Repo.GetAddressById(collection.Address.Id);
                }
                catch (ArgumentNullException ex)
                {
                    // should log that, and redirect to error page
                    _logger.LogTrace(ex, "DB Address was not found.");
                    return RedirectToAction("Error", "Home");
                }
                catch (InvalidOperationException ex)
                {
                    // should log that, and redirect to error page
                    _logger.LogTrace(ex, "Invalid state operation.");
                    return RedirectToAction("Error", "Home");
                }

                try
                {
                    order.OrderedAt = Repo.GetStoreById(collection.OrderedAt.Id);
                }
                catch (ArgumentNullException ex)
                {
                    // should log that, and redirect to error page
                    _logger.LogTrace(ex, "DB Store was not found.");
                    return RedirectToAction("Error", "Home");
                }
                catch (InvalidOperationException ex)
                {
                    // should log that, and redirect to error page
                    _logger.LogTrace(ex, "Invalid state operation.");
                    return RedirectToAction("Error", "Home");
                }

                try
                {
                    order.OrderedBy = Repo.GetCustomerById(collection.OrderedBy.Id);
                }
                catch (ArgumentNullException ex)
                {
                    // should log that, and redirect to error page
                    _logger.LogTrace(ex, "DB Customer was not found.");
                    return RedirectToAction("Error", "Home");
                }
                catch (InvalidOperationException ex)
                {
                    // should log that, and redirect to error page
                    _logger.LogTrace(ex, "Invalid state operation.");
                    return RedirectToAction("Error", "Home");
                }

                //calculate total price and set it
                order.TotalPrice = 0;
                for (var i = 0; i < collection.Pizzas.Count; i++)
                {
                    if (collection.Pizzas[i].Checked)
                    {
                        order.TotalPrice += collection.Pizzas[i].Price * collection.PizzasAmount[i].Quantity ?? default(int);
                        //if no value given, doesn't effect total price (adds 0)
                    }
                }

                order.OrderItems = new List<OrderItem>();
                //Repo.Add(order);

                //add order items
                for (var i = 0; i < collection.Pizzas.Count; i++)
                {
                    if (collection.Pizzas[i].Checked)
                    {
                        try
                        {
                            var orderitem = new OrderItem(collection.PizzasAmount[i].Quantity ?? default(int));
                            //orderitem.Order = new Order { Id = order.Id }; //assign the generated value
                            orderitem.Pizza = new Pizza { Id = collection.Pizzas[i].Id, Name = collection.Pizzas[i].Name };
                            //name is alternate key, have to add it
                            order.OrderItems.Add(orderitem);
                            //Repo.Add(orderitem);
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            _logger.LogTrace(e, "Order Item of quantity <1 not added to DB");
                        }
                    }

                    Repo.Add(order);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException e)  //
            {
                _logger.LogTrace(e, "Place order error.");
                var viewModel = collection;
                viewModel.ErrorMessage = "A customer cannot place another order at the same store within two hours.";
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, "Place order error.");
                return RedirectToAction(nameof(Index));
            }
        }

        //// GET: Order/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Order/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Order/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Order/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}