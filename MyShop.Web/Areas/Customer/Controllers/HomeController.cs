using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
using MyShop.Entities.ViewModels;

namespace MyShop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitIfWork unitIfWork;

        public HomeController(IUnitIfWork unitIfWork)
        {
            this.unitIfWork = unitIfWork;
        }
        public IActionResult Index()
        {
           var products = unitIfWork.Product.GetAll();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var productCountVM = new ProductCountVM()
            {
                Product = unitIfWork.Product.GetFirstOrDefault(p => p.Id == id, include: "Category"),
                Count = 1
            };
            return View(productCountVM);
        }
    }
}
