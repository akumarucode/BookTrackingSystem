using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using Microsoft.EntityFrameworkCore;

namespace BookTrackingSystem.Repositories
{
    public class BookRepository : IBookRepository

    {

        private ApplicationDbContext bookDbContext;

        public BookRepository(ApplicationDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }
        public async Task<book?> DeleteBookAsync(Guid id)
        {
            var existingBook = await bookDbContext.books.FindAsync(id);

            if (existingBook != null)
            {
                bookDbContext.books.Remove(existingBook);
                await bookDbContext.SaveChangesAsync();
                return existingBook;

            }

            return null;
        }

        public async Task<IEnumerable<book>> DisplayBookAsync()
        {
            return await bookDbContext.books.ToListAsync();
        }


        public  Task<book?> GetBookAsync(Guid id)
        {
            return bookDbContext.books.FirstOrDefaultAsync(x => x.bookID == id);
        }

        public async Task<book> RegisterBookAsync(book bookDetails)
        {
             bookDbContext.books.Add(bookDetails);
             bookDbContext.SaveChanges();
            return bookDetails;
        }

        public async Task<book?> UpdateBookAsync(book book)
        {
            var existingBook = await bookDbContext.books.FindAsync(book.bookID);

            if (existingBook != null)
            {
                existingBook.bookName = book.bookName;
                existingBook.author = book.author;
                existingBook.registerTime = book.registerTime;
                existingBook.issuer = book.issuer;

                //save change
                await bookDbContext.SaveChangesAsync();

                return existingBook;


            }

            return null;
        }

        public async Task<book?> UpdateBookStatusBorrowedAsync(Guid id)
        {
            var existingBook = await bookDbContext.books.FindAsync(id);

            if (existingBook != null)
            {

                existingBook.status = "Borrowed";

                //save change
                await bookDbContext.SaveChangesAsync();

                return existingBook;


            }

            return null;
        }

        public async Task<book?> UpdateBookStatusAvailableAsync(Guid id)
        {
            var existingBook = await bookDbContext.books.FindAsync(id);

            if (existingBook != null)
            {

                existingBook.status = "Available";

                //save change
                await bookDbContext.SaveChangesAsync();

                return existingBook;


            }

            return null;
        }
    }
}
