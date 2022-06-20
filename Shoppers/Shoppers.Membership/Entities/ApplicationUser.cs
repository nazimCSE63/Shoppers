using Microsoft.AspNetCore.Identity;
using System;

namespace Shoppers.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? AdminName { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        // if shop user then they'll have it
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
