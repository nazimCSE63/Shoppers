using Autofac;
using AutoMapper;
using Shoppers.Storage.Services;

using ProductBO = Shoppers.Storage.BusinessObjects.Product;


namespace Shoppers.Web.Areas.Admin.Models
{
    public class ProductEditModel
    {
        private IProductService _productService;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IFileService _fileService;


        public ProductEditModel()
        {

        }
        public ProductEditModel(IProductService productService, 
            IMapper mapper, ILifetimeScope scope,
            IFileService fileService)
        {
            _productService = productService;
            _mapper = mapper;
            _scope = scope;
            _fileService = fileService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _productService = scope.Resolve<IProductService>();
            _mapper = scope.Resolve<IMapper>();
            _fileService = scope.Resolve<IFileService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public IFormFile ImageFile { get; set; }

        public void Load(int id)
        {
            var data = _productService.GetProduct(id);
            Id = data.Id;
            Name = data.Name;
            ShortDescription = data.ShortDescription;
            Description = data.Description;
            SKU = data.SKU;
            Size = data.Size;
            Color = data.Color;
            Price = data.Price;
            Image = data.Image;
        }
        public void EditProduct()
        {
            _fileService.DeleteFile(_productService.GetProduct(Id).Image);
            var product = new ProductBO
            {
                Id = Id,
                Name = Name,
                ShortDescription = ShortDescription,
                Description = Description,
                SKU = SKU,
                Size = Size,
                Color = Color,
                Price = Price,
                Image = _fileService.SaveFile(ImageFile),
            };
            _productService.EditProduct(product);
        }
        
    }
}
