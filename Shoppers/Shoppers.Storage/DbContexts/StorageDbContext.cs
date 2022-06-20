using Microsoft.EntityFrameworkCore;
using Shoppers.Storage.Entities;

namespace Shoppers.Storage.DbContexts
{
    public class StorageDbContext : DbContext, IStorageDbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public StorageDbContext(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_assemblyName));
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(s => s.Category)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
