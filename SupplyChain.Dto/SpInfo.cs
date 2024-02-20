using System;
using System.Collections.Generic;
using System.Linq;
using SupplyChain.Procs;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.HashSets;
using UtilClasses.Extensions.Reflections;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    public class SpInfo
    {
        public Modes Mode { get; set; }
        public string Name { get; set; }
        public bool InputJson { get; set; }
        public bool OutputJson { get; set; }
        public string CodeName { get; set; }

        public SpInfo() { }
        public SpInfo(string name)
        {
            Name = name;
        }
        public enum Modes
        {
            None,
            Create,
            DropCreate,
            Drop
        }

        public SpInfo(SpInfo template)
        {
            Mode = template.Mode;
            Name = template.Name;
            InputJson = template.InputJson;
            OutputJson = template.OutputJson;
            CodeName = template.CodeName;
        }
        public SpInfo(Modes mode, string name, string codeName, bool inputJson, bool outputJson)
        {
            Mode = mode;
            Name = name;
            InputJson = inputJson;
            OutputJson = outputJson;
            CodeName = codeName;
        }
    }

    public class ReadWithCol : SpInfo
    {
        public string Column { get; set; }
        public List<ParamSpec> AllParameters { get; set; }
        public int Number { get; set; }
        public bool ReturnsList { get; set; }

        public ReadWithCol()
        {
            AllParameters = new List<ParamSpec>();
        }

        public ReadWithCol(ReadWithCol o) : base(o)
        {
            Column = o.Column;
            Number = o.Number;
            ReturnsList = o.ReturnsList;
            AllParameters = new List<ParamSpec>(o.AllParameters);
        }

        public ReadWithCol(SpInfo si) : base(si)
        {
            AllParameters = new List<ParamSpec>();
        }

        public ReadWithCol(Modes mode, string name, string codeName, bool outputJson, bool inputJson, string column, IEnumerable<ParamSpec> parameters, bool returnsList)
            : base(mode, name, codeName, inputJson, outputJson)
        {
            Column = column;
            AllParameters = new List<ParamSpec>(parameters?? new ParamSpec[] { });
            ReturnsList = returnsList;
        }

        public IEnumerable<ParamSpec> GetParameters(params ParameterMode[] modes) =>
            AllParameters.Where(p => !modes.Any() || modes.Any(m => p.Mode == m)).OrderBy(p=>p.Mode);
        public HashSet<string> GetParameterNames(params ParameterMode[] modes) =>
            GetParameters(modes)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }
    public class ParamSpec
    {
        public string Name { get; set; }
        public ParameterMode Mode { get; set; }

        public ParamSpec()
        {
            Mode = ParameterMode.Parameter;
        }
        public FieldDef ToFieldDef() => new FieldDef(Name, Mode.GetShortName());
    }
    public enum ParameterMode
    {
        [ShortName("P")]
        Parameter,
        [ShortName("N")]
        Null,
        [ShortName("Q")]
        Qualifier
    }
    public static class ParameterExtensions
    {
        public static string GetShortName(this ParameterMode pm) => typeof(ParameterMode).GetFirstCustomAttribute<ShortNameAttribute>(pm.ToString()).Value;
        public static ParamSpec ToParamSpec(this FieldDef fd) => new ParamSpec()
        {
            Name = fd.Name,
            Mode = fd.Mode==null? ParameterMode.Parameter: Enum<ParameterMode>.Values.Single(m => m.GetShortName().EqualsOic(fd.Mode))
        };

        public static IEnumerable<ColumnProperties> Filter(this ReadWithCol rwc, IEnumerable<ColumnProperties> cols,
            params ParameterMode[] modes)
            => rwc.Filter(cols, null, modes);
        public static IEnumerable<ColumnProperties> Filter(this ReadWithCol rwc, IEnumerable<ColumnProperties> cols, ColumnProperties col , params ParameterMode[] modes)
        {
            var lst = new List<ColumnProperties>();
            if (null != col)
                lst.Add(col);
            var names = rwc.GetParameterNames(modes); 
            lst.AddRange(cols.Where(c => names.Contains(c.Name)));
            return lst;
        }

        public static IEnumerable<ParamSpec> ColumnParameters(this ReadWithCol rwc) =>
            rwc.AllParameters.Where(c => c.Name.ContainsOic(rwc.Column));
        public static IEnumerable<ParamSpec> NonColumnParameters(this ReadWithCol rwc) =>
            rwc.AllParameters.Where(c => !c.Name.ContainsOic(rwc.Column));

        
    }
    public class ShortNameAttribute : Attribute
    {
        public string Value { get; set; }
        public ShortNameAttribute(string name) => Value = name;
    }
}
