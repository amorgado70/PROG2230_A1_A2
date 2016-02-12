using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConstellationStore.Models;
using ConstellationStore.Contracts.Data;
using ConstellationStore.Contracts.Repositories;

namespace ConstellationStore.WebUI.Controllers
{
    public class CustomersController : Controller
    {

        IRepositoryBase<Customer> customers;

        public CustomersController(IRepositoryBase<Customer> customers)
        {
            this.customers = customers;
        }//end Constructor

        // GET: list with filter
        public ActionResult Index(string searchString)
        {
            var customer = customers.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.CustomerName.Contains(searchString));
            }

            return View(customer);
        }

        // GET: /Details/5
        public ActionResult Details(int? id)
        {
            var customer = customers.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Create
        public ActionResult Create()
        {
            var customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            customers.Insert(customer);
            customers.Commit();

            return RedirectToAction("Index");
        }

        // GET: /Edit/5
        public ActionResult Edit(int id)
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
        public ActionResult Edit(Customer customer)
        {
            customers.Update(customer);
            customers.Commit();

            return RedirectToAction("Index");
        }

        // GET: /Delete/5
        public ActionResult Delete(int id)
        {
            Customer customer = customers.GetById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            customers.Delete(customers.GetById(id));
            customers.Commit();
            return RedirectToAction("Index");
        }

    }
}
