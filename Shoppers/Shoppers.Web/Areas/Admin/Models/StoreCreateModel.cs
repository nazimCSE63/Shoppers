using Autofac;
using AutoMapper;
using Shoppers.Storage.Services;
using StoreBO = Shoppers.Storage.BusinessObjects.Store;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class StoreCreateModel
    {
        private IStoreService _storeService;
        private IMapper _mapper;

        private ILifetimeScope _scope;
        private IFileService _fileService;
        //private IWebHostEnvironment _webHostEnvironment;

        public StoreCreateModel()
        {

        }
        public StoreCreateModel(IStoreService storeService, IMapper mapper,
            ILifetimeScope scope,
            IFileService fileService)
        {
            _storeService = storeService;
            _mapper = mapper;
            _scope = scope;
            _fileService = fileService;
        }
        public string? Name { get; set; }
        public IFormFile BannerFile { get; set; }
        public IFormFile LogoFile { get; set; }
        public string? Logo { get; set; }
        public string? Banner { get; set; }

        public void CreateStore()
        {
            //var store = _mapper.Map<StoreBO>(this);
            var store = new StoreBO
            {
                Name = Name,
                Banner = _fileService.SaveFile(BannerFile),
                Logo = _fileService.SaveFile(LogoFile)
            };
            _storeService.CreateStore(store);
        }
        public void Resolve(ILifetimeScope scope)
        {
            _storeService = scope.Resolve<IStoreService>();
            _mapper = scope.Resolve<IMapper>();
            _fileService = scope.Resolve<IFileService>();
        }
/*        private string SaveFile(IFormFile image)
        {
            var uniqueFileName = GetUniqueFileName(image.FileName);
            string fileUrl = "/uploads/" + uniqueFileName;
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploads, uniqueFileName);
            image.CopyTo(new FileStream(filePath, FileMode.Create));
            return fileUrl;
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }*/
    }
}
