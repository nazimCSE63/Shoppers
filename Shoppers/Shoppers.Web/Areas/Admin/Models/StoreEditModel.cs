using Autofac;
using AutoMapper;
using Shoppers.Storage.Services;

using StoreBO = Shoppers.Storage.BusinessObjects.Store;


namespace Shoppers.Web.Areas.Admin.Models
{
    public class StoreEditModel
    {
        private IStoreService _storeService;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        private IFileService _fileService;


        public StoreEditModel()
        {

        }
        public StoreEditModel(IStoreService storeService, 
            IMapper mapper, ILifetimeScope scope,
            IFileService fileService)
        {
            _storeService = storeService;
            _mapper = mapper;
            _scope = scope;
            _fileService = fileService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _storeService = scope.Resolve<IStoreService>();
            _mapper = scope.Resolve<IMapper>();
            _fileService = scope.Resolve<IFileService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile BannerFile { get; set; }
        public IFormFile LogoFile { get; set; }
        public string? Banner { get; set; }
        public  string? Logo { get; set; }

        public void Load(int id)
        {
            var data = _storeService.GetStore(id);
            Id = data.Id;
            Name = data.Name;
            Banner = data.Banner;
            Logo = data.Logo;
        }
        public void EditStore()
        {
            //var store = _mapper.Map<StoreBO>(this);
            _fileService.DeleteFile(_storeService.GetStore(Id).Banner);
            _fileService.DeleteFile(_storeService.GetStore(Id).Logo);
            var store = new StoreBO
            {
                Id = Id,
                Name = Name,
                Banner = _fileService.SaveFile(BannerFile),
                Logo = _fileService.SaveFile(LogoFile),
            };
            _storeService.EditStore(store);
        }
        
    }
}
