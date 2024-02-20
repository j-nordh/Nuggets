using System;
using System.Collections.Generic;
using System.Text;
using ScriptOMatic.Generate.Appendable;
using ScriptOMatic.Generate.SQL;
using SupplyChain.Dto;
using UtilClasses;

namespace ScriptOMatic.Generate
{
    public static class CrudSpExtensions
    {
        internal static void Add(this List<Proc> lst, ReadWithCol rf, PopulatedBundle bundle, IEnumerable<string> parameters, IEnumerable<string> where)
        {
            lst.Add(new Proc(rf.Name + (rf.Number == 1 ? "" : rf.Number.ToString()), parameters)
                .With(rf.OutputJson
                    ? (IAppendable)new SelectJson(bundle, null, rf.ReturnsList, where)
                    : new Select(bundle.Columns, bundle.Table.Name, where)));
        }
    }
}
