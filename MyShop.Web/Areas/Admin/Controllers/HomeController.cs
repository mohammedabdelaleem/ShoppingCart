﻿using Microsoft.AspNetCore.Mvc;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
