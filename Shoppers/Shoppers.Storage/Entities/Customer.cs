using Shoppers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Entities
{
    public class Customer : IEntity<int>
    {
        public int Id { get ; set ; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string getName()
        {
            return FirstName + " " + LastName;
        }
    }
}
