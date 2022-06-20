using Microsoft.EntityFrameworkCore;
using Shoppers.Storage.Entities;

namespace Shoppers.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _context;

        public CustomerService(CustomerDbContext context)
        {
            _context = context;    
        }

        public string Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        public Customer GetById(string obj)
        {
            throw new NotImplementedException();
        }

        public void Save(Customer obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
