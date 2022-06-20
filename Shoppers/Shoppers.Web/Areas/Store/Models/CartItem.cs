using Shoppers.Storage.BusinessObjects;

namespace Shoppers.Web.Areas.Store.Models
{
    public class CartItem
    {
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}
