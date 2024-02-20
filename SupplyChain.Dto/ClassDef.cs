using System;
using System.Collections.Generic;
using System.Linq;
using SupplyChain.Procs;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    public class ClassDef
    {
        public string ClassName { get; set; }
        public List<ProcDef> Procedures { get; set; }

        public ClassDef(string className, IEnumerable<ProcDef> procs) : this(className)
        {
            Procedures.AddRange(procs);
        }
        public ClassDef(string className) : this()
        {
            ClassName = className;
        }

        public ClassDef()
        {
            ClassName = null;
            Procedures = new List<ProcDef>();
        }

        public bool IsCrudDef()
        {
            var type = CrudType();
            if (null == type) return false;
            var calls = Procedures.SelectMany(p => p.Calls).ToList();
            var gates = new List<Func<CallDef, bool>>()
            {
                c => c.HasName("create") && c.TakesType(type) &&c.Returns(type),
                c => c.HasName("get") && c.ReturnsList(type) && c.Hides("id"),
                c=> c.HasName("get") && c.Returns(type),
                c=>c.HasName("update") && c.ReturnsVoid(),
                c=> c.HasName("delete") && c.ReturnsVoid()
            };

            foreach (var gate in gates)
            {
                if (!calls.Any(gate))
                    return false;
            }
            return true;
        }
        public string CrudType()=> Procedures.SelectMany(p => p.Calls).Where(c=>c.HasName("Create")).Select(c=>c.Returns).FirstOrDefault()?.StripAllGenerics();
    }
    static class CallExtensions
    {
        public static bool Hides(this CallDef c, string name) => c.HideAll || (c.Hide?.Any(s => s.EqualsOic(name)) ?? false);
        public static bool ReturnsList(this CallDef c) => c.Returns.ContainsOic("List<");
        public static bool ReturnsList(this CallDef c, string type) => c.Returns.EqualsOic($"List<{type}>") || c.Returns.EqualsOic($"json<List<{type}>>");
        public static bool Returns(this CallDef c, string type) => c.Returns.EqualsOic(type) || c.Returns.EqualsOic($"json<{type}>");
        public static bool HasName(this CallDef c, string name) => c.Name.EqualsOic(name);
        public static bool ReturnsVoid(this CallDef c) => c.Returns.EqualsOic("void");

        public static bool TakesType(this CallDef c, string type) => c.Typename.EqualsOic(type) || c.Typename.EqualsOic($"json<{type}>");
        public static bool IsCrudDef(this ClassDef def)
        {
            var type = def.CrudType();
            if (null == type) return false;
            var calls = def.Procedures.SelectMany(p => p.Calls).ToList();
            var gates = new List<Func<CallDef, bool>>()
            {
                c => c.HasName("create") && c.TakesType(type) &&c.Returns(type),
                c => c.HasName("get") && c.ReturnsList(type) && c.Hides("id"),
                c=> c.HasName("get") && c.Returns(type),
                c=>c.HasName("update") && c.ReturnsVoid(),
                c=> c.HasName("delete") && c.ReturnsVoid()
            };

            foreach (var gate in gates)
            {
                if (!calls.Any(gate))
                    return false;
            }
            return true;
        }
    }
}

