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
    public class IngredientController : Controller
    {
        public IProject1DbRepository Repo { get; }

        public IngredientController(IProject1DbRepository repo)
        {
            Repo = repo;
        }

        // GET: Ingredient
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Ingredient> ingredients = Repo.GetAllIngredients();

                var viewModels = ingredients.Select(m => new IngredientViewModel(m));
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

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var ingredient = Repo.GetIngredientById(id);

                var viewModels = new IngredientViewModel(ingredient);
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

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View(new IngredientViewModel() );
        }

        // POST: Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IngredientViewModel collection)
        {
            try
            {
                var ingredient = new Ingredient(collection.Name)
                {
                    Id = collection.Id,
                };

                Repo.Add(ingredient);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //// POST: Ingredient/Edit/5
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

        //// GET: Ingredient/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Ingredient/Delete/5
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