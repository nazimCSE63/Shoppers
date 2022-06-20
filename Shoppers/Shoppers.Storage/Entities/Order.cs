using Shoppers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoppers.Storage.Entities
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Status { get; set; }
    }
}
