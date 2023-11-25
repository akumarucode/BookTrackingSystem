using BookTrackingSystem.Data.Migrations;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookTrackingSystem.Controllers
{
    public class BorrowBookController : Controller
    {

        private readonly IBookRepository bookRepository;
        private readonly IBorrowBookRepository borrowBookRepository;

        public BorrowBookController(IBookRepository bookRepository, IBorrowBookRepository borrowBookRepository)
        {
            this.bookRepository = bookRepository;
            this.borrowBookRepository = borrowBookRepository;
        }

        [Authorize(Roles = "Admin,Librarian,User")]
        [HttpGet]
        public async Task<IActionResult> Borrow(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var getBook = await bookRepository.GetBookAsync(id);

                if (getBook != null)
                {
                    var getBookDetails = new BorrowBookRequest
                    {
                        borrowID = Guid.NewGuid(),
                        bookID = getBook.bookID.ToString(),
                        bookName = getBook.bookName

                    };

                    return View(getBookDetails);
                }

                return View(null);
            }

            return RedirectToAction("Restricted", "Restrictions");
        }


        [Authorize(Roles = "Admin,Librarian,User")]
        [HttpPost]
        public IActionResult Borrow(BorrowBookRequest borrowDetails)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    borrowBookRepository.RegisterBorrowRequest(borrowDetails);
                    return RedirectToAction("BorrowRequestRegistered");
                }

                return View(borrowDetails);


            }

            return RedirectToAction("Restricted", "Restrictions");
        }


        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> borrowList()
        {

            if (User.Identity.IsAuthenticated)
            {

                var borrowList = await borrowBookRepository.DisplayBorrowList();
                return View(borrowList);
            }

            return RedirectToAction("Restricted", "Restrictions");

        }


        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet]
        public async Task<IActionResult> SearchBorrow(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var searchedBook = await borrowBookRepository.DisplayBorrowList();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchedBook = searchedBook.Where(m => m.bookName.Contains(searchString)
                                       || m.fullName.Contains(searchString)
                                       || m.libraryCardNo.Contains(searchString));
                return View(searchedBook);
            }

            return View(searchedBook);
        }


        [Authorize(Roles = "Admin,Librarian")]
        //Delete
        [HttpPost]
        public async Task<IActionResult> ApproveBorrowReq(string confirm_value, Guid Id)
        {
            ViewData["Approve"] = confirm_value;

             if (confirm_value == "Yes")
             {
                    var returnDetails = await borrowBookRepository.GetBorrowDetails(Id);
                    var returnBookReq = new ReturnList
                    {
                        returnID = Guid.NewGuid(),
                        borrowID = returnDetails.borrowID,
                        bookID = returnDetails.bookID,
                        bookName = returnDetails.bookName,
                        fullName = returnDetails.fullName,
                        libraryCardNo = returnDetails.libraryCardNo,
                        borrowDate = returnDetails.borrowDate,
                        expectReturnDate = returnDetails.expectReturnDate,
                        remark = returnDetails.remark

                    };

                    Guid convertedBookID = new Guid(returnDetails.bookID);

                    await bookRepository.UpdateBookStatusBorrowedAsync(convertedBookID);
                    await borrowBookRepository.AddReturnDetails(returnBookReq);
                    await borrowBookRepository.DeleteBorrowAsync(Id);
                    return RedirectToAction("borrowList", "BorrowBook");
             }

         else
            {

                return RedirectToAction("borrowList", "BorrowBook");
            }
            

        }

        [Authorize(Roles = "Admin,Librarian")]
        //Delete
        [HttpPost]
        public async Task<IActionResult> RejectBorrowReq(string confirm_value, Guid Id)
        {
            ViewData["Reject"] = confirm_value;

            if (confirm_value == "Yes")
            {
                
                await borrowBookRepository.DeleteBorrowAsync(Id);
                return RedirectToAction("borrowList", "BorrowBook");
            }

            else
            {

                return RedirectToAction("borrowList", "BorrowBook");
            }


        }


        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> ReturnBook()
        {
            if (User.Identity.IsAuthenticated)
            {

                var returnList = await borrowBookRepository.DisplayReturnList();
                return View(returnList);
            }

            return RedirectToAction("Restricted", "Restrictions");
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> ReturnBook(string confirm_value,Guid Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (confirm_value == "Yes") 
                    {
                        var returnDetails = await borrowBookRepository.GetReturnDetails(Id);
                        Guid convertedBookID = new Guid(returnDetails.bookID);
                        await bookRepository.UpdateBookStatusAvailableAsync(convertedBookID);
                        await borrowBookRepository.DeleteReturnAsync(returnDetails.returnID);
                        return RedirectToAction("BookReturned");
                    }

                    else 
                    {
                        return RedirectToAction("ReturnBook", "BorrowBook");
                    }

                }


            }

            return RedirectToAction("Restricted", "Restrictions");
        }



        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> SearchReturn(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var searchedBook = await borrowBookRepository.DisplayReturnList();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchedBook = searchedBook.Where(m => m.bookName.Contains(searchString)
                                       || m.fullName.Contains(searchString)
                                       || m.libraryCardNo.Contains(searchString));
                return View(searchedBook);
            }

            return View(searchedBook);
        }






        //redirect to updated page
        public ActionResult BorrowRequestRegistered()
        {
            TempData["alertMessage"] = "Borrow request sent!";
            return RedirectToAction("displayBook", "DisplayBook");
        }


        //redirect to returned page
        public ActionResult BookReturned()
        {
            TempData["alertMessage"] = "Book returned!";
            return RedirectToAction("ReturnBook", "BorrowBook");
        }

    }
}
