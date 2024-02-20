using UtilClasses.Extensions.Enumerables;

namespace BomHelper.Dto;

public class Component
{
    public string Name { get; set; }
    public List<string> Urls { get; set; }
    //public string Url { get; set; }
    public List<PriceInfo> Prices { get; set; }
    public string Footprint { get; set; }
    public string Description { get; set; }
    public string PartNumber { get; set; }
    public string ProductId { get; set; }
    public bool NoCsv { get; set; }
}

public class PriceInfo
{
    public List<PricePoint> Prices { get; set; } = new();
    public int InStock { get; set; }
    public DateTime? Updated { get; set; }
    public string Source { get; set; }
}

public static class PriceInfoExtensions
{
    public static PriceInfo GetBest(this IEnumerable<PriceInfo> prices, int needed = 1) 
        => prices?.NotNull().Where(pi => pi.InStock > needed).OrderBy(pi => GetCost(pi, needed)).FirstOrDefault();

    public static decimal? GetCost(this Component c, int needed)
    {
        var price = c.Prices.GetBest(needed) ?? c.Prices.GetBest(0);
        return price?.GetPricePoint(needed)?.Price * needed;
    }


    public static decimal? GetCost(this PriceInfo p, int needed)
    {
        var point = p.GetPricePoint(needed);
        return point?.Price * needed;
    }

    public static PricePoint GetPricePoint(this Component c, int needed) => c.Prices.GetBest(needed).GetPricePoint(needed);
    public static PricePoint GetPricePoint(this PriceInfo p, int needed)
    {
        if ((p?.Prices).IsNullOrEmpty())
            return null;
        //var price = 
            return p.Prices.Where(p => p.BreakPoint <= needed).OrderBy(p => p.BreakPoint).LastOrDefault();
    }
}