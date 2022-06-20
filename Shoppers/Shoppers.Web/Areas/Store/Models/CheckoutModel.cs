namespace Shoppers.Web.Areas.Store.Models
{
    public class CheckoutModel
    {
        public IList<CartItem> Items { get; set; }
        public string ShppingAddress { get; set; }
    }
}
