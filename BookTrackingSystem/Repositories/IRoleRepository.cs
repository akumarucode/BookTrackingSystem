using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BookTrackingSystem.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> DisplayRoleAsync();

        Task<IdentityRole?> GetRoleAsync(string id);

        Task<IdentityRole> AddRoleAsync(IdentityRole roleDetails);

        Task<IdentityRole?> DeleteRoleAsync(string Id);

        Task<Role?> UpdateRoleAsync(Role role);

        Task<IEnumerable<IdentityUser>> DisplayUserSettingAsync();

        Task<IdentityUserRole<string>> SetUserRole(IdentityUserRole<string> userDetails);

        Task<IdentityUserRole<string>> DeleteUserRoleAsync(IdentityUserRole<string> userDetails);

        Task<IEnumerable<IdentityUserRole<string>>> DisplayUserRoleAsync();
    }
}
