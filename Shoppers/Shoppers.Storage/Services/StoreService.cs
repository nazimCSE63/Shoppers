using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.UnitOfWorks;
using StoreEntity = Shoppers.Storage.Entities.Store;

namespace Shoppers.Storage.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreUnitOfWork _storeUnitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IStoreUnitOfWork storeUnitOfWork, IMapper mapper)
        {
            _storeUnitOfWork = storeUnitOfWork;
            _mapper = mapper;
        }
        public void CreateStore(Store store)
        {
            // var storeEntity = _mapper.Map<StoreEntity>(store);
            var storeEntity = new StoreEntity
            {
                Name = store.Name,
                Banner = store.Banner,
                Logo = store.Logo,
            };
            _storeUnitOfWork.Stores.Add(storeEntity);
            _storeUnitOfWork.Save();
        }

        public void DeleteStore(int id)
        {
            _storeUnitOfWork.Stores.Remove(id);
            _storeUnitOfWork.Save();

        }

        public void EditStore(Store store)
        {
            var storeEntity = _storeUnitOfWork.Stores.GetById(store.Id);
            storeEntity.Name = store.Name;
            storeEntity.Banner = store.Banner;
            storeEntity.Logo = store.Logo;

            //_mapper.Map(store, storeEntity);
            _storeUnitOfWork.Save();
        }

        public Store GetStore(int id)
        {
            var storeEntity = _storeUnitOfWork.Stores.GetById(id);
            var store = _mapper.Map<Store>(storeEntity);
            return store;
        }

        public (int total, int totalDisplay, IList<Store> records) GetStores(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = _storeUnitOfWork.Stores.GetDynamic(x => x.Name.Contains(searchText), orderBy, string.Empty, pageIndex, pageSize);
            IList<Store> courses = new List<Store>();

            foreach (var item in result.data)
            {
                var course = _mapper.Map<Store>(item);
                courses.Add(course);
            }
            return (result.total, result.totalDisplay, courses);
        }

        public IList<Store> GetStores()
        {
            var storeEntities = _storeUnitOfWork.Stores.GetAll();
            IList<Store> records = new List<Store>();
            foreach (var storeEntity in storeEntities)
            {
                var store = _mapper.Map<Store>(storeEntity);
                records.Add(store);
            }
            return records;
        }
    }
}
