using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;

namespace ScriptOMatic.Generate.SQL
{
    public class SqlUpdate : IAppendable
    {
        readonly string _table;
        List<ColumnProperties> _columns;
        readonly bool _json;

        public SqlUpdate(string table, IEnumerable<ColumnProperties> columns, bool json)
        {
            _json = json;
            _table = table;
            _columns = columns.SmartToList();
        }
        public SqlUpdate(PopulatedBundle b) : this(b.Table.Name, b.Columns, b.Update.InputJson)
        { }
        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            var valueCols = _columns.Where(c => !c.IsKey).ToList();
            var idCols = _columns.Where(c => c.IsKey).ToList();
            if (!_json)
            {
                return sb.AppendLine($"UPDATE {_table} SET").Indent()
                    .AppendObjects(valueCols, c => $"{c.Name} = @{c.Name}", ",")
                    .OutdentLine("WHERE")
                    .AppendObjects(idCols, c => $"{c.Name} = @{c.Name}", " AND ")
                    .Outdent();
            }

            sb.AppendLine($"UPDATE {_table} SET").Indent()
                .AppendLines(valueCols.Select(c => $"{c.Name} = J.{c.Name}"), ",")
                .OutdentLine("FROM OPENJSON(@json) WITH (")
                .AppendLines(_columns.Select(c => c.Name + " " + c.Type), ",").Outdent().AppendLine(") AS J")
                .OutdentLine("WHERE")
                .AppendObjects(idCols, c => $"{_table}.{c.Name} = J.{c.Name}", ",");

            //if (_aggregates.IsNullOrEmpty()) return sb;

            //sb.AppendLines("", "declare @id bigint", "select @id = Id FROM OPENJSON(@json) WITH (Id bigint)", "").AppendObjects(_aggregates.Select(a => a.GetAppendable(_table, 'U')));
            //return sb;

            //if (_children.IsNullOrEmpty()) return sb;

            //sb.AppendLine()
            //    .AppendLine()
            //    .AppendLine("DECLARE @parentId int")
            //    .AppendLine("SET @parentId = JSON_VALUE (@json, '$.Id')");

            //foreach (var child in _children)
            //{
            //    sb.AppendLine($"DELETE FROM {child.Name} WHERE {child.ForeignKeyColumn} = @parentId");
            //    cols = child.Columns.Where(c => !c.Name.EqualsIc2(child.ForeignKeyColumn)).ToList();
            //    sb.AppendLine($"INSERT INTO {child.Name} (").Indent(2)
            //        .AppendLine(child.ForeignKeyColumn + ", ")
            //        .AppendLines(cols.Select(c => c.Name), ",")
            //        .Outdent().AppendLine(")")
            //        .AppendLine("SELECT @parentId,").Indent()
            //        .AppendLines(cols.Select(c => c.Name), ",").Outdent()
            //        .AppendLine("FROM OPENJSON(@json) WITH (")
            //        .IndentLine($"{child.FieldName} nvarchar(max) AS JSON")
            //        .AppendLine(")")
            //        .AppendLine($"CROSS APPLY OPENJSON({child.FieldName}) WITH (").Indent()
            //        .AppendLines(cols.Select(c => c.Name + " " + c.Type), ",").Outdent()
            //        .AppendLine(")").Outdent();
            //}
            return sb;
        }
    }
}