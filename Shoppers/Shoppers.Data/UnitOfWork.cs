using Microsoft.EntityFrameworkCore;

namespace Shoppers.Data
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext?.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
