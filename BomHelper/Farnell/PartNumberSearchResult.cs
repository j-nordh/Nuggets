using Newtonsoft.Json;

namespace BomHelper.Farnell;

public class Root
{
    [JsonProperty("manufacturerPartNumberSearchReturn")]
    public PartNumberSearchResult PartNumberSearchResult { get; set; }
}
public class PartNumberSearchResult
{
    [JsonProperty("numberOfResults")]
    public int NumberOfResults { get; set; }

    [JsonProperty("products")]
    public List<Product> Products { get; set; }
}