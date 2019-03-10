using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.Library;
using Project1.WebApp.ViewModels;

namespace Project1.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        public IProject1DbRepository Repo { get; }

        public CustomerController(IProject1DbRepository repo)
        {
            Repo = repo;
        }

        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Customer> customers = Repo.GetAllCustomers();

                var viewModels = customers.Select(m => new CustomerViewModel(m));
                return View(viewModels);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var customer = Repo.GetCustomerWithDetailsById(id);

                var viewModels = new CustomerViewModel(customer);
                return View(viewModels);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }

        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel
            {
                Addresses = Repo.GetAllAddresses().ToList(),
                Stores = Repo.GetAllStores().ToList()
            };

            return View( viewModel );
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel collection)
        {
            try
            {
                var cust = new Customer(collection.FirstName, collection.LastName, collection.Email)
                {
                    Id = collection.Id,
                    Address = Repo.GetAddressById(collection.Address.Id),
                    Store = Repo.GetStoreById(collection.Store.Id),
                };

                Repo.Add(cust);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        //// GET: Customer/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Customer/Edit/5
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

        //// GET: Customer/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Customer/Delete/5
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