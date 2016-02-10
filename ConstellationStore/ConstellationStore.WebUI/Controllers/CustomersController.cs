using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
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

        private DataContext db = new DataContext();


        // GET: List with filter
        public ActionResult Index(string searchString)
        {
            var model = customers.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.CustomerName.Contains(searchString));
            }

            return View(model);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            var model = customers.GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var model = new Customer();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            customers.Insert(customer);
            customers.Commit();
            return RedirectToAction("Index");
        }

    }
}
