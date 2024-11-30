using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Users", Schema = "dbo")]
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
