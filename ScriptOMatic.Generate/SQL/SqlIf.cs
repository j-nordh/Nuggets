using System;
using UtilClasses;

namespace ScriptOMatic.Generate.SQL
{
    class SqlIf : IAppendable
    {
        private readonly IAppendable _whenTrue;
        private readonly IAppendable _whenFalse;
        readonly string _condition;

        public SqlIf(string condition, IAppendable whenTrue, IAppendable whenFalse)
        {
            _condition = condition;
            _whenTrue = whenTrue;
            _whenFalse = whenFalse;
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb) =>
            sb.AppendLine($"IF {_condition}").Indent()
                .AppendLine("BEGIN")
                .Indent()
                .AppendObject(_whenTrue).AppendLine()
                .Outdent().AppendLine("END")
                .OutdentLine("ELSE")
                .AppendLine("BEGIN").Indent()
                .AppendObject(_whenFalse)
                .Outdent()
                .AppendLine("END")
                .Outdent();
        public static SqlIf Id(IAppendable whenTrue, IAppendable whenFalse)
        {
            return new SqlIf("@id is not null", whenTrue, whenFalse);
        }

        public static SqlIf Id(Func<bool, IAppendable> f) => Id(f(true), f(false));

        public static SqlIf ParamNotNull(string paramName, Func<string, IAppendable> f) =>
            ParamNotNull(paramName, f(paramName), f(null));

        public static SqlIf ParamNotNull(string paramName, IAppendable whenTrue, IAppendable whenFalse)
        {
            return new SqlIf($"@{paramName} is not null", whenTrue, whenFalse);
        }

    }
}