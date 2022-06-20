using Autofac;
using Shoppers.Storage.DbContexts;
using Shoppers.Storage.Repositories;
using Shoppers.Storage.Services;
using Shoppers.Storage.UnitOfWorks;

namespace Shoppers.Storage
{
    public class StorageModule : Module
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;
    
        public StorageModule(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StorageDbContext>().AsSelf().
                WithParameter("connectionString", _connectionString).
                WithParameter("assemblyName", _assemblyName).
                InstancePerLifetimeScope();

            builder.RegisterType<StorageDbContext>().As<IStorageDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<StoreRepository>().As<IStoreRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StoreUnitOfWork>().As<IStoreUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StoreService>().As<IStoreService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileService>().As<IFileService>()
                .InstancePerLifetimeScope();


           /* builder.RegisterType<StoreUnitOfWork>().As<IStoreUnitOfWork>()
                .InstancePerLifetimeScope();*/
            


            builder.RegisterType<StoreUnitOfWork>().As<IStoreUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<InventoryService>().As<IInventoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>()
         .InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
