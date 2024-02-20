namespace BomHelper.DigiKey;

public class KeywordResult
{
    public LimitedTaxonomy LimitedTaxonomy { get; set; }
    public List<FilterOption> FilterOptions { get; set; }
    public List<Product> Products { get; set; }
    public int ProductsCount { get; set; }
    public int ExactManufacturerProductsCount { get; set; }
    public List<object> ExactManufacturerProducts { get; set; }
    public object ExactDigiKeyProduct { get; set; }
    public SearchLocaleUsed SearchLocaleUsed { get; set; }
}