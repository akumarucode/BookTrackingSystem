﻿using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace BookTrackingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult registerBook()
        {
            return View();
        }

 


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult registerBook(book bookDetails )
        {

            book bookInfo = new book();

            Guid idGenerator = Guid.NewGuid();

            bookInfo.bookID = idGenerator;
            bookInfo.bookName = HttpContext.Request.Form["bookNametxt"].ToString();
            bookInfo.registerTime = Convert.ToDateTime(HttpContext.Request.Form["regdate"]);
            bookInfo.author = HttpContext.Request.Form["authortxt"].ToString();
            bookInfo.issuer = HttpContext.Request.Form["issuertxt"].ToString();

            int result = bookInfo.SaveDetails();
            if (result > 0)
            {
                ViewBag.Result = "Data Saved Successfully";
            }
            else
            {
                ViewBag.Result = "Something Went Wrong";
            }
            return RedirectToAction("DisplayBook", "displayBook");
        }
    }
}