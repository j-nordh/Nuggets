using System;
using System.Collections.Generic;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Dto
{
    public class ColumnProperties
    {
        public enum Modes
        {
            Equals,
            FromTo,
            Omit,
            Like,
            StringMatch
        }
        public ColumnProperties(string name)
        {
            Name = name;
            CodeName = char.ToUpper(Name[0]) + Name.Substring(1);
            Type = "Int";
        }

        public string Type { get; set; }
        public byte? Precision { get; set; }
        public byte? Scale { get; set; }

        public string Name { get; }
        public string CodeName { get; set; }
        public string ParameterName => char.ToLower(CodeName[0]) + CodeName.Substring(1);
        public bool IsKey { get; set; }
        public bool Nullable { get; set; }


        static HashSet<string> _byRefTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "string" };
        public bool IsByRef => _byRefTypes.Contains(GetDtoType());
        public string GetDtoType()
        {
            var t = GetCodeType();
            if (!Nullable || t.Equals("string")) return t;
            return $"{t}?";
        }

        public string FullType
        {
            get
            {
                string t = Type;
                if (t.EqualsOic("decimal"))
                    t += $"({Precision}, {Scale})";
                return t;
            }
        }

        private string GetCodeType()
        {
            switch (Type.SubstringBefore("(").ToLower())
            {
                case "nvarchar":
                case "varchar": return "string";
                case "int": return "int";
                case "tinyint": return "byte";
                case "smallint": return "short";
                case "datetime": return "DateTime";
                case "bit": return "bool";
                case "bigint": return "long";
                case "date": return "DateTime";
                case "decimal": return "decimal";
                default: return $"Unknown({Type})";
            }
        }
    }
}