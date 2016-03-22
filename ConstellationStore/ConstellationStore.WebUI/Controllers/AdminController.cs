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
    public class AdminController : Controller
    {

        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<Order> orders;

        public AdminController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<Order> orders)
        {
            this.customers = customers;
            this.products = products;
            this.orders = orders;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductIndex(string searchString, string sortOrder)
        {
            var product = products.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Description.Contains(searchString));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Description);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                default:
                    product = product.OrderBy(s => s.Description);
                    break;
            }

            return View(product);
        }

        // GET: /Details/5
        public ActionResult ProductDetails(int? id)
        {
            var product = products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Create
        public ActionResult ProductCreate()
        {
            var product = new Product();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(Product product)
        {
            products.Insert(product);
            products.Commit();

            return RedirectToAction("ProductIndex");
        }

        // GET: /Edit/5
        public ActionResult ProductEdit(int id)
        {
            Product product = products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(Product product)
        {
            products.Update(product);
            products.Commit();

            return RedirectToAction("ProductIndex");
        }

        // GET: /Delete/5
        public ActionResult ProductDelete(int id)
        {
            Product product = products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("ProductDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ProdcutDeleteConfirm(int id)
        {
            products.Delete(products.GetById(id));
            products.Commit();
            return RedirectToAction("ProductIndex");
        }

        // GET: list with filter
        public ActionResult CustomerIndex(string searchString)
        {
            var customer = customers.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.CustomerName.Contains(searchString));
            }

            return View(customer);
        }

        // GET: /Details/5
        public ActionResult CustomerDetails(int? id)
        {
            var customer = customers.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Create
        public ActionResult CustomerCreate()
        {
            var customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerCreate(Customer customer)
        {
            customers.Insert(customer);
            customers.Commit();

            return RedirectToAction("CustomerIndex");
        }

        // GET: /Edit/5
        public ActionResult CustomerEdit(int id)
        {
            Customer customer = customers.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerEdit(Customer customer)
        {
            customers.Update(customer);
            customers.Commit();

            return RedirectToAction("CustomerIndex");
        }

        // GET: /Delete/5
        public ActionResult CustomerDelete(int id)
        {
            Customer customer = customers.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("CustomerDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDeleteConfirm(int id)
        {
            customers.Delete(customers.GetById(id));
            customers.Commit();
            return RedirectToAction("CustomerIndex");
        }

        public ActionResult OrderIndex(string searchString1, string searchString2, string sortOrder)
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
        public ActionResult OrderDetails(int? id)
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
        public ActionResult OrderCreate()
        {
            var order = new Order();

            // viewBag for DropdownList
            var customer = customers.GetAll();
            ViewBag.Customer = customer.OrderBy(s => s.CustomerName);

            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate(Order order)
        {
            orders.Insert(order);
            orders.Commit();

            return RedirectToAction("OrderIndex");
        }

        // GET: /Edit/5
        public ActionResult OrderEdit(int id)
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
        public ActionResult OrderEdit(Order order)
        {
            orders.Update(order);
            orders.Commit();

            return RedirectToAction("OrderIndex");
        }

        // GET: /Delete/5
        public ActionResult OrderDelete(int id)
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
        [HttpPost, ActionName("OrderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDeleteConfirm(int id)
        {
            orders.Delete(orders.GetById(id));
            orders.Commit();
            return RedirectToAction("OrderIndex");
        }

    }

}