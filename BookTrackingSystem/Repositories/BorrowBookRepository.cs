using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookTrackingSystem.Repositories
{
    public class BorrowBookRepository : IBorrowBookRepository
    {
        private ApplicationDbContext bookDbContext;

        public BorrowBookRepository(ApplicationDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }

        public async Task<BorrowBookRequest> RegisterBorrowRequestAsync(BorrowBookRequest borrowDetails)
        {
            bookDbContext.BorrowBookRequests.Add(borrowDetails);
            bookDbContext.SaveChanges();
            return borrowDetails;
        }



        public async Task<IEnumerable<BorrowBookRequest>> DisplayBorrowList()
        {
            return await bookDbContext.BorrowBookRequests.ToListAsync();
        }

        public async Task<IEnumerable<ReturnList>> DisplayReturnList()
        {
            return await bookDbContext.ReturnLists.ToListAsync();
        }

        public Task<BorrowBookRequest?> GetBorrowDetails(Guid id)
        {
            return bookDbContext.BorrowBookRequests.FirstOrDefaultAsync(x => x.borrowID == id);
        }

        public Task<ReturnList?> GetReturnDetails(Guid id)
        {
            return bookDbContext.ReturnLists.FirstOrDefaultAsync(x => x.borrowID == id);
        }

        public async Task<ReturnList> AddReturnDetails(ReturnList returnDetails)
        {
            bookDbContext.ReturnLists.Add(returnDetails);
            bookDbContext.SaveChanges();
            return returnDetails;
        }


        public async Task<BorrowBookRequest?> DeleteBorrowAsync(Guid id)
        {
            var existingBorrow = await bookDbContext.BorrowBookRequests.FindAsync(id);

            if (existingBorrow != null)
            {
                bookDbContext.BorrowBookRequests.Remove(existingBorrow);
                await bookDbContext.SaveChangesAsync();
                return existingBorrow;

            }

            return null;
        }

        public async Task<ReturnList?> DeleteReturnAsync(Guid id)
        {
            var existingReturn = await bookDbContext.ReturnLists.FindAsync(id);

            if (existingReturn != null)
            {
                bookDbContext.ReturnLists.Remove(existingReturn);
                await bookDbContext.SaveChangesAsync();
                return existingReturn;

            }

            return null;
        }


    }
}
