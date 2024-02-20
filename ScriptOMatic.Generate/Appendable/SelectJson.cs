using System.Collections.Generic;
using System.Linq;
using ScriptOMatic.Generate.Extensions;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using SupplyChain.Dto.Extensions;

namespace ScriptOMatic.Generate.Appendable
{
    public class SelectJson :IAppendable
    {
        private readonly PopulatedBundle _b;
        private readonly string _qualifier;
        private List<string> _columns;
        private readonly List<SubQuery> _subQueries;
        private readonly string _name;
        private readonly string _alias;
        private readonly string _primaryKeyColumn;
        private List<string> _where;
        readonly bool _returnsList;

        public SelectJson(PopulatedBundle b, string qualifier, bool returnsList, params string[] whereStatements) : this(b, qualifier,  returnsList, (IEnumerable<string>)whereStatements)
        { }
        public SelectJson(PopulatedBundle b, string qualifier, bool returnsList, IEnumerable<string> where = null)
        {
            _returnsList = returnsList;
            _qualifier = qualifier;
            _columns = b.RootNode.Columns
                .Select(c => $"{(b.RootNode.Alias == null ? "" : b.RootNode.Alias + ".")}{c.Name}")
                .ToList();
            _name = b.RootNode.Name;
            b.RootNode.Alias = b.RootNode.Alias ?? b.Table.GenerateAlias();
            _alias = b.RootNode.Alias;
            _primaryKeyColumn = b.RootNode.IdCol()?.Name;
            _subQueries = b.SubQueries().SmartToList();
            _where = where.SmartToList();
            if (qualifier.IsNotNullOrEmpty() && !_where.Any(c=>c.Contains($@"{qualifier}")))
                _where.Add($"{ qualifier} = @{qualifier}");
        }
        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {

            return sb.AppendLine("SELECT")
                .Indent(_ => sb.AppendObjects(_columns.AsAppendable().Union(_subQueries.Select(o=>new AppendableSubQuery(o, _alias))), ","))
                .AppendLine($"FROM {_name} AS {_alias}")
                .Maybe(_where.Any(), () => sb
                    .AppendLine("WHERE")
                    .AppendObjects(_where, c => $"({c})", " AND"))
                .Append("FOR JSON AUTO").MaybeAppend((!_returnsList) || _qualifier.IsNotNullOrEmpty(), ", without_array_wrapper").AppendLine();
        }

    }
}