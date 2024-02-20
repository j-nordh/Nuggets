using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ScriptOMatic.Generate.Extensions;
using ScriptOMatic.Generate.Properties;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.Db.Extensions;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Generate
{
    public class DbLoader
    {
        private readonly CodeEnvironment _env;
        private readonly DbDef _def;
        private int _maxDepth;

        private List<(string pkTable, string pkColumn, string fkTable, string fkColumn)> _fks;
        private readonly Dictionary<string, List<TableNode>> _subscribers = new Dictionary<string, List<TableNode>>();
        private Dictionary<string, TableNode> _processedTables;

        public DbLoader(CodeEnvironment env)
        {
            
            _env = env;
            _def = _env.GetDbDef();
        }

        public void Init(int maxDepth)
        {
            _maxDepth = maxDepth;
            using(var conn = GetConn())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText =Resources.ForeignKeysQuery;
                _fks = new List<(string pkTable, string pkColumn, string fkTable, string fkColumn)>();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        _fks.Add((rdr["ReferencedTable"].ToString(), rdr["ReferencedColumn"].ToString(), rdr["ReferencingTable"].ToString(), rdr["ReferencingColumn"].ToString()));
                }
            }
        }

        public TableNode GetNode(string name)
        {

                _processedTables = new Dictionary<string, TableNode>(StringComparer.OrdinalIgnoreCase);
                var ret = BuildNode(name, 0);
            while (_subscribers.Any())
            {
                var kvp = _subscribers.First();
                BuildNode(kvp.Key, kvp.Value.Min(s => s.Depth) + 1);
            }
            return ret;

        }

        public PopulatedBundle Populate(Bundle b)
        {
            return b.Populate(_env, Columns(b.Table.Name), GetNode(b.Table.Name));
        }
        private TableNode BuildNode(string name, int depth)
        {
            if (_processedTables.ContainsKey(name) || (depth >= _maxDepth))
            {
                _subscribers.Remove(name);
                return null;
            }


            var tn = new TableNode(name)
            {
                Columns = Columns(name),
                AggregateCandidates =
                    _fks
                        .Where(x => x.pkTable.EqualsOic(name))
                        .Distinct(x => x.fkTable)
                        .Select(x => GetAggregate(name, x.fkTable, x.pkColumn, x.fkColumn))
                        .Union(
                            _fks
                                .Where(x => x.fkTable.EqualsOic(name))
                                .Select(x => GetReverseAggregate(x.pkTable, name, x.pkColumn, x.fkColumn))).ToList(),
                Depth = depth
            };
            _processedTables[name] = tn;

            foreach (var link in _def.LinkTables)
            {
                var (me, other) = link.Split(name);
                if (me == null || other == null) continue;

                tn.AggregateCandidates.Add(SetupAggregate(new LinkedAggregate() { Link = link }, me.Table, other.Table, me.Field, other.Field));
            }

            foreach (var agg in tn.AggregateCandidates.Select(a => a.Table).Distinct())
            {
                if (_processedTables.TryGetValue(agg, out var t))
                    tn.Children.Add(t);
                else
                    _subscribers.GetOrAdd(agg).Add(tn);
            }

            if (!_subscribers.TryGetValue(name, out var lst)) return tn;

            lst.ForEach(n => n.Children.Add(tn));
            _subscribers.Remove(name);

            return tn;
        }
        public List<ColumnProperties> Columns(string table)
        {
            var cols = new List<ColumnProperties>();
            using (var conn = GetConn())
            {
                var keyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                conn.ExecuteReader(string.Format(Resources.PrimaryKeyQuery, table), rdr => keyColumns.Add(rdr["COLUMN_NAME"].ToString()));
                conn.ExecuteReader(string.Format(Resources.TableQuery, table), rdr =>
                {
                    var name = rdr["COLUMN_NAME"].ToString();
                    var type = rdr["DATA_TYPE"].ToString();
                    var isDec = type.EqualsOic("decimal");
                    var cp = new ColumnProperties(name)
                    {
                        Type = type,
                        IsKey = keyColumns.Contains(name),
                        Precision = rdr["NUMERIC_PRECISION"].MaybeAsByte(isDec),
                        Scale = rdr["NUMERIC_SCALE"].MaybeAsByte(isDec),
                        Nullable = rdr["IS_NULLABLE"].AsBoolean()
                    };
                    var lenIndex = rdr.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
                    if (!rdr.IsDBNull(lenIndex))
                    {
                        cp.Type = rdr.GetInt32(lenIndex) == -1
                            ? $"{cp.Type}(Max)"
                            : $"{cp.Type}({rdr.GetInt32(lenIndex)})";
                    }


                    cols.Add(cp);
                });
            }
            return cols;
        }
        public Aggregate GetAggregate(string pkTable, string fkTable, string pkColumn, string fkColumn) => SetupAggregate(new Aggregate(), pkTable, fkTable, pkColumn, fkColumn);
        public ReverseAggregate GetReverseAggregate(string pkTable, string fkTable, string pkColumn, string fkColumn) =>
            SetupAggregate(new ReverseAggregate(), pkTable, fkTable, pkColumn, fkColumn);

        private T SetupAggregate<T>(T a, string pkTable, string fkTable, string pkColumn, string fkColumn) where T : Aggregate
        {
            a.Table = fkTable;
            a.Alias = StringUtil.FixPluralization(fkTable.SubstringAfter("tbl").Replace(StringUtil.ToSingle(pkTable), ""));
            a.Repo = fkTable.SubstringAfter("tbl");
            a.TargetType = StringUtil.ToSingle(fkTable);
            a.PrimaryKeyColumn = pkColumn;
            a.ForeignKeyColumn = fkColumn;
            switch (a)
            {
                case ReverseAggregate ra:
                    ra.Table = pkTable;
                    ra.Alias = StringUtil.ToSingle(pkTable);
                    ra.Repo = pkTable.SubstringAfter("tbl");
                    ra.TargetType = StringUtil.ToSingle(pkTable);
                    ra.PrimaryKeyColumn = pkColumn;
                    ra.ForeignKeyColumn = fkColumn;
                    return (T)(Aggregate)ra;
                case LinkedAggregate la:
                    la.Table = la.Link.Name;
                    return (T)(Aggregate)la;
            }

            return a;
        }

        private SqlConnection GetConn()
        {
            var conn = new SqlConnection(
                $"Data Source={_env.Db.Server}; Database={_env.Db.Name}; Integrated Security=True;");
            conn.Open();
            return conn;
        }
    }
}
