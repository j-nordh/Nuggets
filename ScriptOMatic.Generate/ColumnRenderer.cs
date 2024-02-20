using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Pages
{
    public class ColumnRenderer
    {
        Dictionary<string, JsonField> _jsonFields;
        Dictionary<string, string> _enumFields;
        public string TableName { get; set; }

        public ColumnRenderer()
        {
            _jsonFields = new Dictionary<string, JsonField>(StringComparer.OrdinalIgnoreCase);
            _enumFields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        public ColumnRenderer(Dictionary<string, JsonField> jsonFields, Dictionary<string, string> enumFields) : this()
        {
            _jsonFields = jsonFields ?? _jsonFields;
            _enumFields = enumFields ?? _enumFields;

        }

        public string DeclareStrings(ColumnProperties col) => DeclareString(col, false);
        public string NullableDeclareStrings(ColumnProperties col) => DeclareString(col, true);
        public string DeclareString(ColumnProperties col, bool allowNull) => FormatDeclareLine(col, allowNull);
        public IEnumerable<string> DeclareStrings(IEnumerable<ColumnProperties> cols, bool allowNull) => cols.Select(col => FormatDeclareLine(col, allowNull));
        private static Func<ColumnProperties, bool, string> FormatDeclareLine(Func<string, string> nameOverride = null, string typeOverride = null) => (col, allowNull) => FormatDeclareLine(col, allowNull, nameOverride, typeOverride);
        private static string FormatDeclareLine(ColumnProperties col, bool allowNull, Func<string, string> nameOverride = null, string typeOverride = null) =>
            FormatDeclareLine(nameOverride?.Invoke(col.Name) ?? col.Name,
                typeOverride ?? col.Type,
                allowNull,
                col.Precision,
                col.Scale);
        private static string FormatDeclareLine(string name, string type, bool allowNull, byte? p, byte? s)
        {
            var decDef = "";
            if (type.EqualsOic("decimal"))
            {
                decDef = $"({p.Value}, {s.Value})";
            }
            return $"@{name,-20} {type}{decDef}{(allowNull ? " = null" : "")}";
        }

        private static string FormatWhereLine(string colName, string paramName, string op) => $"(@{paramName} IS NULL OR {colName} {op} @{paramName})";

        private static string FormatLikeWhereLine(string colName, string paramName) =>
            $"(@{paramName} IS NULL OR {colName} LIKE '%' + @{paramName} + '%')";

        private static string FormatStringMatchLine(string colName) =>
            $@"(@{colName}Mode IS NULL OR (
		(@{colName}Mode =1 AND {colName} = @{colName}Value) OR
		(@{colName}Mode = 2 AND {colName} like @{colName}Value + '%') OR
		(@{colName}Mode = 3 AND {colName} like '%' + @{colName}Value) OR
		(@{colName}Mode =4 AND {colName} like '%' + @{colName}Value + '%') OR
		(@{colName}Mode = 5 AND {colName} <> @{colName}Value) OR
		(@{colName}Mode = 6 AND NOT {colName} like '%' + @{colName}Value +'%')))";

        //public IEnumerable<string> WhereStrings(ColumnProperties col)
        //{
        //    switch (col.Mode)
        //    {
        //        case Modes.Equals:
        //            return new[] { FormatWhereLine(col.Name, col.Name, "=") };
        //        case Modes.FromTo:
        //            return new[] { FormatWhereLine(col.Name, col.Name + "From", ">="), FormatWhereLine(col.Name, col.Name + "To", "<") };
        //        case Modes.Like:
        //            return new[] { FormatLikeWhereLine(col.Name, col.Name) };
        //        case Modes.StringMatch:
        //            return new[] { FormatStringMatchLine(col.Name) };
        //        case Modes.Omit:
        //            return new string[] { };
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        public string DtoString(ColumnProperties col, bool asProperty)
        {
            var propDeclaration = asProperty ? "{ get; set; }" : ";";
            var t = ComputedType(col);
            //var attr = IsEnum(col) ? "[JsonConverter(typeof(StringEnumConverter))]\n":"";
            return $"public {t} {col.CodeName}{propDeclaration}";
        }

        public string ParameterString(ColumnProperties col) => ParameterString(col, "");

        public string ParameterString(ColumnProperties col, string prefix, bool nullable = false)
        {
            var t = ComputedType(col) + (nullable && !col.IsByRef ? "?" : "");
            var nullValue = nullable ? "=null" : "";

            return $"{t} {ParameterName(col, prefix)}{nullValue}";
        }

        public string ComputedType(ColumnProperties col) =>
            _jsonFields.Maybe(col.Name)?.Type ??
            _jsonFields.Maybe(TableName + col.Name)?.Type ??
            _enumFields.Maybe(col.Name) ??
            _enumFields.Maybe(TableName + col.Name) ??
            col.GetDtoType();

        private bool IsEnum(ColumnProperties col) =>
            _enumFields.ContainsKey(col.Name) ||
            _enumFields.ContainsKey(TableName + col.Name);
        public IEnumerable<string> ParameterStrings(IEnumerable<ColumnProperties> cols) => ParameterStrings(cols, "");
        public IEnumerable<string> ParameterStrings(IEnumerable<ColumnProperties> cols, string prefix) => cols.Select(c => ParameterString(c, prefix));
        public string JsonDeclaration(ColumnProperties col) => $"{col.Name} {col.FullType} '$.{col.Name}'";

        static Dictionary<string, Func<string, string>> _nvarcharConverters = new Dictionary<string, Func<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            {"datetime", n=>$"'''' + Convert(nvarchar(30), {n},120)  + ''''"},
            {"bigint", n=>$"convert(nvarchar(15),{n})" },
            {"nvarchar", n=> $"'''' + REPLACE({n.Replace("\r","").Replace("\n", "' + CHAR(10) + '")}, '''', '''''') + ''''" },
            {"decimal", n=>$"convert(nvarchar(20), {n})" },
            {"tinyint", n=>$"convert(nvarchar(4), {n})" },
            {"int", n=> $"convert(nvarchar(10), {n})"},
            {"bit", n=> $"convert(nvarchar(1), {n})" }
        };
        public string ConvertToNVarChar(ColumnProperties col)
        {
            var f = _nvarcharConverters.Maybe(col.Type);
            string ret = null;
            if (null != f) ret = f(col.Name);
            else if (col.Name.EqualsOic("Value") && !col.Type.StartsWithOic("nvarchar"))
                ret = $"'''' + replace({col.Name},',', '.') + ''''";
            else
            {
                foreach (var k in _nvarcharConverters.Keys)
                {
                    if (!col.Type.StartsWithOic(k)) continue;

                    return ret = _nvarcharConverters[k](col.Name);
                }
            }
            if (null == ret)
                throw new KeyNotFoundException($"Could not find a converter to convert from {col.Type} to NVarChar");
            if (col.Nullable) ret = $"ISNULL({ret}, 'null')";
            return ret;
        }

        public string ParameterName(ColumnProperties col) => ParameterName(col, "");
        public string ParameterName(ColumnProperties col, string prefix) => prefix.IsNullOrWhitespace() ? col.ParameterName : $"{prefix}{col.CodeName}";


        public string Casted(ColumnProperties col) => Casted(col, "");
        public string Casted(ColumnProperties col, string prefix)
         => CastIfNeeded(col) + ParameterName(col, prefix);

        public string CastIfNeeded(ColumnProperties col)
        {
            if (!col.GetDtoType().Equals(ComputedType(col)))
                return $"({col.GetDtoType()})";
            return "";
        }
        public string Casted(IEnumerable<ColumnProperties> cols, string separator = ", ") => cols.Select(Casted).Join(separator);

        public Func<ColumnProperties, string> HashString(int? decimalPlaces) => c => HashString(c, decimalPlaces);
        public string HashString(ColumnProperties col, int? decimalPlaces, bool normalizeLineBreaks = false, bool ignoreCase = false, bool removeWhitespace =false)
        {
            if (col.GetDtoType().ContainsOic("decimal") && decimalPlaces != null)
                return col.GetDtoType().ContainsOic("?")
                    ? $"hash = hash * 17 + ({col.CodeName}.Round({decimalPlaces}) ?? 0xDEADBEEF).GetHashCode();"
                    : $"hash = hash * 17 + Math.Round({col.CodeName}, {decimalPlaces}).GetHashCode()";

            if (col.GetDtoType().EqualsOic("string"))
            {
                var white = removeWhitespace ? ".RemoveAllWhitespace()" : "";
                var toLower = ignoreCase ?  ".ToLowerInvariant()"  :"";
                var lineBreaks = normalizeLineBreaks ? ".ReplaceOic(\"\\r\",\"\")" : "";
                return $"hash = hash * 17 + ({col.CodeName}?{white}{toLower}{lineBreaks}.GetHashCode() ?? (int)0xDEADBEEF);";
            }
                

            return col.IsByRef || col.Nullable
                ? $"hash = hash * 17 + ({col.CodeName}?.GetHashCode() ?? (int)0xDEADBEEF);"
                : $"hash = hash * 17 + {col.CodeName}.GetHashCode();";
        }

        public string EqualsString(ColumnProperties col, int? decimalPlaces, bool normalizeLineBreaks = false, bool ignoreCase = false, bool removeWhitespace =false)
        {
            if (col.GetDtoType().ContainsOic("decimal") && decimalPlaces != null)
                return $"{col.CodeName}.Equals(other.{col.CodeName}, {decimalPlaces})";

            if (!col.GetDtoType().EqualsOic("string"))
                return $"{col.CodeName} == other.{col.CodeName}";
            var white = removeWhitespace ? ".RemoveAllWhitespace()" : "";
            var toLower = ignoreCase ?  ".ToLowerInvariant()"  :"";
            var lineBreaks = normalizeLineBreaks ? ".ReplaceOic(\"\\r\",\"\")" : "";
            if ((white + toLower + lineBreaks).Length == 0)
                lineBreaks = ".ToString()";

            return $"({col.CodeName}?{white}{toLower}{lineBreaks}.EqualsOic(other.{col.CodeName}?{white}{toLower}{lineBreaks}) ?? false)";
        }

        public string HashString(Aggregate a) =>
            $@"foreach(var x in {a.Alias} ?? Enumerable.Empty<{a.TargetType}>())
    hash = hash * 17 + (x?.GetStateCode() ?? (int)0xDEADBEEF);";

        public string Where(ColumnProperties col) => Where(col, "", "=");
        public string Where(ColumnProperties col, string value)
        {
            if (null == value)
                return $"{col.Name} is null";
            if (col.Type.ContainsOic("varchar"))
            {
                value = value.Trim(new[] { '"', '\'' });
                value = $"'{value}'";
            }
            return $"{col.Name} = {value}";
        }

        public string Where(ColumnProperties col, bool allowNull) => Where(col, "", "=", allowNull);
        public Func<ColumnProperties, string> Where(bool allowNull) => c => Where(c, "", "=", allowNull);
        public string Where(ReadWithCol rwc, ParameterMode? mode = null, bool allowNull = false) => rwc.AllParameters.Select(p => Where(p, allowNull)).Join(" AND ");
        public string Where(ParamSpec p) => Where(p, true);
        public string Where(ParamSpec p, bool allowNull)
        {
            switch (p.Mode)
            {
                case ParameterMode.Parameter: return Where(p.Name, "", "=", allowNull);
                case ParameterMode.Null: return WhereIsNull(p.Name);
                case ParameterMode.Qualifier: return Where(p.Name, "", "=", true);
            }
            throw new NotImplementedException($"ParamterMode {p.Mode} not implemented!");
        }
        public List<string> Where(IEnumerable<ParamSpec> ps, bool allowNull) => ps.Select(p => Where(p, allowNull)).ToList();
        public string Where(ColumnProperties col, string prefix, string op, bool allowNull = true)
        => Where(col.Name, prefix, op, allowNull);
        public string Where(string colName, string prefix, string op, bool allowNull = true)
        => allowNull
            ? $"@{prefix}{colName} is null or @{prefix}{colName} {op} {colName}"
            : $"@{prefix}{colName} {op} {colName}";
        public string WhereIsNull(string colName) => $"{colName} is null";


        public Joiner Join(IEnumerable<ColumnProperties> cols) => new Joiner(this, cols);

        public class Joiner
        {
            private readonly ColumnRenderer _cr;
            private readonly IEnumerable<ColumnProperties> _cols;

            public Joiner(ColumnRenderer cr, IEnumerable<ColumnProperties> cols)
            {
                _cr = cr;
                _cols = cols;
            }
            public string Casted() => JoinIt(_cr.Casted);
            public string ParameterNames() => JoinIt(_cr.ParameterName);
            public string ParameterNames(string prefix) => JoinIt(c => _cr.ParameterName(c, prefix));
            public string ParameterStrings() => JoinIt(_cr.ParameterString);
            public string ParameterStrings(string prefix) => JoinIt(c => _cr.ParameterString(c, prefix));
            private string JoinIt(Func<ColumnProperties, string> selector) => _cr.JoinIt(_cols, selector);
        }

        private string JoinIt(IEnumerable<ColumnProperties> cols, Func<ColumnProperties, string> selector, string separator = ", ")
        {
            return cols.Select(c => selector(c)).Join(separator);
        }

    }
}