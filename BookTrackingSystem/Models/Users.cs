using BookTrackingSystem.Data.Migrations;
using BookTrackingSystem.Models.ConnectionString;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Globalization;

namespace BookTrackingSystem.Models
{
    [NotMapped]
    public class Users : IdentityUser
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName  { get; set; }

        public string NormalizedUserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        public string? roleId { get; set; }


        //public string NormalizedEmail { get; set; }

        //public bool EmailConfirmed { get; set; }

        //public string PasswordHash { get; set; }

        //public string SecurityStamp { get; set; }

        //public string ConcurrencyStamp { get; set; }

        //public string PhoneNumber { get; set; }

        //public bool PhoneNumberConfirmed { get; set; }

        //public bool TwoFactorEnabled { get; set; }

        //public DateTimeOffset LockoutEnd { get; set; }

        //public bool LockoutEnabled { get; set; }

        //public int AccessFailedCount { get; set; }

        //Display roles
        public IEnumerable<SelectListItem> Roles
        {
            get; set;
        }

        //Collect roles
        public string RoleId { get; set; }

        public IEnumerable<SelectListItem> Userslist
        {
            get; set;
        }

        //Collect UserID
        public string UserId { get; set; }



    }
}
