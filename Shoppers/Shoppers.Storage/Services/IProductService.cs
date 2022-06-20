using Shoppers.Storage.BusinessObjects;

namespace Shoppers.Storage.Services
{
    public interface IProductService
    {
        void CreateProduct(Product product);
        void DeleteProduct(int id);
        void EditProduct(Product product);
        Product GetProduct(int id);
        (int total, int totalDisplay, IList<Product> records) GetProducts(int pageIndex, int pageSize,
           string searchText, string orderBy);
        IList<Product> GetProducts();
    }
}
