using BookTrackingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using BookTrackingSystem.Models;


namespace BookTrackingSystem.Controllers
{
    public class displayBookController : Controller
    {
        private ApplicationDbContext _context;

        public displayBookController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult displayBook()
        {
            var books = _context.books.ToList();
            List<book> bookList = new List<book>();

            if (books != null)
            {

                foreach (var Book in books)
                {
                    var book = new book()
                    {
                        bookID = Book.bookID,
                        bookName = Book.bookName,
                        author = Book.author,
                        registerTime = Book.registerTime,
                        issuer = Book.issuer,

                    };
                    bookList.Add(book);
                }
                return View(bookList);
            }

            return View(bookList);
        }

    }
}
