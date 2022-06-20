using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.UnitOfWorks;
using ProductEntity = Shoppers.Storage.Entities.Product;

namespace Shoppers.Storage.Services
{
    public class ProductService : IProductService
    {
        private readonly IStoreUnitOfWork _productUnitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IStoreUnitOfWork productUnitOfWork, IMapper mapper)
        {
            _productUnitOfWork = productUnitOfWork;
            _mapper = mapper;
        }
        public void CreateProduct(Product product)
        {
            var productEntity = new ProductEntity
            {
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                SKU = product.SKU,
                Size = product.Size,
                Color = product.Color,
                Quantity = product.Quantity,
                Price = product.Price,
                Image = product.Image,
                CategoryId = product.CategoryId
            };
            _productUnitOfWork.Products.Add(productEntity);
            _productUnitOfWork.Save();
        }

        public void DeleteProduct(int id)
        {
            _productUnitOfWork.Products.Remove(id);
            _productUnitOfWork.Save();

        }

        public void EditProduct(Product product)
        {
            var productEntity = _productUnitOfWork.Products.GetById(product.Id);
            productEntity.Name = product.Name;
            productEntity.ShortDescription = product.ShortDescription;
            productEntity.Description = product.Description;
            productEntity.SKU = product.SKU;
            productEntity.Size = product.Size;
            productEntity.Color = product.Color;
            productEntity.Price = product.Price;
            productEntity.Image = product.Image;

            //_mapper.Map(Product, ProductEntity);
            _productUnitOfWork.Save();
        }

        public Product GetProduct(int id)
        {
            var productEntity = _productUnitOfWork.Products.Get(x => x.Id == id, "Category").SingleOrDefault();
            var product = _mapper.Map<Product>(productEntity);
            return product;
        }

        public (int total, int totalDisplay, IList<Product> records) GetProducts(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = _productUnitOfWork.Products.GetDynamic(x => x.Name.Contains(searchText), orderBy, string.Empty, pageIndex, pageSize);
            IList<Product> products = new List<Product>();

            foreach (var item in result.data)
            {
                var product = _mapper.Map<Product>(item);
                products.Add(product);
            }
            return (result.total, result.totalDisplay, products);
        }

        public IList<Product> GetProducts()
        {
            var productEntities = _productUnitOfWork.Products.Get(x => x.Id < 1000, "Category").ToList();
            IList<Product> records = new List<Product>();
            foreach (var productEntity in productEntities)
            {
                var product = _mapper.Map<Product>(productEntity);
                records.Add(product);
            }
            return records;
        }
    }
}
