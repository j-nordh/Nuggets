using BomHelper.Dto;
using UtilClasses.Extensions.Decimals;
using UtilClasses.Extensions.Strings;

namespace BomHelper
{
    internal class CurrencyPriceFetcher : IPriceFetcher
    {
        private readonly string _url;

        public decimal ExchangeRate { get; set; }

        public CurrencyPriceFetcher(string url, decimal exchangeRate)
        {
            _url = url;
            ExchangeRate = exchangeRate;
        }

        public async Task<PriceInfo> GetPrice()
        {
            await Task.FromResult(0);
            var p = _url.SubstringAfter(":").AsDecimal() * ExchangeRate;
            return new()
            {
                Prices = new List<PricePoint> { new() { Price = p, BreakPoint = 1 } },
                InStock = 99999,
                Updated = DateTime.UtcNow,
                Source = _url
            };
        }
    }
}
