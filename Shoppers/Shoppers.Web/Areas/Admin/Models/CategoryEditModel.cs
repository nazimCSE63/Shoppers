using Autofac;
using AutoMapper;
using Shoppers.Storage.BusinessObjects;
using Shoppers.Storage.Services;

namespace Shoppers.Web.Areas.Admin.Models
{
    public class CategoryEditModel
    {
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryEditModel()
        {

        }
        public CategoryEditModel(ILifetimeScope scope, 
            ICategoryService categoryService,
            IMapper mapper)
        {
            _scope = scope;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public void Load(int id)
        {
            var category = _categoryService.GetCategory(id);
            _mapper.Map(category, this);
        }
        public void EditCategory()
        {
            var category = _mapper.Map<Category>(this);
            _categoryService.EditCategory(category);
        }
        public void Resolve(ILifetimeScope scope)
        {
            _categoryService = scope.Resolve<ICategoryService>();
            _mapper = scope.Resolve<IMapper>();
        }
    }
}
