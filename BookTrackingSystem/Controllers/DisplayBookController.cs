using BookTrackingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using BookTrackingSystem.Models;
using static System.Reflection.Metadata.BlobBuilder;
using Syncfusion.XlsIO;
using System.Data;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using BookTrackingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookTrackingSystem.Controllers
{
    public class displayBookController : Controller
    {
        private readonly IBookRepository bookRepository;

        public displayBookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
           return View();
        }

        //[HttpGet]
        //public IActionResult displayBook()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {

        //        var books = _context.books.ToList();
        //        List<book> bookList = new List<book>();

        //        if (books != null)
        //        {

        //            foreach (var Book in books)
        //            {
        //                var book = new book()
        //                {
        //                    bookID = Book.bookID,
        //                    bookName = Book.bookName,
        //                    author = Book.author,
        //                    registerTime = Book.registerTime,
        //                    issuer = Book.issuer,

        //                };
        //                bookList.Add(book);
        //            }
        //            return View(bookList);
        //        }

        //        return View(bookList);
        //    }

        //    return RedirectToAction("Restricted","Restrictions");

        //}

        [Authorize(Roles = "Admin,Librarian,User")]
        public async Task<IActionResult> displayBook()
        {

            if (User.Identity.IsAuthenticated)
            {

                var bookList = await bookRepository.DisplayBookAsync();
                return View(bookList);
            }

            return RedirectToAction("Restricted", "Restrictions");

        }

        [Authorize(Roles = "Admin,Librarian,User")]
        public async Task<IActionResult> SearchBook(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var searchedBook = await bookRepository.DisplayBookAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchedBook = searchedBook.Where(m => m.bookName.Contains(searchString)
                                       || m.author.Contains(searchString)
                                       || m.issuer.Contains(searchString));
                return View(searchedBook);
            }

            return View(searchedBook);
        }

        [Authorize(Roles = "Admin,Librarian")]
        public ActionResult ExportToExcel()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                //Initialize Application
                IApplication application = excelEngine.Excel;

                //Set the default application version as Excel 2016
                application.DefaultVersion = ExcelVersion.Excel2016;

                //Create a workbook with a worksheet
                IWorkbook workbook = application.Workbooks.Create(1);

                //Access first worksheet from the workbook instance
                IWorksheet worksheet = workbook.Worksheets[0];

                //Export data to Excel
                DataTable dataTable = GetDataTable();
                worksheet.ImportDataTable(dataTable, true, 1, 1);
                worksheet.UsedRange.AutofitColumns();

                //Save the workbook to disk in xlsx format
                //workbook.SaveAs("Output.xlsx", ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
            }

            return View();
        }

        [Authorize(Roles = "Admin,Librarian")]
        private static DataTable GetDataTable()
        {
            //Create a DataTable with four columns
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            //Add five DataRows
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

            return table;
        }

    }
}
