using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate.Extensions
{
    public static class SubQueryExtensions
    {
        public static IndentingStringBuilder AppendJoins(this IndentingStringBuilder sb, List<TableNode> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                sb.AppendJoin(path[i], path[i + 1]);
            }
            return sb;
        }
        public static IndentingStringBuilder AppendJoin(this IndentingStringBuilder sb, TableNode a, TableNode b)
        {
            TableNode l = null, r = null;
            l = a;
            r = b;

            var str = $"INNER JOIN {r.Name} {r.Alias} on {l.Alias}.{l.GetKey()} = {r.Alias}.{r.GetKey()}";
            //            MessageBox.Show($@"
            //A=> {a.Name}  Pk:{a.PrimaryKeyColumn} Fk:{a.ForeignKeyColumn} Columns:{a.Columns.Select(col => col.Name).Join(", ")}
            //B=> {b.Name}  Pk:{b.PrimaryKeyColumn} Fk:{b.ForeignKeyColumn} Columns:{b.Columns.Select(col => col.Name).Join(", ")}
            //L=>{l.Name}
            //R=>{r.Name}
            //Result=>{str}");

            sb.AppendLine(str);

            return sb;
        }

        public static IndentingStringBuilder AppendWhere(this IndentingStringBuilder sb, TableNode outer, TableNode inner)
        {
            var agg = outer.AggregateCandidates.SingleOrDefault(a => a.Table.EqualsOic(inner.Name));
            if (null == agg) throw new KeyNotFoundException("Could not find a matching aggregate");
            return sb.AppendWhere(agg, outer, inner);
        }
        public static IndentingStringBuilder AppendWhere(this IndentingStringBuilder sb, Aggregate agg, TableNode outer, TableNode inner) => sb.AppendLine($"WHERE {inner.Alias}.{agg.ForeignKeyColumn} = {outer.Alias}.{agg.PrimaryKeyColumn}");
        public static string GetKey(this TableNode n)
        {
            var idCol = n.Columns.SingleOrDefault(c => c.IsKey);
            return null != idCol ? idCol.Name : "Id";
            //if (null == n.PrimaryKeyColumn && null == n.ForeignKeyColumn) return "Id";//hail mary!
            //if(n.Columns.Any(c=>c.Name.EqualsOic(n.PrimaryKeyColumn))) return n.PrimaryKeyColumn;
            //if (n.Columns.Any(c => c.Name.EqualsOic(n.ForeignKeyColumn))) return n.ForeignKeyColumn;

            throw new ArgumentException("No such column!");
        }

        public static SubQuery ToSubQuery(this Aggregate agg, TableNode table, string parentName, string parentAlias = null)
        {
            //var child = _rootNode.Children.First(c => c.Name.Equals(agg.Table));
            if (agg.ShortName.IsNullOrEmpty()) agg.ShortName = $"{table.Name.Where(char.IsUpper).AsString()}";
            string columns = table.Columns.Select(c => $"[{agg.ShortName}].{c.Name}").Join(", ");
            if (parentAlias.IsNullOrEmpty()) parentAlias = $"[{parentName.Where(char.IsUpper).AsString()}]";

            switch (agg)
            {
                case ReverseAggregate ra:
                    return new SubQuery()
                    {
                        Name = agg.Table,
                        Alias = agg.Alias,
                        Query = $@"SELECT 
                                   {columns} 
                                   FROM {agg.Table} {table.Alias} 
                                   WHERE {parentAlias}.{agg.ForeignKeyColumn} = {table.Alias}.{agg.PrimaryKeyColumn}",
                    };
                case LinkedAggregate la:
                    var (me, other) = la.Link.Split(parentName);
                    var valueTable = table.Children.First(c => c.Name.EqualsOic(other.Table));
                    var valueAlias = valueTable.Alias.IsNullOrEmpty() ? $"[{valueTable.Name.Where(char.IsUpper).AsString()}]" : valueTable.Alias;
                    var linkColumns = new[] { me.Field, other.Field };
                    var colsOnLink = table.Columns.Where(c => !linkColumns.ContainsOic(c.Name)).ToList().Select(c => $"{table.Alias}.{c.Name}");
                    columns = valueTable.Columns.Select(c => $"{valueAlias}.{c.Name}").Union(colsOnLink).Join(",\r\n");
                    return new SubQuery()
                    {
                        Name = la.Link.Name,
                        Alias = StringUtil.FixPluralization(other.Table),
                        Query = $@"SELECT 
                                   {columns} 
                                   FROM {valueTable.Name} {valueAlias}
                                   INNER JOIN {la.Table} {table.Alias} ON {table.Alias}.{other.Field} = {valueAlias}.Id
                                   WHERE { parentAlias }.Id = { table.Alias }.{ me.Field}".TrimLines()
                    };
                default:
                    var alias = agg.ShortName ?? $"[{agg.Table.Where(char.IsUpper).AsString()}]";
                    return new SubQuery
                    {
                        Name = agg.Alias,
                        Query = $@"SELECT 
                                   {columns} 
                                   FROM {agg.Table} AS {alias}
                                   WHERE {alias}.{agg.ForeignKeyColumn} = {parentAlias}.Id".TrimLines()
                    };
            }
        }

    }
}