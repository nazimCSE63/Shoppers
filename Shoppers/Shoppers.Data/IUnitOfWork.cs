namespace Shoppers.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
