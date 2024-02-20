using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BomHelper.Dto;
using Newtonsoft.Json;
using UtilClasses;
using UtilClasses.Extensions.Doubles;
using UtilClasses.Extensions.Strings;

namespace BomHelper.Farnell
{
    internal class PriceFetcher : IPriceFetcher
    {
        private readonly string _url;
        private readonly DictionaryOic<string> _partNumbers;

        public PriceFetcher(string url, DictionaryOic<string> partNumbers)
        {
            _url = url;
            _partNumbers = partNumbers;
        }
        public async Task<PriceInfo> GetPrice()
        {
            var part = _url.SubstringAfter(".com/").SubstringAfter("/").SubstringBefore("/");
            var client = new HttpClient();
            var ps = HttpUtility.ParseQueryString("");
            ps["term"] = $"manuPartNum:{part}";
            ps["storeInfo.id"] = "se.farnell.com";
            ps["resultsSettings.responseGroup"] = "medium";
            ps["callInfo.responseDataFormat"] = "JSON";
            ps["callinfo.apiKey"] = "aqy8wwfdytcx8mcs5r62bpey";
            ps["resultsSettings.numberOfResults"] = "1";
            var uri = new Uri($"https://api.element14.com/catalog/products?{ps}");
            var msg = await client.GetAsync(uri);
            var content = await msg.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<Root>(content).PartNumberSearchResult;
            if ((res?.NumberOfResults ??0)== 0)
                return null;
            var prod = res.Products.Where(p=>p.Stock.Level >0).OrderByDescending(p=>p.Stock.Level).First();
            var ret = new PriceInfo()
            {
                InStock = prod.Stock.Level,
                Source = _url,
                Updated = DateTime.UtcNow,
                Prices = prod.Prices.Select(p => new PricePoint() {Price = p.Cost.AsDecimal(), BreakPoint = p.From})
                    .ToList()
            };
            _partNumbers[_url] = prod.TranslatedManufacturerPartNumber;
            return ret;
        }
    }// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
}
