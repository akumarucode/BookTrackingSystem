using System.ComponentModel.DataAnnotations;

namespace Microsoft.AspNetCore.Identity
{
    public class IdentityUserRole
    {
        [Key]
        public string UserId { get; set; }
        public string? RoleId { get; set; }

    }
}
