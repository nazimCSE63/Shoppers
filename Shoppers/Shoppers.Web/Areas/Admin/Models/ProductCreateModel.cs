using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;
using ProductBO = Shoppers.Storage.BusinessObjects.Product;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class ProductCreateModel
    {
        private IProductService _productService;
        private IMapper _mapper;

        private ILifetimeScope _scope;
        private IFileService _fileService;
        //private IWebHostEnvironment _webHostEnvironment;

        public ProductCreateModel()
        {

        }
        public ProductCreateModel(IProductService productService, IMapper mapper,
            ILifetimeScope scope,
            IFileService fileService)
        {
            _productService = productService;
            _mapper = mapper;
            _scope = scope;
            _fileService = fileService;
        }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public IList<Category> Categories { set; get; }


        public void CreateProduct()
        {
            //var Product = _mapper.Map<ProductBO>(this);
            var product = new ProductBO
            {
                Name = Name,
                ShortDescription = ShortDescription,
                Description = Description,
                SKU = SKU,
                Size = Size,
                Color = Color,
                Quantity=Quantity,
                Price = Price,
                CategoryId = CategoryId,
                Image = _fileService.SaveFile(ImageFile)
            };
            _productService.CreateProduct(product);
        }
        public void Resolve(ILifetimeScope scope)
        {
            _productService = scope.Resolve<IProductService>();
            _mapper = scope.Resolve<IMapper>();
            _fileService = scope.Resolve<IFileService>();
        }
    }
}
