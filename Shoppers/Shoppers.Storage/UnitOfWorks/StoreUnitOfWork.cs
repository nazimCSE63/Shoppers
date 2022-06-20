using Microsoft.EntityFrameworkCore;
using Shoppers.Data;
using Shoppers.Storage.DbContexts;
using Shoppers.Storage.Repositories;

namespace Shoppers.Storage.UnitOfWorks
{
    public class StoreUnitOfWork : UnitOfWork, IStoreUnitOfWork
    {
        public IStoreRepository Stores { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public StoreUnitOfWork(IStorageDbContext dbContext,
            IStoreRepository storeRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository) : base((DbContext)dbContext)
        {
            Stores = storeRepository;
            Products = productRepository;
            Categories = categoryRepository;
            Orders = orderRepository;
        }

    }
}
