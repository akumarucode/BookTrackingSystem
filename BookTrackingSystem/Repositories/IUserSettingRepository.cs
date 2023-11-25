using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BookTrackingSystem.Repositories
{
    public interface IUserSettingRepository
    {
        Task<IEnumerable<IdentityUser>> DisplayUserSettingAsync();

        Task<IdentityUser?> GetUserAsync(string id);

        Task<Users?> UpdateUserAsync(Users user);
        Task<IdentityUser?> DeleteUserAsync(string Id);


        
    }
}
