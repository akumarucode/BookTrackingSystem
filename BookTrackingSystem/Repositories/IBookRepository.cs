using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;

namespace BookTrackingSystem.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<book>> DisplayBookAsync();

        Task<book?> GetBookAsync(Guid id);

        Task<book?> GetBookRefAsync(book bookDetails);

        Task<book> RegisterBookAsync(book bookDetails);
        Task<book?> UpdateBookAsync(book book);

        Task<book?> UpdateBookStatusBorrowedAsync(Guid id);
        Task<book?> UpdateBookStatusAvailableAsync(Guid id);
        Task<book?> DeleteBookAsync(Guid Id);

        



    }
}
