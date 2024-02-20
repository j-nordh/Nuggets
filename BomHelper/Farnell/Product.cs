using Newtonsoft.Json;

namespace BomHelper.Farnell;

public class Product
{
    [JsonProperty("sku")]
    public string Sku { get; set; }

    [JsonProperty("displayName")]
    public string DisplayName { get; set; }

    [JsonProperty("productStatus")]
    public string ProductStatus { get; set; }

    [JsonProperty("rohsStatusCode")]
    public string RohsStatusCode { get; set; }

    [JsonProperty("packSize")]
    public int PackSize { get; set; }

    [JsonProperty("unitOfMeasure")]
    public string UnitOfMeasure { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("datasheets")]
    public List<Datasheet> Datasheets { get; set; }

    [JsonProperty("prices")]
    public List<Price> Prices { get; set; }

    [JsonProperty("vendorId")]
    public string VendorId { get; set; }

    [JsonProperty("vendorName")]
    public string VendorName { get; set; }

    [JsonProperty("brandName")]
    public string BrandName { get; set; }

    [JsonProperty("translatedManufacturerPartNumber")]
    public string TranslatedManufacturerPartNumber { get; set; }

    [JsonProperty("translatedMinimumOrderQuality")]
    public int TranslatedMinimumOrderQuality { get; set; }

    [JsonProperty("stock")]
    public Stock Stock { get; set; }

    [JsonProperty("comingSoon")]
    public bool ComingSoon { get; set; }

    [JsonProperty("inventoryCode")]
    public int InventoryCode { get; set; }

    [JsonProperty("nationalClassCode")]
    public object NationalClassCode { get; set; }

    [JsonProperty("publishingModule")]
    public object PublishingModule { get; set; }

    [JsonProperty("vatHandlingCode")]
    public string VatHandlingCode { get; set; }

    [JsonProperty("releaseStatusCode")]
    public int ReleaseStatusCode { get; set; }

    [JsonProperty("isSpecialOrder")]
    public bool IsSpecialOrder { get; set; }

    [JsonProperty("isAwaitingRelease")]
    public bool IsAwaitingRelease { get; set; }

    [JsonProperty("reeling")]
    public bool Reeling { get; set; }

    [JsonProperty("discountReason")]
    public int DiscountReason { get; set; }
}
public class Datasheet
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}
public class Price
{
    [JsonProperty("to")]
    public int To { get; set; }

    [JsonProperty("from")]
    public int From { get; set; }

    [JsonProperty("cost")]
    public double Cost { get; set; }
}