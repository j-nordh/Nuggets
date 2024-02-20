using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate.SQL
{
    public class SqlUpsert : IAppendable
    {
        private readonly string _name;
        private readonly IEnumerable<ColumnProperties> _columns;

        public SqlUpsert(string name, IEnumerable<ColumnProperties> columns)
        {
            _name = name;
            _columns = columns;
        }
        private class OpWrapper
        {
            public IAppendable Insert { get; }
            public IAppendable Select { get; }
            public IAppendable Update { get; }
            public bool HasId { get; }

            public OpWrapper(bool hasId, IAppendable insert, IAppendable select, IAppendable update)
            {
                HasId = hasId;
                Insert = insert;
                Select = select;
                Update = update;
            }
        }
        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            var op = new OpWrapper
                (_columns.Count(c => c.IsKey) == 1 && _columns.Any(c => c.Name.EqualsOic("Id") && c.IsKey),
                new SqlInsert(_name, _columns, false),
                new Select(_columns, _name, _columns.Where(c => c.IsKey).Select(c => $"{c.Name} = @{c.Name}")),
                new SqlUpdate(_name, _columns, false));
            return AppendTransaction(op, sb);
        }

        private IndentingStringBuilder AppendTransaction(OpWrapper op, IndentingStringBuilder sb)
        {
            if (_columns.Any(c => !c.IsKey))
                return sb
                    .AppendLine("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;")
                    .AppendLine("BEGIN TRAN")
                    .Indent().
                    AppendLine($"IF EXISTS ( SELECT {_columns.First().Name} FROM {_name} WITH (UPDLOCK) WHERE ")
                    .Indent()
                    .AppendObjects(_columns.Where(c => c.IsKey), c => $"{c.Name} = @{c.Name}", " AND ")
                    .OutdentLines(")", "BEGIN")
                    .AppendObject(op.Update)
                    .OutdentLines("END", "ELSE", "BEGIN")
                    .AppendObject(op.Insert)
                    .MaybeAppendLine(op.HasId, "set @Id = SCOPE_IDENTITY()")
                    .Outdent()
                    .AppendLine("END")
                    .AppendObject(op.Select)
                    .Outdent()
                    .AppendLine("COMMIT");
            else
                return sb
                    .AppendLine("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;")
                    .AppendLine("BEGIN TRAN")
                    .Indent().
                    AppendLine($"IF NOT EXISTS ( SELECT {_columns.First().Name} FROM {_name} WITH (UPDLOCK) WHERE ")
                    .Indent()
                    .AppendObjects(_columns.Where(c => c.IsKey), c => $"{c.Name} = @{c.Name}", " AND ")
                    .OutdentLine(")")
                    .AppendObject(op.Insert)
                    .Outdent()
                    .AppendObject(op.Select)
                    .Outdent()
                    .AppendLine("COMMIT");
        }


        private IndentingStringBuilder AppendRowCount(OpWrapper op, IndentingStringBuilder sb) => sb
            .AppendObject(op.Update)
            .AppendLine()
            .AppendLines("IF @@ROWCOUNT =0", "BEGIN")

            .Indent()
            .AppendObject(op.Insert)
            .MaybeAppendLine(op.HasId, "set @Id = SCOPE_IDENTITY()")
            .Outdent()
            .AppendLine("END")
            .AppendObject(op.Select);
    }
}