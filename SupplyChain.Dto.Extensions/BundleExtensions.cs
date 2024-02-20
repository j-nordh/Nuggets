using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Procs;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto.Extensions
{
    public static class BundleExtensions
    {
        public static List<string> ProcedureNames(this Bundle b) =>
            b.AllProcedures().Select(p => p.Name)
                .Union(b.AllProcedures()
                    .Where(si => si.Name.ContainsOic("upsert") && si.InputJson)
                    .Select(si => si.Name + "JSON"))
                .ToList();
        public static List<SpInfo> AllProcedures(this Bundle b) => b.Procedures.Values.Union(b.SpecializedProcedures.Values.SelectMany(s => s)).ToList();
        public static string GenerateAlias(this TableInfo t) => t.Name.Where(char.IsUpper).AsString();

        public static bool IsReadOnly(this Bundle b) => !b.ShouldCreateAny(Bundle.SpType.Create, Bundle.SpType.Update, Bundle.SpType.Upsert, Bundle.SpType.Delete);

        public static bool ShouldCreate(this Bundle b, Bundle.SpType t) =>
            b.Procedures.Maybe(t).ShouldCreate();

        public static bool ShouldCreateAny(this Bundle b, params Bundle.SpType[] ts) => ts.Any(b.ShouldCreate);


    }
}
