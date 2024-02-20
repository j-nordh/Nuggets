using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto.Extensions
{
    public static class LinkTableExtensions
    {
        public static (Link Me, Link Other) Split(this LinkTable lt, string name) => 
            lt.A.Table.EqualsOic(name) 
                ? (lt.A, lt.B) 
                : lt.B.Table.EqualsOic(name) 
                    ? (lt.B, lt.A) 
                    : (null, null);

        public static string Name(this LinkTable lt, Link parent) =>
            $"{StringUtil.ToSingle(parent.Table)}{StringUtil.FixPluralization(lt.A.Table.EqualsOic(parent.Table) ? lt.B.Table : lt.A.Table)}";
    }
}
