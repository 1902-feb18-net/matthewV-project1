using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.Library;
using Project1.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project1.WebApp.Controllers
{
    public class PizzaController : Controller
    {
        public IProject1DbRepository Repo { get; }

        public PizzaController(IProject1DbRepository repo)
        {
            Repo = repo;
        }

        // GET: Pizza
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Pizza> pizzas = Repo.GetAllPizzas();

                var viewModels = pizzas.Select(m => new PizzaViewModel(m));
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

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var pizza = Repo.GetPizzaById(id);

                var viewModels = new PizzaViewModel(pizza);
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

        // GET: Pizza/Create
        public ActionResult Create()
        {
            var viewModel = new PizzaViewModel
            {             
                Ingredients = Repo.GetAllIngredients().Select(i => new IngredientViewModel(i)).ToList() 
                //pass all ingredients for choosing in pizza creation
            };

            return View(viewModel);
        }

        // POST: Pizza/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaViewModel collection)
        {
            try
            {
                var pizza = new Pizza(collection.Name, collection.Price)
                {
                    Id = collection.Id,
                };

                //Repo.Add(pizza);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        //// GET: Pizza/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Pizza/Edit/5
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

        //// GET: Pizza/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Pizza/Delete/5
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