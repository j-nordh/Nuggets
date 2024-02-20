using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Objects;

namespace ScriptOMatic.Generate.SQL
{
    class Select : IAppendable
    {
        private string _from;
        private readonly List<ColumnProperties> _columns;
        private List<string> _where;


        public Select(IEnumerable<ColumnProperties> columns, string from, params string[] whereStatements) : this(columns, from, (IEnumerable<string>)whereStatements) { }
        public Select(IEnumerable<ColumnProperties> columns, string from, IEnumerable<string> where = null)
        {
            _columns = columns.SmartToList();
            _from = from;
            _where = where.SmartToList();
        }
        public Select(PopulatedBundle b, bool noId = false) : this(b.Columns, b.Table.Name)
        {
            if (_columns.Any(c => c.IsKey) && !noId) CheckId();
        }
        public Select From(string from) => this.Do(() => _from = from);

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb) =>
            sb.AppendLine("SELECT")
                .Indent(() =>
                    sb.AppendObjects(_columns, c => c.Name, ","))
                .AppendLine($"FROM {_from}")
                .Maybe(_where.Any(), () => sb
                    .AppendLine("WHERE")
                    .Indent()
                    .AppendObjects(_where, c => $"({c})", " AND"))
                .Outdent();

        public Select CheckId() => this.Do(() => _where.AddRange(_columns.Where(c => c.IsKey).Select(c => $"@{c.Name} is null or @{c.Name} = {c.Name}")));
        public Select WithWhere(params string[] statement) => this.Do(() => _where.AddRange(statement));
    }
}