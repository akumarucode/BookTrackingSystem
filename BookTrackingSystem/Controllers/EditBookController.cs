using Azure;
using BookTrackingSystem.Data;
using BookTrackingSystem.Data.Migrations;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookTrackingSystem.Controllers
{
    public class EditBookController : Controller
    {

        private readonly IBookRepository bookRepository;

        public EditBookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var book = await bookRepository.GetBookAsync(id);

                if (book != null)
                {
                    var editBookRequest = new EditBookRequest
                    {
                        bookID = book.bookID,
                        bookName = book.bookName,
                        refNo = book.refNo,
                        author = book.author,
                        registerTime = book.registerTime,
                        issuer = book.issuer

                    };

                    return View(editBookRequest);
                }

                return View(null);
            }

            return RedirectToAction("Restricted", "Restrictions");
        }




        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditBookRequest editBookRequest)
        {
            if(ModelState.IsValid) 
            {
                var book = new book 
                { bookID = editBookRequest.bookID, bookName = editBookRequest.bookName,refNo = editBookRequest.refNo, author = editBookRequest.author, registerTime = editBookRequest.registerTime, issuer = editBookRequest.issuer 
                };

                var getBookDetails = await bookRepository.GetBookRefAsync(book);

                if (getBookDetails == null)
                {
                    var updatedBook = await bookRepository.UpdateBookAsync(book);

                    if (updatedBook != null)
                    {

                        //show success notification
                        return RedirectToAction("Updated");

                    }
                }

                else
                {
                    if (getBookDetails.refNo == editBookRequest.refNo && getBookDetails.bookID == editBookRequest.bookID)
                    {
                        var updatedBook = await bookRepository.UpdateBookAsync(book);

                        if (updatedBook != null)
                        {

                            //show success notification
                            return RedirectToAction("Updated");

                        }
                    }

                    if (getBookDetails.refNo == editBookRequest.refNo && getBookDetails.bookID != editBookRequest.bookID)
                    {
                        TempData["alertMessage"] = "Reference No. already exist!";
                        return View(editBookRequest);

                    }

                    else
                    {
                        var updatedBook = await bookRepository.UpdateBookAsync(book);

                        if (updatedBook != null)
                        {

                            //show success notification
                            return RedirectToAction("Updated");

                        }
                    }

                }
            }
            //show failure notification
            return RedirectToAction("Edit", new { id = editBookRequest.bookID });
        }

        //[Authorize(Roles = "Admin,Librarian")]
        ////Delete
        //[HttpGet]
        //public IActionResult Delete(string confirm_value,Guid Id)
        //{

        //    var book = bookDbContext.books.Find(Id);
        //    if (confirm_value == "Yes")
        //    {
        //        if (book != null)
        //        {
        //            bookDbContext.books.Remove(book);
        //            bookDbContext.SaveChanges();

        //            //show success notification
        //            return RedirectToAction("Deleted");
        //        }
        //    }
        //    else
        //    {
        //        //error notification
        //        return RedirectToAction("DisplayBook", "displayBook");
        //    }

        //    return null;
            

        //}        
        
        [Authorize(Roles = "Admin,Librarian")]
        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string confirm_value,Guid Id)
        {

            
            if (confirm_value == "Yes")
            {
                await bookRepository.DeleteBookAsync(Id);
                TempData["alertMessage"] = "Book deleted!";
                return RedirectToAction("DisplayBook", "displayBook");
            }
            else
            {
                //error notification
                return RedirectToAction("DisplayBook", "displayBook");
            }


        }

        [Authorize(Roles = "Admin,Librarian")]
        //redirect to updated page
        public ActionResult Updated()
        {
            TempData["alertMessage"] = "Book details updated!";
            return RedirectToAction("DisplayBook", "displayBook");
        }


        [Authorize(Roles = "Admin,Librarian")]
        //redirect to updated page
        public ActionResult Deleted()
        {
            TempData["alertMessage"] = "Book deleted!";
            return RedirectToAction("DisplayBook", "displayBook");
        }

        [Authorize(Roles = "Admin,Librarian")]
        //redirect to updated page
        public ActionResult Cancel()
        {
            TempData["alertMessage"] = "Operation cancelled!";
            return RedirectToAction("DisplayBook", "displayBook");
        }

    }
}
