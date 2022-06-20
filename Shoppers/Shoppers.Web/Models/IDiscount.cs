namespace Shoppers.Web.Models;

public interface IDiscount
{
    public string ProductID { get; set; }
    public double PercentageSingle { get; set; }
    public double FixedAmountSingle{ get; set; }
    public static double PercentageTotal { get; set; }
    public static double FixedAmountTotal{ get; set; }
    public int productQuantity { get; set; } 
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }

    void CalculatePercentageSingle(double actualPrice,double percentage);
    void CalculateFixedAmountSingle(double actualPrice,double percentage);
    void CalculatePercentageTotal(double actualPrice, double percentage, double limitPrice);
    void CalculateFixedAmountTotal(double actualPrice, double percentage , double limitPrice);
    bool isExpire();


}