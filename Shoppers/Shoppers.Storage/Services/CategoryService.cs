using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using CategoryEntity= Shoppers.Storage.Entities.Category;
using Shoppers.Storage.UnitOfWorks;

namespace Shoppers.Storage.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IStoreUnitOfWork _storeUnitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IStoreUnitOfWork storeUnitOfWork, IMapper mapper)
        {
            _storeUnitOfWork = storeUnitOfWork;
            _mapper = mapper;
        }
        public void CreateCategory(Category category)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(category);
            _storeUnitOfWork.Categories.Add(categoryEntity);
            _storeUnitOfWork.Save();
        }

        public void DeleteCategory(int id)
        {
            _storeUnitOfWork.Categories.Remove(id);
            _storeUnitOfWork.Save();
        }

        public void EditCategory(Category category)
        {
            var categoryEntity = _storeUnitOfWork.Categories.GetById(category.Id);
            _mapper.Map(category, categoryEntity);
            _storeUnitOfWork.Save();
        }

        public IList<Category> GetAllCategory()
        {
            var categoryEntities = _storeUnitOfWork.Categories.GetAll();

            var categories = new List<Category>();
            foreach (var category in categoryEntities)
            {
                categories.Add(_mapper.Map<Category>(category));
            }
            return categories;
        }

        public (int total, int totalDisplay, IList<Category> records) GetCategories(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            var result = _storeUnitOfWork.Categories.GetDynamic(x => x.Name.Contains(searchText), orderBy, string.Empty, pageIndex, pageSize);
            var categories = new List<Category>();

            foreach (var item in result.data)
            {
                var category = _mapper.Map<Category>(item);
                categories.Add(category);
            }
            return (result.total, result.totalDisplay, categories);
        }

        public Category GetCategory(int id)
        {
            var categoryEntity = _storeUnitOfWork.Categories.GetById(id);
            var category = _mapper.Map<Category>(categoryEntity);
            return category;
        }
  
    }
}
