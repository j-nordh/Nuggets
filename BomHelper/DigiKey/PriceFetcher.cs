//using ApiClient;
//using ApiClient.Models;
//using ApiClient.OAuth2;
//using BomHelper.Dto;
//using Newtonsoft.Json;
//using UtilClasses;
//using UtilClasses.Extensions.Doubles;
//using UtilClasses.Extensions.Strings;

//namespace BomHelper.DigiKey
//{
//    internal class PriceFetcher : IPriceFetcher
//    {
//        private readonly string _url;
//        private readonly DictionaryOic<string> _partNumbers;

//        public PriceFetcher(string url, DictionaryOic<string> partNumbers)
//        {
//            _url = url;
//            _partNumbers = partNumbers;
//        }
//        public async Task<PriceInfo> GetPrice()
//        {
//            var settings = ApiClientSettings.CreateFromConfigFile();
//            if (settings.ExpirationDateTime < DateTime.UtcNow)
//            {
//                var svc = new OAuth2Service(settings);
//                var accessToken = await svc.RefreshTokenAsync();
//                if (accessToken.IsError)
//                {
//                    throw new InvalidDataException("Refresh token invalid");
//                }
//                settings.UpdateAndSave(accessToken);
//            }

//            var id = _url;
//            if (id.StartsWithOic("http"))
//            {
//                id = id.SubstringBeforeLast("/").SubstringAfterLast("/");
//            }


//            var client = new ApiClientService(settings);
//            var json = await client.KeywordSearch(id);
//            var res = JsonConvert.DeserializeObject<KeywordResult>(json);
//            var prod = res.Products.FirstOrDefault(p => p.MinimumOrderQuantity == 1);
//            if (null == prod) return null;
//            _partNumbers[_url] = prod.ManufacturerPartNumber;
//            return new PriceInfo
//            {
//                InStock = prod.QuantityAvailable,
//                Prices = prod.StandardPricing.Select(sp => new PricePoint()
//                { BreakPoint = sp.BreakQuantity, Price = sp.UnitPrice.AsDecimal() }).ToList(),
//                Updated = DateTime.UtcNow,
//                Source = _url
//            };
//        }
//    }
//}
