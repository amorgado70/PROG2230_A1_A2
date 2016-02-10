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
        public ActionResult Index(string searchString)
        {
            var product = products.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Description.Contains(searchString));
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

        private DataContext contextForDelete = new DataContext();

        // GET: /Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = contextForDelete.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = contextForDelete.Products.Find(id);
            contextForDelete.Products.Remove(product);
            contextForDelete.SaveChanges();
            return RedirectToAction("Index");
        }

        //// GET: /Delete/5
        //public ActionResult Delete(int id)
        //{
        //    Product product = products.GetById(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(Product product)
        //{
        //    products.Delete(product);
        //    products.Commit();

        //    return RedirectToAction("Index");
        //}

    }
}
