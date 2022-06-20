using Shoppers.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoppers.Storage.Entities
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? StoreId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Category Category { get; set; }
    }
}
