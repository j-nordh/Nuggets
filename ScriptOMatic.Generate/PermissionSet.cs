using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Dto;

namespace ScriptOMatic.Generate
{
    public class PermissionSet
    {
        public string Table { get; set; }
        public Dictionary<string, string> Templates { get; }
        public List<PermDef> Permissions { get; set; }
        public PermissionSet(string table=null, IEnumerable<PermDef> permissions = null, string cTemplate = null, string rTemplate = null, string uTemplate = null, string dTemplate = null)
        {
            Templates = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            Permissions = new List<PermDef>(permissions??new PermDef[] { });
            Table = table;
            Templates["c"] = cTemplate;
            Templates["r"] = rTemplate;
            Templates["u"] = uTemplate;
            Templates["d"] = dTemplate;
        }
    }
}
