using BomHelper.DigiKey;
using UtilClasses.Extensions.Doubles;

namespace BomHelper.Dto;

public class PricePoint
{
    public decimal Price { get; set; }
    public int BreakPoint { get; set; }
    public PricePoint(){}

    public PricePoint(StandardPricing sp)
    {
        Price = sp.UnitPrice.AsDecimal();
        BreakPoint = sp.BreakQuantity;
    }

    public override string ToString() => $"{Price} (min {BreakPoint})";
}