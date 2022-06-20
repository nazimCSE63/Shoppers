using Shoppers.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Membership.Seeds
{
    internal static class SuperAdminRoleSeed
    {
        internal static UserRole adminUserRole
        {
            get
            {
                return new UserRole
                {
                    UserId =Guid.Parse("103EFF56-6FA7-4A42-848B-2CA7F4CD2610"),
                    RoleId =Guid.Parse("507F74F3-16C8-4084-9772-F017F64E3667")
                };
            }
        }
    }
}
