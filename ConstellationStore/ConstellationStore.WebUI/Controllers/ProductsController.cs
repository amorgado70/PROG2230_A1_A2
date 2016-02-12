using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConstellationStore.Models;
using ConstellationStore.Contracts.Data;
using ConstellationStore.Contracts.Repositories;

namespace ConstellationStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {

        IRepositoryBase<Product> products;

        public ProductsController(IRepositoryBase<Product> products)
        {
            this.products = products;
        }//end Constructor

        // GET: list with filter
        public ActionResult Index(string searchString, string sortOrder)
        {
            var product = products.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Description.Contains(searchString));
            }

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Description);
                    break;
                case "Date":
                    product = product.OrderBy(s => s.ProductId);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(s => s.ProductId);
                    break;
                default:
                    product = product.OrderBy(s => s.Description);
                    break;
            }  

            return View(product);
        }

        // GET: /Details/5
        public ActionResult Details(int? id)
        {
            var product = products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Create
        public ActionResult Create()
        {
            var product = new Product();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            products.Insert(product);
            products.Commit();

            return RedirectToAction("Index");
        }

        // GET: /Edit/5
        public ActionResult Edit(int id)
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
        public ActionResult Edit(Product product)
        {
            products.Update(product);
            products.Commit();

            return RedirectToAction("Index");
        }

        // GET: /Delete/5
        public ActionResult Delete(int id)
        {
            Product product = products.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            products.Delete(products.GetById(id));
            products.Commit();
            return RedirectToAction("Index");
        }

    }
}
