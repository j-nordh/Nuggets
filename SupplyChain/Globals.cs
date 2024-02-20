using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using UtilClasses;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;

namespace SupplyChain
{
    static class Globals
    {
        public static void Init(CodeEnvironment env)
        {
            Map = new TextFileMap(env);
        }

        public static TextFileMap Map { get; private set; }
        public static readonly HashSet<string> ForbiddenNames = new(StringComparer.OrdinalIgnoreCase) { "object" };

        public static string FixForbidden(string t)
        {
            if (null == t) return null;
            var stripped = t.StripAllGenerics();
            if (!ForbiddenNames.Contains(stripped)) return t?.MapTypeName();
            var specified = $"{Map.GetNamespace(stripped)}.{stripped}";
            var ret = t.Contains("<") ? t.Replace(stripped, specified) : specified;
            return ret.MapTypeName();
        }

    }
}
