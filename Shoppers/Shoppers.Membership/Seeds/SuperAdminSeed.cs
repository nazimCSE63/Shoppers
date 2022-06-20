using Microsoft.AspNetCore.Identity;
using Shoppers.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Membership.Seeds
{
    internal class SuperAdminSeed
    {     
        
        internal static ApplicationUser superAdmin
        {
            get
            {
                return SuperAdminSeed.AppUser();
            }
        }

        internal static ApplicationUser AppUser()
        {
            var user = new ApplicationUser
            {
                Id = Guid.Parse("103EFF56-6FA7-4A42-848B-2CA7F4CD2610"),
                UserName = "apptest.fatema@gmail.com",
                NormalizedUserName = "apptest.fatema@gmail.com".ToUpper(),
                Email = "apptest.fatema@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "apptest.fatema@gmail.com".ToUpper(),
                ConcurrencyStamp = DateTime.Now.Ticks.ToString(),
                SecurityStamp = "BEF27F7F-B574-4BBE-AD31-B2EBC0E24816"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "123456");
            return user;
        }

        
    }
    
}
