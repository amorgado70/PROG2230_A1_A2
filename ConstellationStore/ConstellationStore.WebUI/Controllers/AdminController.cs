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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }



    }
}