using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    public static class CrudSpExtensions
    {
        public static void Add(this List<string> lst, string name, IEnumerable<string> ss)
        {
            if (ss.IsNullOrEmpty()) return;
            lst.Add($"\"{name}\":[{ss.Select(s => $"\"{s}\"").Join(", ")}]");
        }

        public static void Add(this List<string> lst, string name, string value)
        {
            if (value.IsNullOrEmpty()) return;
            lst.Add($"\"{name}\": \"{value}\"");
        }


        public static IAppendable CreateIfRequested(this SpInfo info, bool predicate, Func<IAppendable> creator) => info.ShouldCreate(predicate) ? creator() : null;
        public static IAppendable CreateIfRequested(this SpInfo info, Func<bool> predicate, Func<IAppendable> creator) => info.ShouldCreate(predicate()) ? creator() : null;
        public static IAppendable CreateIfRequested(this SpInfo info, Func<IAppendable> creator) => info.ShouldCreate() ? creator() : null;
        public static bool ShouldCreate(this SpInfo info) => info != null && (info.Mode == SpInfo.Modes.Create || info.Mode == SpInfo.Modes.DropCreate);
        public static bool ShouldCreate(this SpInfo info, bool predicate) =>info != null && info.ShouldCreate() && predicate;
        public static bool ShouldCreate(this SpInfo info, Func<bool> predicate) => info != null && info.ShouldCreate(predicate());
        public static bool ShouldCreate(this SpInfo info, Func<SpInfo, bool> predicate) => info != null && info.ShouldCreate() && predicate(info);
    }
}