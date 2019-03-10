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
    public class AddressController : Controller
    {
        public IProject1DbRepository Repo { get; }
        private readonly ILogger<AddressController> _logger;

        public AddressController(IProject1DbRepository repo, ILogger<AddressController> logger)
        {
            Repo = repo;
            _logger = logger;
        }

        // GET: Address
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Address> address = Repo.GetAllAddresses();

                var viewModels = address.Select(m => new AddressViewModel(m));
                return View(viewModels);
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
        }

        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var address = Repo.GetAddressById(id);

                var viewModels = new AddressViewModel(address);
                return View(viewModels);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, $"DB Address {id} was not found.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            return View(new AddressViewModel());
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel collection)
        {
            try
            {
                var address = new Address(collection.Street, collection.City, collection.State, 
                    collection.Country, collection.Zipcode)
                {
                    Id = collection.Id,
                };

                Repo.Add(address);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, "Create address error.");
                return View(collection);
            }
        }

        //// GET: Address/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Address/Edit/5
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

        //// GET: Address/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Address/Delete/5
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