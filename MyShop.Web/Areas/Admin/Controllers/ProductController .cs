using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
using MyShop.Entities.ViewModels;

namespace MyShop.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitIfWork unitIfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitIfWork unitIfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitIfWork = unitIfWork;
            this.webHostEnvironment = webHostEnvironment;
        }



        public IActionResult GetAllDataJson()
        {
            var products = unitIfWork.Product.GetAll(include:"Category").Select(p=> new
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = new { Name = p.Category.Name } // Ensures correct JSON structure
            }).ToList();

            return Json(new { data = products });
        }
        
        public IActionResult Index()
        {
            return View("Index");
        }


        [HttpGet]
        public IActionResult Add()
        {
            ProductCategoriesVM model = new ProductCategoriesVM()
            {
                Product = new Product(),
               CategoryList = unitIfWork.Category.GetAll().Select(x=> new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString()
               })
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductCategoriesVM productCategoriesVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                string rootPath = webHostEnvironment.WebRootPath; // wwwroot path 

                if(file != null)
                {
                    var filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(rootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);

                    using(var filestream = new FileStream(Path.Combine(Upload, filename+extension), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    productCategoriesVM.Product.Img = @"Images\Products\" + filename + extension;
                }

                unitIfWork.Product.Add(productCategoriesVM.Product);
                if (unitIfWork.Complete() > 0)
                {
                    TempData["Add"] = "Item has been added successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error While Adding New Product, Retry Again.";
                    return View("Error");
                }
            }

            return View("Add", productCategoriesVM);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) // i think that i don't need this , i only click on the button 
            {
                return NotFound();
            }

            ProductCategoriesVM model = new ProductCategoriesVM()
            {
                Product = unitIfWork.Product.GetFirstOrDefault(c => c.Id == id),
                CategoryList = unitIfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View("Edit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategoriesVM productCategoriesVM, IFormFile file)
        {
            ModelState.Remove("file"); // Removes validation for 'file'
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var rootPath = webHostEnvironment.WebRootPath;
                    var filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);
                    var upload = Path.Combine(rootPath, @"Images\Products");

                    // delete old image if found
                    if (productCategoriesVM.Product.Img != null)
                    {
                        var oldPath = Path.Combine(rootPath, productCategoriesVM.Product.Img.TrimStart('\\'));  //  Images\Products\   => output :   4f3fe72e - fd8a - 4614 - a82b - 224778d891c3.jpg 

                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath); // delete [4f3fe72e - fd8a - 4614 - a82b - 224778d891c3.jpg ] from wwwroot\Images\Products\
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    productCategoriesVM.Product.Img = @"Images\Products\" + filename + extension;
                }

                unitIfWork.Product.Update(productCategoriesVM.Product);
                unitIfWork.Complete();
                TempData["Edit"] = "Item Updated Successfully";
                return RedirectToAction("Index");

            }
            return View("Edit", productCategoriesVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = unitIfWork.Product.GetFirstOrDefault(c => c.Id == id);
            ViewBag.Title = "Details";
            return View("Details", product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = unitIfWork.Product.GetFirstOrDefault(c => c.Id == id);
            ViewBag.Title = "Delete";
            return View("Details", product);
        }


        public IActionResult ConfirmDelete(int id)
        {
            var product = unitIfWork.Product.GetFirstOrDefault(c => c.Id == id);
            unitIfWork.Product.Remove(product);
            unitIfWork.Complete();
            TempData["Delete"] = "Item Has Been Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
