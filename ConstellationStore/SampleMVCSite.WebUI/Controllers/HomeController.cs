using System.Web.Mvc;
using SampleMVCSite.Contracts.Repositories;
using SampleMVCSite.Contracts.Data;
using SampleMVCSite.Models;

namespace SampleMVCSite.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<Customer> customers;
        public HomeController(IRepositoryBase<Customer> customers)
        { this.customers = customers; }

        public ActionResult Index()
        {
            //CustomerRepository customers = new CustomerRepository(new DataContext());
            ProductRepository products = new ProductRepository(new DataContext());
            Orders orders = new Orders(new DataContext());

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}