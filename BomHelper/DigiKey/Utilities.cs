using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BomHelper.DigiKey
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class FilterOption
    {
        public List<Value> Values { get; set; }
        public int ParameterId { get; set; }
        public string Parameter { get; set; }
    }

    public class LimitedTaxonomy
    {
        public List<LimitedTaxonomy> Children { get; set; }
        public int ProductCount { get; set; }
        public int NewProductCount { get; set; }
        public int ParameterId { get; set; }
        public string ValueId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }

    public class Manufacturer
    {
        public int ParameterId { get; set; }
        public string ValueId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }

    public class Packaging
    {
        public int ParameterId { get; set; }
        public string ValueId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }

    public class Parameter
    {
        public int ParameterId { get; set; }
        public string ValueId { get; set; }
        [JsonPropertyName("Parameter")]
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class SearchLocaleUsed
    {
        public string Site { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public string ShipToCountry { get; set; }
    }

    public class Series
    {
        public int ParameterId { get; set; }
        public string ValueId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }

    public class StandardPricing
    {
        public int BreakQuantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }

    public class Value
    {
        public string ValueId { get; set; }
        [JsonPropertyName("Value")]
        public string Name{ get; set; }
    }


}
