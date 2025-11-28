using Microsoft.AspNetCore.Identity;
using System;

namespace Nafaa.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string NationalId { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Guid? CharityId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
