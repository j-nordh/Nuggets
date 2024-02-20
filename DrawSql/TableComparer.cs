using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using SupplyChain.Dto;
using UtilClasses.Db.Extensions;
using UtilClasses.Extensions.Enumerables;

namespace DrawSql
{
    class TableComparer
    {
        private readonly CodeEnvironment _env;
        private readonly bool _force;
        private readonly List<Table> _parsedTables;
        private HashSet<string> _newNames;
        private HashSet<string> _dropNames;
        private HashSet<string> _updatedNames;
        private List<ForeignKey> _foreignKeys;
        public TableComparer(CodeEnvironment env, IEnumerable<Table> parsedTables, bool force)
        {
            _env = env;
            _force = force;
            _parsedTables = parsedTables as List<Table> ?? parsedTables.ToList();
            Init();
        }

        private void Init()
        {
            List<Table> dbTables;
            using (var conn = new SqlConnection($"Server={_env.Db.Server};Database={_env.Db.Name};Trusted_Connection=True;"))
            {
                var tables = conn.QueryDirect<NameId>("select name, object_id as Id from sys.tables").ToDictionary(o => o.Id, o => new Table(o.Name));
                var cols = conn.QueryDirect<FromDbColumn>(Properties.Resources.ColumnQuery);
                int i = 0;
                foreach (var col in cols)
                {
                    tables[col.TableId].Columns[i.ToString()] = col;
                    i += 1;
                }
                dbTables = tables.Values.ToList();
                _foreignKeys = conn.QueryDirect<ForeignKey>(Properties.Resources.ForeignKeyQuery);
            }

            var parsedNames = _parsedTables.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var dbNames = dbTables.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var dbTablesByName = dbTables.ToDictionary(t => t.Name);

            _newNames = _parsedTables
                .Where(t => !dbNames.Contains(t.Name))
                .Select(t => t.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            _dropNames = dbTables
                .Where(t => !parsedNames.Contains(t.Name))
                .Select(t => t.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            _updatedNames = _parsedTables
                .Where(t => !(_newNames.Contains(t.Name) || _dropNames.ContainsOic(t.Name)))
                .Where(t => _force || t.IsChanged(dbTablesByName[t.Name]))
                .Select(t => t.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            KeysToDrop = _foreignKeys.Where(fk =>
                _dropNames.Contains(fk.ReferencedTable) || _updatedNames.Contains(fk.ReferencedTable))
                .Where(fk=>!(_dropNames.Contains(fk.ReferencingTable) || _updatedNames.Contains(fk.ReferencingTable)));

            KeysToRecreate = _foreignKeys
                .Where(fk => !_dropNames.Contains(fk.ReferencingTable))
                .Where(fk => _updatedNames.Contains(fk.ReferencedTable) && !_updatedNames.Contains(fk.ReferencingTable))
                .ToList();
        }

        public IEnumerable<ForeignKey> KeysToDrop { get; private set; }
        public IEnumerable<ForeignKey> KeysToRecreate { get; private set; }
        public IEnumerable<Table> Drop => _parsedTables.WhereInAny(_dropNames, _updatedNames);
        public IEnumerable<string> DropNames => _dropNames.Union(_updatedNames);
        public IEnumerable<Table> Create => _parsedTables.WhereInAny(_newNames, _updatedNames);

        public IEnumerable<string> UnchangedNames =>
            _parsedTables.WhereNotInAny(_newNames, _updatedNames, _dropNames).Select(t => t.Name);


        public int New => _newNames.Count;
        public int Changed => _updatedNames.Count;
        public int Dropped => _dropNames.Count;
        public int Unchanged => _parsedTables.Count - New - Changed - Dropped;
    }

    static class TableComparerExtensions
    {
        public static bool ContainsName(this ISet<string> set, Table t) => set.Contains(t.Name);

        public static IEnumerable<Table> WhereNotInAny(this IEnumerable<Table> ts, params ISet<string>[] sets) =>
            ts.Where(t => !sets.Any(s => s.ContainsName(t)));
        public static IEnumerable<Table> WhereInAny(this IEnumerable<Table> ts, params ISet<string>[] sets) =>
            ts.Where(t => sets.Any(s => s.ContainsName(t)));
    }
}
