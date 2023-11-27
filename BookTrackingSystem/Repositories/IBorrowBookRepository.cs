using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;

namespace BookTrackingSystem.Repositories
{
    public interface IBorrowBookRepository
    {
        //Borrow methods
        Task<BorrowBookRequest> RegisterBorrowRequestAsync(BorrowBookRequest borrowDetails);

        Task<IEnumerable<BorrowBookRequest>> DisplayBorrowList();

        Task<BorrowBookRequest?> GetBorrowDetails(Guid id);

        Task<BorrowBookRequest?> DeleteBorrowAsync(Guid Id);



        //Return methods
        Task<ReturnList> AddReturnDetails(ReturnList returnList);

        Task<IEnumerable<ReturnList>> DisplayReturnList();

        Task<ReturnList?> GetReturnDetails(Guid id);

        Task<ReturnList?> DeleteReturnAsync(Guid Id);

    }
}
