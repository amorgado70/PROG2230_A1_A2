using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ConstellationStore.Models;
using ConstellationStore.Contracts.Data;
using ConstellationStore.Contracts.Repositories;

namespace ConstellationStore.WebUI.Controllers
{
    public class ViewOrderCustomerController : Controller
    {

        IRepositoryBase<Order> orders;
        IRepositoryBase<Customer> customers;

        public ViewOrderCustomerController(IRepositoryBase<Order> orders,IRepositoryBase<Customer> customers)
        {
            this.orders = orders;
            this.customers = customers;
        }//end Constructor

        // GET: list with filter
        public ActionResult Index(string searchString)
        {

            ViewOrderCustomer orderscustomers = new ViewOrderCustomer();
            orderscustomers.Orders = orders.GetAll();
            orderscustomers.Customers = customers.GetAll();

            return View(orderscustomers);
        }
    }
}
