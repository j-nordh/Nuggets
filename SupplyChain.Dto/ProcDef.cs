using System.Collections.Generic;
using System.Linq;

namespace SupplyChain.Procs
{
    public class ProcDef 
    {
        public string SpName;
        public string Name;
        public List<CallDef> Calls;
        public ColFields ColFields { get; set; }

        public ProcDef(string spName, string name)
        {
            SpName = spName;
            Name = name;
            Calls = new List<CallDef>();
        }
        public ProcDef() : this("", "")
        { }

    }
}
