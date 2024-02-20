using System.Collections.Generic;
using System.Linq;

namespace SupplyChain.Dto
{
    public class SubQuery
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Query { get; set; }
        public SubQuery Clone() => new SubQuery { Name = Name, Alias = Alias, Query = Query};
    }
}
