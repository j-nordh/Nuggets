using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.HashSets;

namespace SupplyChain.Procs
{
    public class CallDef
    {
        public string Typename;
        
        public Dictionary<string, string> Mapping;
        public string Returns { get; set; }
        public string Mode { get; set; }
        public string Name { get; set; }
        public bool HideAll { get; set; }
        
        public List<string> Parameters = new List<string>();
        
        public HashSet<string> Hide = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        
        public List<string> Require = new List<string>();
        
        public HashSet<string> Only = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        
        public Dictionary<string, string> StaticStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        public Dictionary<string, int> StaticInts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public HashSet<string> NullableParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);


        public CallDef()
        { }

        public CallDef(CallDef o)
        {
            Typename = o.Typename;
            Mapping = o.Mapping.Clone();
            Returns = o.Returns;
            Mode = o.Mode;
            Name = o.Name;
            HideAll = o.HideAll;
            Parameters = o.Parameters?.ToList();
            Hide = o.Hide.Clone();
            Require = o.Require.ToList();
            Only = o.Only.Clone();
            StaticStrings = o.StaticStrings.Clone();
            StaticInts = o.StaticInts.Clone();
        }

        public bool ShouldSerializeMapping() => Mapping.IsNotNullOrEmpty();
        public bool ShouldSerializeParameters() => Parameters.IsNotNullOrEmpty();
        public bool ShouldSerializeHide() => Hide.IsNotNullOrEmpty();
        public bool ShouldSerializeRequire() => Require.IsNotNullOrEmpty();
        public bool ShouldSerializeOnly() => Only.IsNotNullOrEmpty();
        public bool ShouldSerializeStaticStrings() => StaticStrings.IsNotNullOrEmpty();
        public bool ShouldSerializeStaticInts() => StaticInts.IsNotNullOrEmpty();
        public bool ShouldSerializeHideAll() => HideAll;
        public bool ShouldSerializeReturns() => Returns.IsNotNullOrEmpty();
        public bool ShouldSerializeMode() => Mode.IsNotNullOrEmpty();
        public bool ShouldSerializeName() => Name.IsNotNullOrEmpty();
        public bool ShouldSerializeTypename() => Typename.IsNotNullOrEmpty();

    }
    public class ColFields
    {
        public string Column { get; set; }
        public List<FieldDef> Fields { get; set; }
        public bool ReturnsList { get; set; }
        public ColFields() { }
        public ColFields(string column)
        {
            Fields = new List<FieldDef>();
            Column = column;
        }
        public ColFields(string column, IEnumerable<FieldDef> fields, bool returnsList)
        {
            Column = column;
            Fields = new List<FieldDef>(fields);
            ReturnsList = returnsList;
        }
        public bool ShouldSerializeFields() => !Fields.IsNullOrEmpty();
    }
    public class FieldDef
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public FieldDef()
        { }
        public FieldDef(string name, string mode)
        {
            Name = name;
            Mode = mode;
        }
    }
}
