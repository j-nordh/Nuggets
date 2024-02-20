using System;
using System.Collections.Generic;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    public class EnumDef
    {
        public string Name;
        public string Table { get; set; }
        public string IdColumn { get; set; }
        public string NameColumn { get; set; }
        public List<AttributeDef> Attributes { get; set; }

        public EnumDef()
        {
            Attributes = new List<AttributeDef>();
        }
    }

    public class AttributeDef
    {
        public string Column { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public AttributeStyle Style { get; set; }

        public string ToString(IDictionary<string, object> dict)
        {
            switch (Style)
            {
                case AttributeStyle.PresentIfTrue:
                    return dict[Column].AsBoolean() ? $"[{Name}]" : null;
                case AttributeStyle.CastedValue:
                    return $"[{Name}(({Type}) {dict[Column]})]";
                case AttributeStyle.Value:
                    return $"[{Name}({dict[Column]})]";
            }
            throw new ArgumentOutOfRangeException("");
        }

        public override string ToString() => $"{Name} ({Style.ToString()})";
    }
    public enum AttributeStyle
    {
        PresentIfTrue,
        CastedValue,
        Value
    }

}