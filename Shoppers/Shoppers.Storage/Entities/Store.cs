using Shoppers.Data;

namespace Shoppers.Storage.Entities
{
    public class Store : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }
        public string? Banner { get; set; }
        public string? Logo { get; set; }
        public string? ContactInfo { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
