using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System;
using UtilClasses.Extensions.DateTimes;
namespace Json2Csv.Parsers
{
    class EcoParser : IParser
    {
        public void Configure()
        { }

        public IEnumerable<Page> Parse(JToken o)
        {
            foreach (var tag in o.Children())
            {
                var p = new Page(tag["Name"].Value<string>());
                p.Headings.AddRange(new[] { "Date", "Value", "Consumption" });
                p.Rows = tag["DateValues"].Select(t => t.GetRow(("Date", v=>DateTime.Parse(v).ToSaneString()), "Value", "Consumption"));
                yield return p;
            }
        }
    }

    class Selector
    {
        public string Name { get; set; }
        public Func<string, string> Filter { get; set; }
        public static implicit operator Selector(string s) => new Selector() { Name=s, Filter=i=>i};
        public static implicit operator Selector((string s, Func<string,string> f) t)=>new Selector() { Name=t.s, Filter =t.f};
    }
    static class Extensions
    {
        public static string[] GetRow(this JToken t, params Selector[] ss) => ss.Select(s => s.Filter(t[s.Name].Value<string>())).ToArray();
    }
}
