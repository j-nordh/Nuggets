using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace Json2Csv
{
    interface IParser
    {
        void Configure();
        IEnumerable<Page> Parse(JToken o);
    }

    class Page
    {
        public string Name { get; }
        public List<string> Headings { get; }
        public IEnumerable<string[]> Rows { get; set; } 

        public Page(string name)
        {
            Name = name;
            Headings = new List<string>();
        }
    }
}
