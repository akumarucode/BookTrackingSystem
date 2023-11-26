using BookTrackingSystem.Data;
using BookTrackingSystem.Data.Migrations;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public async Task<IActionResult> registerBook(book bookDetails)
        {
            if (User.Identity.IsAuthenticated)
            {
                if(ModelState.IsValid)
                {
                    
                    var getBookDetails = await bookRepository.GetBookRefAsync(bookDetails);
                    if (getBookDetails == null) 
                    
                    {
                        var addBookRequest = new book
                        {
                            bookID = bookDetails.bookID,
                            refNo = bookDetails.refNo,
                            bookName = bookDetails.bookName,
                            author = bookDetails.author,
                            registerTime = bookDetails.registerTime,
                            issuer = bookDetails.issuer,
                            status = "Available"

                        };

                        await bookRepository.RegisterBookAsync(addBookRequest);
                        TempData["alertMessage"] = "Book registered!";
                        return RedirectToAction("DisplayBook", "displayBook");
                    }


                    else
                    {
                        TempData["alertMessage"] = "Reference No. already exist!";
                        return View(bookDetails);
                    }
                }

                else
                {
                    return View(bookDetails);
                }

            }

            return RedirectToAction("Restricted", "Restrictions");
        }


    }
}
