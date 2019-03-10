using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public StoreController(IProject1DbRepository repo)
        {
            Repo = repo;
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
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
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
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            var viewModel = new StoreViewModel() { Addresses = Repo.GetAllAddresses().ToList() };

            return View( viewModel );
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
            catch
            {
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