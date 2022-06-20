using Autofac;
using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;

namespace Shoppers.Web.Areas.Store.Models
{
    public class FrontProductListModel
    {
        private IProductService _productService;
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }
        public FrontProductListModel(IProductService productService,
            ILifetimeScope scope,
            ICategoryService categoryService,
            IMapper mapper, 
            IList<Product> products,
            IList<Category> categories)
        {
            _productService = productService;
            _scope = scope;
            _categoryService = categoryService;
            _mapper = mapper;
            Products = products;
            Categories = categories;
        }

        public void  LoadProducts()
        {
            var productBOList = _productService.GetProducts();

            foreach (var product in productBOList)
            {
                var p = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    Image = product.Image,
                    Price = product.Price,
                };
                Products.Add(p);
            };
            Categories =  _categoryService.GetAllCategory();
        }

    }
}
