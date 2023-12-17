using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            if (borrowDetails.remark == null)
            {
                var borrowBook = new BorrowBookRequest
                {
                    borrowID = borrowDetails.borrowID,
                    bookID = borrowDetails.bookID,
                    bookName = borrowDetails.bookName,
                    fullName = borrowDetails.fullName,
                    libraryCardNo = borrowDetails.libraryCardNo,
                    borrowDate = borrowDetails.borrowDate,
                    expectReturnDate = borrowDetails.expectReturnDate,
                    remark = "None"
                };

                bookDbContext.BorrowBookRequests.Add(borrowBook);
                bookDbContext.SaveChanges();
            }

            else
            {
                bookDbContext.BorrowBookRequests.Add(borrowDetails);
                bookDbContext.SaveChanges();
            }

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


        //History methods
        public async Task<BorrowHistory> BorrowTransactionINAsync(BorrowHistory transactionDetails)
        {
            bookDbContext.borrowHistories.Add(transactionDetails);
            bookDbContext.SaveChanges();
            return transactionDetails;
        }


        public async Task<ReturnHistory> ReturnTransactionINAsync(ReturnHistory transactionDetails)
        {
            bookDbContext.returnHistories.Add(transactionDetails);
            bookDbContext.SaveChanges();
            return transactionDetails;
        }



        public async Task<IEnumerable<BorrowHistory>> DisplayBorrowHistory()
        {
            return await bookDbContext.borrowHistories.OrderByDescending(x => x.approvedDate).ToListAsync();
        }

        public async Task<IEnumerable<ReturnHistory>> DisplayReturnHistory()
        {
            return await bookDbContext.returnHistories.OrderByDescending(x=>x.actualReturnDate).ToListAsync();
        }


    }
}
