using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Shoppers.Storage.BusinessObjects;
using CategoryEntity =  Shoppers.Storage.Entities.Category;
using Shoppers.Storage.Repositories;
using Shoppers.Storage.Services;
using Shoppers.Storage.UnitOfWorks;

namespace Shoppers.Storage.Test.Services
{
    public class CategoryServicesTest
    {
        private AutoMock _mock;
        private Mock<IStoreUnitOfWork> _storeUnitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private ICategoryService _categoryService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock.Dispose();
        }
        [SetUp]
        public void Setup()
        {
            _storeUnitOfWorkMock = _mock.Mock<IStoreUnitOfWork>();
            _categoryRepositoryMock = _mock.Mock<ICategoryRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _categoryService = _mock.Create<CategoryService>();
        }
        [TearDown]
        public void TearDown()
        {
            _storeUnitOfWorkMock.Reset();
            _categoryRepositoryMock.Reset();
            _mapperMock.Reset();
        }
        [Test]
        //MethodName_WhichSituationCodeWillRun_ExpectedResult
        public void CreateCategory_void_CreateCategory()
        {
            //arrange

            var category = new Category
            {
                Id = 1,
                Description = "sdafgh",
                Name = "Category 1"
            };
            _storeUnitOfWorkMock.Setup(x => x.Categories).
                Returns(_categoryRepositoryMock.Object);

            _mapperMock.Setup(x => x.Map<CategoryEntity>(category)).
                Returns(new CategoryEntity() 
                {
                    Id = category.Id, 
                    Name = category.Name ,
                    Description = category.Description
                });

            _categoryRepositoryMock.Setup(x => x.
            Add(It.Is<CategoryEntity>(x => x.Name == category.Name))).Verifiable();
            _storeUnitOfWorkMock.Setup(x => x.Save()).Verifiable();

            //act
            _categoryService.CreateCategory(category);

            //assert
            _categoryRepositoryMock.VerifyAll();
            _categoryRepositoryMock.VerifyAll();
        }
    }
}