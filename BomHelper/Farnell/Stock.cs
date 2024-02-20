using Newtonsoft.Json;

namespace BomHelper.Farnell;

public class Stock
{
    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("leastLeadTime")]
    public int LeastLeadTime { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("shipsFromMultipleWarehouses")]
    public bool ShipsFromMultipleWarehouses { get; set; }

    [JsonProperty("breakdown")]
    public List<Breakdown> Breakdown { get; set; }

    [JsonProperty("regionalBreakdown")]
    public List<RegionalBreakdown> RegionalBreakdown { get; set; }
}

public class RegionalBreakdown
{
    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("leastLeadTime")]
    public int LeastLeadTime { get; set; }

    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("warehouse")]
    public string Warehouse { get; set; }

    [JsonProperty("shipsFromMultipleWarehouses")]
    public bool ShipsFromMultipleWarehouses { get; set; }
}
public class Breakdown
{
    [JsonProperty("inv")]
    public int Inv { get; set; }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("lead")]
    public int Lead { get; set; }

    [JsonProperty("warehouse")]
    public string Warehouse { get; set; }
}