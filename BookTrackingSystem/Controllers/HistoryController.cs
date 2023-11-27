using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTrackingSystem.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly IBorrowBookRepository borrowBookRepository;

        public HistoryController(IBookRepository bookRepository, IBorrowBookRepository borrowBookRepository)
        {
            this.bookRepository = bookRepository;
            this.borrowBookRepository = borrowBookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> borrowHistory()
        {
            if (User.Identity.IsAuthenticated)
            {

                var borrowHistory = await borrowBookRepository.DisplayBorrowHistory();
                return View(borrowHistory);
            }

            return RedirectToAction("Restricted", "Restrictions");

        }

        [Authorize(Roles = "Admin,Librarian,User")]
        public async Task<IActionResult> SearchBorrowHistory(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var searchedBorrowHistory = await borrowBookRepository.DisplayBorrowHistory();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchedBorrowHistory = searchedBorrowHistory.Where(m => m.bookName.Contains(searchString)
                                       || m.libraryCardNo.Contains(searchString)
                                       || m.borrowerName.Contains(searchString));
                return View(searchedBorrowHistory);
            }

            return View(searchedBorrowHistory);
        }


        [Authorize(Roles = "Admin,Librarian")]
        public async Task<IActionResult> returnHistory()
        {
            if (User.Identity.IsAuthenticated)
            {

                var returnHistory = await borrowBookRepository.DisplayReturnHistory();
                return View(returnHistory);
            }

            return RedirectToAction("Restricted", "Restrictions");

        }

        [Authorize(Roles = "Admin,Librarian,User")]
        public async Task<IActionResult> SearchReturnHistory(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var searchedReturnHistory = await borrowBookRepository.DisplayReturnHistory();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchedReturnHistory = searchedReturnHistory.Where(m => m.bookName.Contains(searchString)
                                       || m.libraryCardNo.Contains(searchString)
                                       || m.borrowerName.Contains(searchString));
                return View(searchedReturnHistory);
            }

            return View(searchedReturnHistory);
        }
    }
}
