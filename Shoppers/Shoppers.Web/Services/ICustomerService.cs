using Shoppers.Storage.Entities;

namespace Shoppers.Web.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();

        Customer GetById(string obj);

        void Save(Customer obj);

        void Update(Customer obj);

        string Delete(string id);
    }
}
