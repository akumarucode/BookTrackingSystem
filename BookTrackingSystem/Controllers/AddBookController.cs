using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrackingSystem.Controllers
{


    public class AddBookController : Controller
    {

        private readonly IBookRepository bookRepository;

        public AddBookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet]
        public IActionResult registerBook()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Restricted", "Restrictions");
        }


        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public IActionResult registerBook(book bookDetails)
        {
            if (User.Identity.IsAuthenticated)
            {
                var addBookRequest = new book
                {
                    bookID = bookDetails.bookID,
                    bookName = bookDetails.bookName,
                    author = bookDetails.author,
                    registerTime = bookDetails.registerTime,
                    issuer = bookDetails.issuer,
                    status = "Available"

                };

            bookRepository.RegisterBookAsync(addBookRequest);
            return RedirectToAction("DisplayBook", "displayBook");


            }

            return RedirectToAction("Restricted", "Restrictions");
        }

      
    }
}
