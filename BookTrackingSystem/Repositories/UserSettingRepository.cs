using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookTrackingSystem.Repositories
{
    public class UserSettingRepository : IUserSettingRepository
    {

        private IdentityDbContext _context;

        public UserSettingRepository(IdentityDbContext context)
        {
            this._context = context;
        }
        public async Task<IdentityUser?> DeleteUserAsync(string id)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser != null)
            {
                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync();
                return existingUser;

            }

            return null;
        }

        public async Task<IEnumerable<IdentityUser>> DisplayUserSettingAsync()
        {
            return await _context.Users.ToListAsync();
        }


        public Task<IdentityUser?> GetUserAsync(string id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Users?> UpdateUserAsync(Users user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            string capsUsername = user.UserName.ToUpper();

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.NormalizedUserName = capsUsername;
                existingUser.Email = user.Email;
   

                //save change
                await _context.SaveChangesAsync();

                return user;


            }

            return null;
        }


    }
}
