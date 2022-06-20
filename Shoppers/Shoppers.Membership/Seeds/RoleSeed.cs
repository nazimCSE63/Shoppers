using Shoppers.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Membership.Seeds
{
    internal static class RoleSeed
    {
        internal static Role[] Roles
        {
            get
            {
                return new Role[]
                {
                    new Role{ Id = Guid.Parse("507F74F3-16C8-4084-9772-F017F64E3667"), Name = "SuperAdmin", NormalizedName = "SUPERADMIN", ConcurrencyStamp =  DateTime.Now.Ticks.ToString()  },
                    new Role{ Id = Guid.NewGuid(), Name = "StoreAdmin", NormalizedName = "STOREADMIN", ConcurrencyStamp =  DateTime.Now.Ticks.ToString()  },
                    new Role{ Id = Guid.NewGuid(), Name = "Customer", NormalizedName = "CUSTOMER", ConcurrencyStamp =  DateTime.Now.AddMinutes(1).Ticks.ToString()  }
                };
            }
        }
    }
}
