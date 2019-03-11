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

                var viewModels = stores.Select(m => new StoreViewModel(m)
                { });
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
            return StoreDetails(id);
        }

        public ActionResult StoreDetails(int id)
        {
            try
            {
                var store = Repo.GetStoreWithDetailsById(id);
                var viewModel = new StoreViewModel(store)
                {
                    Addresses = Repo.GetAllAddresses().ToList(),
                    Ingredients = Repo.GetAllIngredients().Select(i => new IngredientViewModel(i)).ToList()
                };

                return View(viewModel);
            }
            catch (ArgumentNullException ex)
            {
                // should log that, and redirect to error page
                _logger.LogTrace(ex, $"DB Store {id} info was not found.");
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



        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return StoreDetails(id);
        }

        // POST: Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StoreViewModel collection)
        {
            try
            {
                var store = new Store(collection.Name)
                {
                    Id = collection.Id,
                };

                store.Address = Repo.GetAddressById(collection.Address.Id);
                Repo.Update(store); //change address

                var details = Repo.GetStoreWithDetailsById(store.Id);

                for (var i = 0; i < collection.Ingredients.Count; i++)
                {
                    if (collection.Ingredients[i].Checked)
                    {
                        //check if store item already exists and update
                        bool found = false;
                        foreach (var item in details.StoreItems)
                        {
                            try
                            {
                                if (item.Ingredient.Name == collection.Ingredients[i].Name)
                                {
                                    found = true;
                                    item.Quantity = collection.IngredientsAmount[i].Quantity ?? default(int);
                                    //if no value given, 0 will throw an error, preventing update
                                    Repo.Update(item);
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                _logger.LogTrace(e, "Store Item not updated to quantity of <1.");
                            }
                        }

                        //otherwise, add new store item
                        if (!found)
                        {
                            try {
                            var storeitem = new StoreItem(collection.IngredientsAmount[i].Quantity ?? default(int));
                            //if no value given, 0 will throw an error, preventing update of inventory
                            storeitem.Store = store;
                            storeitem.Ingredient = new Ingredient(collection.Ingredients[i].Name)
                            { Id = collection.Ingredients[i].Id };
                            Repo.Add(storeitem);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                _logger.LogTrace(e, "Store Item of quantity <1 not added to DB.");
                            }
                        }
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogTrace(e, "Update store error.");
                //collection.Addresses = Repo.GetAllAddresses().ToList(); //? Fixed null addresses error, but gave null storeitems error?
                return RedirectToAction(nameof(Index));
            }
        }



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