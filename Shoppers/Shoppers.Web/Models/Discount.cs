namespace Shoppers.Web.Models

{
    public class Discount : IDiscount
    {
        public string? ProductID { get; set; }
        public double PercentageSingle { get; set; }
        public double FixedAmountSingle { get; set; }
        public static double PercentageTotal { get; set; }
        public static double FixedAmountTotal { get; set; }
        public int productQuantity { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool ExpireStatus { get; set; }  

        public void CalculatePercentageSingle(double actualPrice, double percentage)
        {
            if (PercentageTotal == null && FixedAmountTotal==null && FixedAmountSingle ==null)
            {
                PercentageSingle = actualPrice * ((100 - percentage) / 100);
            }
        }

        public void CalculateFixedAmountSingle(double actualPrice, double fixedpriceDiscount)
        {
            if (PercentageTotal == null && FixedAmountTotal == null && PercentageSingle == null)
            {
                if(fixedpriceDiscount < actualPrice)
                {
                    FixedAmountSingle = actualPrice - fixedpriceDiscount;
                }         
            }
        }

        public void CalculatePercentageTotal(double actualPrice, double percentage, double limitPrice)
        {
            if (PercentageTotal == null && FixedAmountSingle == null && FixedAmountTotal==null && actualPrice > limitPrice)
            {
                PercentageTotal = actualPrice * ((100 - percentage) / 100);
            }

        }

        public void CalculateFixedAmountTotal(double actualPrice, double fixedPrice , double limitPrice)
        {
            if(PercentageTotal == null && FixedAmountSingle == null && PercentageSingle == null && actualPrice < limitPrice)
            {
                FixedAmountTotal = actualPrice - fixedPrice; 
            }
        }

        public bool isExpire()
        {
            bool flag = false;

            if(productQuantity < 1)
            {
                flag = true;            
            }
            if(DateTime.Now > endDate)
            {
                flag = true;
            }

            return flag;
        }
    }
}
