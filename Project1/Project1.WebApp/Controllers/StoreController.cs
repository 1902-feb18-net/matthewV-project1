using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.Library;
using Project1.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.WebApp.Controllers
{
    public class StoreController : Controller
    {
        public IProject1DbRepository Repo { get; }
        private readonly ILogger<StoreController> _logger;

        public StoreController(IProject1DbRepository repo, ILogger<StoreController> logger)
        {
            Repo = repo;
            _logger = logger;
        }

        // GET: Store
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Store> stores = Repo.GetAllStores();

                var viewModels = stores.Select(m => new StoreViewModel(m));
                return View(viewModels);
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
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var store = Repo.GetStoreWithDetailsById(id);
                var viewModel = new StoreViewModel(store);
                return View(viewModel);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, $"DB Store {id} was not found.");
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, "Invalid state operation.");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            try
            {
                var viewModel = new StoreViewModel()
                { Addresses = Repo.GetAllAddresses().ToList() };

                return View(viewModel);
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

        // POST: Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreViewModel collection)
        {
            try
            {
                var store = new Store(collection.Name)
                {
                    Id = collection.Id,
                    Address = Repo.GetAddressById(collection.Address.Id)
                };

                Repo.Add(store);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, "Create store error.");
                return View(collection);
            }
        }



        //// GET: Store/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Store/Edit/5
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



        //// GET: Store/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Store/Delete/5
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