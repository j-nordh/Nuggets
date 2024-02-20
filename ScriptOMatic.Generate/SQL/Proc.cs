using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses;
using UtilClasses.Extensions.Objects;

namespace ScriptOMatic.Generate.SQL
{
    internal class Proc : IAppendable
    {
        private readonly string _name;
        private List<string> _parameters;
        private List<IAppendable> _bodyParts;
        public Proc(string name, IEnumerable<string> parameters = null)
        {
            _name = name;
            _parameters = parameters as List<string> ?? parameters?.ToList() ?? new List<string>();
            _bodyParts = new List<IAppendable>();
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb) =>
            sb
                .Append($"CREATE OR ALTER PROCEDURE {_name} ")
                .Maybe(_parameters.Any(), () => sb
                    .AppendLine("(")
                    .Indent()
                    .AppendLines(_parameters, ",")
                    .OutdentLine(") AS"))
                .Maybe(!_parameters.Any(), () => sb
                    .AppendLine("AS")
                    .Indent())
                .AppendObjects(_bodyParts)
                .Outdent();

        public Proc With(IAppendable part) => this.Do(() => _bodyParts.Add(part));
        public Proc With(IEnumerable<IAppendable> parts) => this.Do(() => _bodyParts.AddRange(parts));
        public Proc With(params string[] lines) => With(lines.AsAppendable());
        public Proc WithIdParam(bool include = true)
        {
            if (include) _parameters.Add("@id bigint = null");
            return this;

        }
        public Proc WithParams(IEnumerable<string> ps)
        {
            foreach (var p in ps)
            {
                _parameters.Add(p.StartsWith("@") ? p : $"@{p}");
            }
            return this;
        }
        public Proc WithParam(string p) => this.Do(() => _parameters.Add(p));

        public Proc IfId(Func<bool, IAppendable> f) => this.Do(() => _bodyParts.Add(SqlIf.Id(f)));
        public Proc WithQualifier(string qualifier, Func<string, IAppendable> f) => this.Do(() => _bodyParts.Add(SqlIf.ParamNotNull(qualifier, f)));
    }
}