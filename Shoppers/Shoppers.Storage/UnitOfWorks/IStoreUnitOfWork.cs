using Shoppers.Data;
using Shoppers.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.UnitOfWorks
{
    public interface IStoreUnitOfWork: IUnitOfWork
    {
        IStoreRepository Stores { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
    }
}
