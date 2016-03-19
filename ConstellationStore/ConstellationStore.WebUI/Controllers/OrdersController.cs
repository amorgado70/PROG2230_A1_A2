using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConstellationStore.Models;
using ConstellationStore.Models.ViewModel;
using ConstellationStore.Contracts.Data;
using ConstellationStore.Contracts.Repositories;
using System.Data.Entity;

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

        public ActionResult Index(string searchString1, string searchString2, string sortOrder)
        {
            DataContext contextForIndex = new DataContext();
            var viewModel =
                from o in contextForIndex.Orders
                join c in contextForIndex.Customers on o.CustomerId equals c.CustomerId
                select new OrderViewModel { Order = o, Customer = c };
            
            if (!String.IsNullOrEmpty(searchString1))
            {
                viewModel = viewModel.Where(s => s.Order.OrderId.ToString().Contains(searchString1));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                viewModel = viewModel.Where(s => s.Customer.CustomerName.Contains(searchString2));
            }

            ViewBag.OrderIdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.OrderDateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CustomerNameSortParm = sortOrder == "Customer" ? "name_desc" : "Customer";

            switch (sortOrder)
            {
                case "id_desc":
                    viewModel = viewModel.OrderByDescending(s => s.Order.OrderId);
                    break;
                case "Date":
                    viewModel = viewModel.OrderBy(s => s.Order.OrderDate);
                    break;
                case "date_desc":
                    viewModel = viewModel.OrderByDescending(s => s.Order.OrderDate);
                    break;
                case "Customer":
                    viewModel = viewModel.OrderBy(s => s.Customer.CustomerName);
                    break;
                case "name_desc":
                    viewModel = viewModel.OrderByDescending(s => s.Customer.CustomerName);
                    break;
                default:
                    viewModel = viewModel.OrderBy(s => s.Order.OrderId);
                    break;
            }  

            return View(viewModel);
        }

        // GET: /Details/5
        public ActionResult Details(int? id)
        {
            var order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            DataContext contextForIndex = new DataContext();
            var viewModel =
                from o in contextForIndex.Orders
                join c in contextForIndex.Customers on o.CustomerId equals c.CustomerId
                where o.OrderId == id
                select new OrderViewModel { Order = o, Customer = c };

            return View(viewModel);

        }
        // GET: /Create
        public ActionResult Create()
        {
            var order = new Order();

            // viewBag for DropdownList
            var customer = customers.GetAll();
            ViewBag.Customer = customer.OrderBy(s => s.CustomerName);

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

            // viewBag for DropdownList
            var customer = customers.GetAll();
            ViewBag.Customer = customer.OrderBy(s => s.CustomerName);

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

        // GET: /Delete/5
        public ActionResult Delete(int id)
        {
            Order order = orders.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            DataContext contextForIndex = new DataContext();
            var viewModel =
                from o in contextForIndex.Orders
                join c in contextForIndex.Customers on o.CustomerId equals c.CustomerId
                 where o.OrderId == id
                select new OrderViewModel { Order = o, Customer = c };

            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            orders.Delete(orders.GetById(id));
            orders.Commit();
            return RedirectToAction("Index");
        }
    }
}
