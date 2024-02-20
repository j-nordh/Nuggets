using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate.SQL
{
    public class SqlInsert : IAppendable
    {
        List<ColumnProperties> _columns;
        string _tableName;
        bool _inputJson;

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            var cols = _columns.Select(c => c.Name).ToList();
            sb.AppendLine($"INSERT INTO {_tableName} ")
                .AppendLine("       ( " + cols.Join(",  ") + ")");
            if (!_inputJson) return
                sb.AppendLine("VALUES (" + cols.Select(c => $"@{c}").Join(", ") + ")");

            sb
                .Append("SELECT ")
                .Indent()
                .AppendLines(cols, ",")
                .OutdentLine("FROM OPENJSON(@json) WITH (")
                .AppendObjects(_columns, c => c.Name + " " + c.Type, ",")
                .Outdent().AppendLine(")")
                .Outdent();


            return sb;
        }

        public SqlInsert(string name, IEnumerable<ColumnProperties> columns, bool inputJson)
        {
            _tableName = name;
            _columns = columns.ToList();
            var primaryIdColumn = _columns.SingleOrDefault(c => c.IsKey && c.Name.EqualsOic("Id"));
            if (primaryIdColumn != null)
            {
                _columns.Remove(primaryIdColumn);
            }
            _inputJson = inputJson;
        }
        private SqlInsert()
        { }

        public static SqlInsert FromBundle(PopulatedBundle b)
        {
            var cols = b.Columns.Where(c => !c.Name.EqualsIc2("id")).ToList();
            return new SqlInsert()
            {
                _tableName = b.Table.Name,
                _inputJson = b.Create.InputJson,
                _columns = cols,
            };
        }

    }
}