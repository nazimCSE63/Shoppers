using Autofac;
using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class CategoryCreateModel
    {
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryCreateModel()
        {

        }
        public CategoryCreateModel(ILifetimeScope scope, 
            ICategoryService categoryService,
            IMapper mapper)
        {
            _scope = scope;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public void CreateCategory()
        {
            var category = _mapper.Map<Category>(this);
            _categoryService.CreateCategory(category);
        }
        public void Resolve(ILifetimeScope scope)
        {
            _categoryService = scope.Resolve<ICategoryService>();
            _mapper = scope.Resolve<IMapper>();
        }
    }
}
