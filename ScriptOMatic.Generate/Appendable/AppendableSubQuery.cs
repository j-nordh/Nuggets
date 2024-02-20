using System.Linq;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate.Appendable
{
    public class AppendableSubQuery : Appendable<SubQuery>
    {
        private readonly string _parentAlias;

        public AppendableSubQuery(SubQuery obj, string _parentAlias) : base(obj)
        {
            this._parentAlias = _parentAlias;
        }

        public override IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            var alias = _obj.Alias.IsNullOrEmpty() ? _obj.Name : _obj.Alias;
            return sb
                .AutoIndentOn("(", ")")
                .AppendLines("(").Indent()
                .AppendLines(_obj.Query)
                .Outdent()
                //.AppendLine(")")
                .Append($"FOR JSON AUTO) [{alias}]").Outdent();

            /*
             * selected.Columns.Select(c => alias + c.Name), ","
             * .AppendLines($"FROM {selected} {selected.Alias ?? ""}")
             */
        }
        public override string ToString() => _obj.Name;
    }

}