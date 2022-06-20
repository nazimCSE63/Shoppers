using Autofac;
using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;

namespace Shoppers.Web.Areas.Store.Models
{
    public class ProductDetailsModel
    {
        private IProductService _productService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public int Id { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Category Category { get; set; }

        public ProductDetailsModel(IProductService productService,
            ILifetimeScope scope,
            IMapper mapper)
        {
            _productService = productService;
            _scope = scope;
            _mapper = mapper;
        }
        public void Load(int id)
        {
            var product = _productService.GetProduct(id);
            _mapper.Map(product, this);
        }
    }
}
