using Shoppers.Storage.BusinessObjects;

namespace Shoppers.Storage.Services
{
    public interface IStoreService
    {
        void CreateStore(Store store);
        void DeleteStore(int id);
        void EditStore(Store store);
        Store GetStore(int id);
        (int total, int totalDisplay, IList<Store> records) GetStores(int pageIndex, int pageSize,
           string searchText, string orderBy);
        IList<Store> GetStores();
    }
}
