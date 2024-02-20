using ScriptOMatic.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;

namespace ScriptOMatic.Generate
{
    public class Sql
    {
        public static string Permissions(PermissionSet ps) => 
            new PermissionRenderer(ps.Table, ps.Templates).Append(ps.Permissions).ToString();

        public static IEnumerable<IAppendable> CrudSPs(PopulatedBundle b) => 
            new SqlRenderer().Append(b).Parts;

        public static IEnumerable<IAppendable> LinkTable(PopulatedLinkTable plt)=> new SqlRenderer().AppendLinkTable(plt).Parts;

    }
}
