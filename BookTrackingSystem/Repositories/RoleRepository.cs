using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookTrackingSystem.Repositories
{
    public class RoleRepository : IRoleRepository
    {

        private IdentityDbContext _context;

        public RoleRepository(IdentityDbContext context)
        {
            this._context = context;
        }

        public async Task<IdentityRole> AddRoleAsync(IdentityRole roleDetails)
        {
            _context.Roles.Add(roleDetails);
            _context.SaveChanges();
            return roleDetails;
        }

        public Task<IdentityRole?> GetRoleAsync(string id)
        {
            return _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<IEnumerable<IdentityRole>> DisplayRoleAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IdentityRole?> DeleteRoleAsync(string id)
        {
            var existingRole = await _context.Roles.FindAsync(id);

            if (existingRole != null)
            {
                _context.Roles.Remove(existingRole);
                await _context.SaveChangesAsync();
                return existingRole;

            }

            return null;
        }

        public async Task<IdentityUserRole<string?>> DeleteUserRoleAsync(IdentityUserRole<string?> userDetails)
        {

            string id = userDetails.UserId;
            var existingUserRole = await _context.UserRoles.FindAsync(id);
           

            if (existingUserRole != null)
            {
                _context.UserRoles.Remove(existingUserRole);
                await _context.SaveChangesAsync();
                return existingUserRole;

            }

            return null;
        }



        public async Task<Role?> UpdateRoleAsync(Role role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);

            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.NormalizedName = role.NormalizedName;
                existingRole.ConcurrencyStamp = role.ConcurrencyStamp;

                //save change
                await _context.SaveChangesAsync();

                return role;


            }

            return null;
        }

        public async Task<IEnumerable<IdentityUser>> DisplayUserSettingAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IdentityUserRole<string>> SetUserRole(IdentityUserRole<string> userDetails)
        {
            _context.UserRoles.Add(userDetails);
            _context.SaveChanges();
            return userDetails;
        }




    }
}
