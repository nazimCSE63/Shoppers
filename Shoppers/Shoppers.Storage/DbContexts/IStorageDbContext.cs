using Microsoft.EntityFrameworkCore;
using Shoppers.Storage.Entities;

namespace Shoppers.Storage.DbContexts
{
    public interface IStorageDbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }

    }
}
