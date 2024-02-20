using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Strings;

namespace DrawSql
{
    class Formatter
    {
        
        IndentingStringBuilder _sb;
        private Dictionary<string, int> _usedNames = new Dictionary<string, int>();
        private SqlSettings _s;

        public Formatter(IndentingStringBuilder sb)
        {
            _sb = sb;
            _s = new SqlSettings();
        }

        public void Append(Table t, List<ForeignKey> fks)
        {
            _sb.AppendLine($"CREATE TABLE {t.Name} (")
                .Indent()
                .AppendLines(t.Columns.Values.Select(Format).Union(fks.Select(Add)), ",")
                .Maybe(t.Columns.Values.Any(c=>c.IsPrimaryKey), ()=>_sb
                    .PrevLine()
                    .AppendLine(",")
                    .AppendLine($"CONSTRAINT PK_{t.Name} PRIMARY KEY ({t.Columns.Values.Where(c=>c.IsPrimaryKey).Select(c=>c.Name).Join(", ")})"))
                .Outdent()
                .AppendLine(")");
        }

        private string Format(Column c) => $"{c.Name} {_s.GetType(c)}";
        
        public string Add(ForeignKey fk)
        {
            var keyName = $"fk_{fk.ReferencingTable}_{fk.ReferencedTable}";
            var count = _usedNames.Increment(keyName);
            if (count > 1) keyName += $"_{count}";
            return $"CONSTRAINT {keyName} FOREIGN KEY({fk.ReferencingColumn}) REFERENCES {fk.ReferencedTable}({fk.ReferencedColumn})";
        }

        public string Drop(ForeignKey fk) =>$"ALTER TABLE {fk.ReferencingTable} DROP CONSTRAINT {fk.Name}";

    }
}
