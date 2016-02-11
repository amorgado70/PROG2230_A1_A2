using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConstellationStore.Models;
using ConstellationStore.Contracts.Data;
using ConstellationStore.Contracts.Repositories;

namespace ConstellationStore.WebUI.Controllers
{
    public class OrdersController : Controller
    {
        IRepositoryBase<Order> orders;
        IRepositoryBase<Customer> customers;

        public OrdersController(IRepositoryBase<Order> orders, IRepositoryBase<Customer> customers)
        {
            this.orders = orders;
            this.customers = customers;
        }//end Constructor

        // GET: list with filter
        //public ActionResult Index(string searchString)
        //{
        //    var order = orders.GetAll();

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        order = order.Where(s => s.OrderId.ToString().Contains(searchString));
        //    }

        //    return View(order);
        //}
        public ActionResult Index()
        {
            ViewBag.Orders = orders.GetAll();
            ViewBag.Customers = customers.GetAll();
            return View();
        }

        // GET: /Details/5
        public ActionResult Details(int? id)
        {
            var order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Create
        public ActionResult Create()
        {
            var order = new Order();
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            orders.Insert(order);
            orders.Commit();

            return RedirectToAction("Index");
        }

        // GET: /Edit/5
        public ActionResult Edit(int id)
        {
            Order order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            orders.Update(order);
            orders.Commit();

            return RedirectToAction("Index");
        }

        private DataContext contextForDelete = new DataContext();

        // GET: /Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = contextForDelete.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = contextForDelete.Orders.Find(id);
            contextForDelete.Orders.Remove(order);
            contextForDelete.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: /Delete/5
        //public ActionResult Delete(int id)
        //{
        //    Order order = orders.GetById(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Order order)
        //{
        //    orders.Delete(order);
        //    orders.Commit();

        //    return RedirectToAction("Index");
        //}

    }
}
