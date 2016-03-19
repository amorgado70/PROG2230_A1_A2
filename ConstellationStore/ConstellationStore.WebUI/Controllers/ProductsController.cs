using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConstellationStore.Models;
using ConstellationStore.Contracts.Data;
using ConstellationStore.Contracts.Repositories;
using ConstellationStore.Services;

namespace ConstellationStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {

        IRepositoryBase<Customer> customers;
        IRepositoryBase<Product> products;
        IRepositoryBase<Basket> baskets;
        IRepositoryBase<BasketItem> basketitems;
        BasketService basketService;

        public ProductsController(IRepositoryBase<Customer> customers, IRepositoryBase<Product> products, IRepositoryBase<Basket> baskets, IRepositoryBase<BasketItem> basketitems)
        {
            this.customers = customers;
            this.products = products;
            this.baskets = baskets;
            this.basketitems = basketitems;
            basketService = new BasketService(this.baskets, this.basketitems);
        }

        public ActionResult QuantityInBasket()
        {
            var result = basketService.QuantityInBasket(this.HttpContext);
            return Json(result);
        }

        public ActionResult AddToBasket(int id)
        {
            basketService.AddToBasket(this.HttpContext, id, 1);//always add one to the basket
            return RedirectToAction("BasketSummary");
        }
        public ActionResult BasketSummary()
        {
            ViewBag.QuantityInBasket = basketService.QuantityInBasket(this.HttpContext);
            ViewBag.AmountInBasket = basketService.AmountInBasket(this.HttpContext);
            var model = basketService.GetBasket(this.HttpContext);
            return View(model.BasketItems);
        }

        public ActionResult DeleteFromBasket(int id)
        {
            var model = basketService.GetBasket(this.HttpContext);
            BasketItem basketItem = model.BasketItems.Where(i => i.BasketItemID == id).First();
            if (basketItem == null)
            {
                return HttpNotFound();
            }
            return View(basketItem);
        }

        [HttpPost, ActionName("DeleteFromBasket")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            basketService.RemoveFromBasket(id);
            return RedirectToAction("Index");
        }

        // GET: list with filter
        public ActionResult Index(string searchString, string sortOrder)
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

        //// GET: /Details/5
        //public ActionResult Details(int? id)
        //{
        //    var product = products.GetById(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// GET: /Create
        //public ActionResult Create()
        //{
        //    var product = new Product();
        //    return View(product);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Product product)
        //{
        //    products.Insert(product);
        //    products.Commit();

        //    return RedirectToAction("Index");
        //}

        //// GET: /Edit/5
        //public ActionResult Edit(int id)
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
        //public ActionResult Edit(Product product)
        //{
        //    products.Update(product);
        //    products.Commit();

        //    return RedirectToAction("Index");
        //}

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
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirm(int id)
        //{
        //    products.Delete(products.GetById(id));
        //    products.Commit();
        //    return RedirectToAction("Index");
        //}

    }
}
