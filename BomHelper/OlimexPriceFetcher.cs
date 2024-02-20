using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BomHelper.Dto;
using UtilClasses.Extensions.Decimals;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Integers;
using UtilClasses.Extensions.Strings;

namespace BomHelper
{
    internal class OlimexPriceFetcher:IPriceFetcher

    {
        private readonly string _url;
        private readonly decimal _exchangeRate;

        public OlimexPriceFetcher(string url, decimal exchangeRate)
        {
            _url = url;
            _exchangeRate = exchangeRate;
        }
        public async Task<PriceInfo> GetPrice()
        {
            var c = new HttpClient();
            var response = await c.GetAsync(_url);
            var content =await  response.Content.ReadAsStringAsync();
            content = content.SubstringAfter("<table class=\"pricing\">").SubstringBefore("</table>");
            var rows = content.Split("</tr>");
            var ret = new PriceInfo() {InStock = 99999, Source = _url, Updated = DateTime.UtcNow};
            foreach (var row in rows)
            {
                if (!row.ContainsOic("price")) continue;
                var bp = 1;
                if (row.Contains("eligibleQuantity"))
                    bp = row.SubstringAfter("eligibleQuantity").SubstringAfter(">").SubstringBefore("-").Trim().AsInt();
                var price = row.SubstringAfter("itemprop=\"price\"").SubstringAfter(">").SubstringBefore("<")
                    .AsDecimal() * _exchangeRate;
                
                ret.Prices.Add(new PricePoint(){BreakPoint = bp, Price = price});
            }
            return ret;
        }
    }
}
