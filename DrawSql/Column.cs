using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Expressions;
using UtilClasses.Extensions.Strings;
using UtilClasses.MathClasses;
using TLookup = System.Collections.Generic.List<(System.Func<string, bool> predicate, string value)>;
namespace DrawSql
{
    public class Column
    {
        public virtual string DataType { get; set; }
        public string Name { get; set; }
        public string ForeignKey { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool Nullable { get; set; }
        public virtual string DefaultValue { get; set; }

        public const string UNKNOWN_DATATYPE = "UNKNOWN DATATYPE!!!!!";
        public const string FOREIGNKEY = "ForeignKey";
        public const string IDENTIFIER = "Identifier";
        public const string NAME = "Name";
        public const string DATETIME = "DateTime";
        public const string BOOL = "bit";
        public const string TEXT = "nvarchar(MAX)";

        private static readonly TLookup _autoTypes = new TLookup
        {
            (n=>n.EqualsOic("id"), IDENTIFIER),
            (n=>n.EqualsOic("name"), NAME ),
            (n=>n.Length>2 && (n.EndsWith("Id") || n.EndsWith("Id?")), FOREIGNKEY ),
            (n=>n.EqualsOic("timestamp"), DATETIME),
            (n=>n.StartsWith("Is") && char.IsUpper(n[2]), BOOL),
            (n=>n.EqualsOic("Description"), TEXT),
            (_=>true, UNKNOWN_DATATYPE)
        };

        private static readonly Dictionary<string, string> _typeSynonyms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["dec"] = "decimal",
            ["uuid"] = "guid"
        };

        private static readonly Dictionary<string, string> _typeReplacements = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["short"] = "smallint",
            ["byte"] = "tinyint",
            ["blob"] = "varbinary(MAX)",
            ["guid"] = "UniqueIdentifier",
            ["decimal"] = "decimal(18,6)",
            ["dec"] = "decimal(18,6)",
            ["bigdec"] = "decimal(28,8)",
            ["coord"] = "decimal(18,8)",
            ["bool"] = BOOL,
            ["json"] = TEXT,
            ["text"] = TEXT
        };

        private string GetTypeName(string datatype)
        {
            datatype = _typeSynonyms.Maybe(datatype) ?? datatype;
            datatype = _typeReplacements.Maybe(datatype) ?? datatype;
            return datatype;
        }

        public Column()
        {

        }

        public Column(string name)
        {
            name = name.Trim().Replace(Convert.ToChar(160), ' ');
            while (name.Contains("  "))
                name = name.Replace("  ", " ");

            if (name.StartsWith("*"))
            {
                IsPrimaryKey = true;
                name = name.Substring(1);
            }
            if (name.Contains("="))
            {
                DefaultValue = name.SubstringAfter("=");
                name = name.SubstringBefore("=").Trim();
            }
            Name = name;
            DataType = UNKNOWN_DATATYPE;
            int start = name.IndexOf('[');
            int end = name.IndexOf(']');
            if (end > start && start > 0)
            {
                DataType = name.Substring(start + 1, end - start - 1);
                name = name.Substring(0, start).Trim();
            }
            else
            {

                if (name.Contains(" "))
                {
                    name.SplitAssign(" ", this, () => Name, () => DataType);
                }
                else
                {
                    DataType = _autoTypes.First(mapping => mapping.predicate(name)).value;
                }
            }
            if (DataType.Equals(IDENTIFIER))
                IsPrimaryKey = true;
            if (DataType.EndsWith("?"))
            {
                Nullable = true;
                DataType = DataType.TrimEnd('?');
            }

            if (Name.EndsWith("?"))
            {
                Nullable = true;
                Name = Name.TrimEnd('?');
            }
            DataType = GetTypeName(DataType);
        }

    }

    public static class ColumnExtensions
    {
        private static SqlSettings _s = new SqlSettings();
        private static List<(string Caption, Func<Column, Column, bool> Predicate)> _comparisons = new List<(string,Func<Column, Column, bool>)>
        {
            ("Type", CheckType),
            { c=>c.Name, string.Equals},
            c=>c.Nullable,
            {c=>c.DefaultValue, (a,b)=>a.IsNullOrEquals(b)}
        };

        private static bool CheckType(Column a, Column b)
        {
            if (a.DataType.EqualsOic(b.DataType)) return true;
            if (a.IsIdentifier() && b.IsIdentifier()) return true;
            var tA = _s.GetType(a).ReplaceOic(" NOT NULL", "");
            var tB = _s.GetType(b).ReplaceOic(" NOT NULL", "");
            if (a.DataType.EqualsOic(tB)) return true;
            if (b.DataType.EqualsOic(tA)) return true;
            return false;
        }

        public static IEnumerable<string> Differences(this Column a, Column b) => _comparisons.Where(c => !c.Predicate(a, b)).Select(c=>c.Caption);

        private static bool IsIdentifier(this Column c) =>
            c.DataType.EqualsOic("BigInt") && c.IsPrimaryKey || c.DataType.EqualsOic("Identifier");

        private static void Add<T>(this List<(string, Func<Column, Column, bool>)> lst,
            Expression<Func<Column, T>> e, Func<T, T, bool> f)
        {
            lst.Add((e.GetMember().Name, (a,b)=>f(e.Get<T>(a), e.Get<T>(b))) );
        }

        private static void Add(this List<(string, Func<Column, Column, bool>)> lst, 
            Expression<Func<Column, bool>> e) => lst.Add(e, (a, b) => a == b);
    }
}
