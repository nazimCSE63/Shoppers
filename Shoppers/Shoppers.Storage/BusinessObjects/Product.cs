namespace Shoppers.Storage.BusinessObjects
{
    public class Product
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
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Category Category { get; set; }
    }
}
