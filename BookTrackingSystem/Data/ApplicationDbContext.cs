using Azure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookTrackingSystem.Models;
using Microsoft.AspNetCore.Identity;
using BookTrackingSystem.Models.viewModels;

namespace BookTrackingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<book> books { get; set; }

        public DbSet<Users> AspNetUsers { get; set; }
        public DbSet<BorrowBookRequest> BorrowBookRequests { get; set; }
        public DbSet<ReturnList> ReturnLists { get; set; }

        public DbSet<BorrowHistory> borrowHistories { get; set; }
        public DbSet<ReturnHistory> returnHistories { get; set; }



    }
}