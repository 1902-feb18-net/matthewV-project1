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
    public class OrderController : Controller
    {
        public IProject1DbRepository Repo { get; }

        public OrderController(IProject1DbRepository repo)
        {
            Repo = repo;
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
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
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
                return RedirectToAction("Error", "Home");
            }
            catch (InvalidOperationException ex)
            {
                // should log that, and redirect to error page
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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