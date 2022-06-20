using Microsoft.EntityFrameworkCore;
using Shoppers.Data;
using Shoppers.Storage.DbContexts;
using Shoppers.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Repositories
{
    public class StoreRepository : Repository<Store, int>, IStoreRepository
    {
        public StoreRepository(IStorageDbContext context) : base((DbContext)context)
        {
        }
    }
}
