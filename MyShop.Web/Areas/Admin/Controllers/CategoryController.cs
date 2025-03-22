using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitIfWork unitIfWork;

        public CategoryController(IUnitIfWork unitIfWork)
        {
            this.unitIfWork = unitIfWork;
        }

        public IActionResult GetAllDataJson()
        {
            var categories = unitIfWork.Category.GetAll();//Select(c=>new { 
            //    Name = c.Name,
            //    Description = c.Description,
            //    CreatedAt = c.CreatedAt
            //      }).ToList();

        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles };


            return Json(new { data = categories }, options);
        }

            public IActionResult Index()
        {
            ////  TempData.Keep("Message"); // Ensures TempData persists if needed
            //IEnumerable<Category> categories = unitIfWork.Category.GetAll();
            return View("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                unitIfWork.Category.Add(category);
                if (unitIfWork.Complete() > 0)
                {
                    TempData["Add"] = "Item has been added successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error While Adding New Category, Retry Again.";
                    return View("Error");
                }
            }

            return View("Add", category);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) // i think that i don't need this , i only click on the button 
            {
                return NotFound();
            }

            var category = unitIfWork.Category.GetFirstOrDefault(c => c.Id == id);
            return View("Edit", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitIfWork.Category.Update(category);
                unitIfWork.Complete();
                TempData["Edit"] = "Item Updated Successfully";
                return RedirectToAction("Index");

            }
            return View("Edit", category);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = unitIfWork.Category.GetFirstOrDefault(c => c.Id == id);
            ViewBag.Title = "Details";
            return View("Details", category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = unitIfWork.Category.GetFirstOrDefault(c => c.Id == id);
            ViewBag.Title = "Delete";
            return View("Details", category);
        }


        public IActionResult ConfirmDelete(int id)
        {
            var category = unitIfWork.Category.GetFirstOrDefault(c => c.Id == id);
            unitIfWork.Category.Remove(category);
            unitIfWork.Complete();
            TempData["Delete"] = "Item Has Been Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
