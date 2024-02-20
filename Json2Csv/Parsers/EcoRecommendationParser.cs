using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UtilClasses.Extensions.DateTimes;
using UtilClasses.Extensions.Strings;

namespace Json2Csv.Parsers
{
    class EcoRecommendationParser : IParser
    {
        public void Configure()
        {
        }

        public IEnumerable<Page> Parse(JToken o)
        {
            var page = new Page("Recommendations");
            page.Headings.AddRange(new[] {"Timestamp", "Elpris", "FjV pris", "Recommendation"});
            page.Rows = o.Children().Select(r => new[]
            {
                r["issued"].Value<DateTime>().ToStartOfHour().AddHours(1).ToString(),
                GetUtilityPrice(r, 1),
                GetUtilityPrice(r, 2),
                r["processCall"]["output"]["hours"]["1"]["import"]["DistrictHeating"].Value<decimal>() > 0
                    ? "Fjärrvärme"
                    : "Värmepump"
            });
            yield return page;
        }

        private string GetUtilityPrice(JToken r,  int utilityType) =>
            r["processCall"]["input"]["prices"].Children().First(p => p["utilityType"].Value<int>()== utilityType)
                ["values"].Children().First().Value<decimal>().ToString();
    }
}