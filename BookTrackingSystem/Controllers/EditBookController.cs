using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookTrackingSystem.Controllers
{
    public class EditBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    book newBook = new book();
        //    EditBookController books = newBook.GetAllEditBookController().FirstOrDefault(emp => emp.ID == id);
        //    return View(employee);
        //}        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    book newBook = new book();
        //    EditBookController books = newBook.GetAllEditBookController().FirstOrDefault(emp => emp.ID == id);
        //    return View(employee);
        //}
    }
}
