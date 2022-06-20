using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Shoppers.Storage.BusinessObjects;
using CategoryEntity =  Shoppers.Storage.Entities.Product;
using Shoppers.Storage.Repositories;
using Shoppers.Storage.Services;
using Shoppers.Storage.UnitOfWorks;

namespace Shoppers.Storage.Test.Services
{
    public class ProductServicesTest
    {
        private AutoMock _mock;
        private Mock<IStoreUnitOfWork> _storeUnitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IProductRepository> _categoryRepositoryMock;
        private IProductService _categoryService;

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
            _categoryRepositoryMock = _mock.Mock<IProductRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _categoryService = _mock.Create<ProductService>();
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

        

            _categoryRepositoryMock.Setup(x => x.
            Add(It.Is<CategoryEntity>(x => x.Id == 2))).Verifiable();
            _storeUnitOfWorkMock.Setup(x => x.Save()).Verifiable();

            //act
            _categoryService.GetProduct(2);

            //assert
            _categoryRepositoryMock.VerifyAll();
            _categoryRepositoryMock.VerifyAll();
        }
    }
}