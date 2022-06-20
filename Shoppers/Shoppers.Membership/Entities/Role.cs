using Microsoft.AspNetCore.Identity;
using System;

namespace Shoppers.Membership.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
            : base()
        {
        }

        public Role(string roleName)
            : base(roleName)
        {
        }
    }
}
