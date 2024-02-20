using ScriptOMatic.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using ScriptOMatic.Generate.Appendable;
using ScriptOMatic.Generate.Extensions;
using ScriptOMatic.Generate.SQL;
using SupplyChain.Dto;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Enums;

namespace ScriptOMatic.Generate
{
    class SqlRenderer : Renderer<PopulatedBundle>
    {
        PopulatedBundle _bundle;
        bool _hasKeys;
        ColumnRenderer _cr;
        public SqlRenderer()
        {
            _cr = new ColumnRenderer();
        }
        private List<string> GetKeyDeclarations(bool allowNull) => _bundle.Columns.Where(c => c.IsKey).Select(c => _cr.DeclareString(c, allowNull)).ToList();
        protected override IEnumerable<IAppendable> Render(PopulatedBundle b)
        {
            if (b?.Columns == null) yield break;
            _bundle = b;
            _hasKeys = _bundle.Columns.Any(c => c.IsKey);

            var parts = new[]
            {
                GetCreate(),
                GetRead(),
                GetReadJson(),
                GetProcedures(_bundle.SpecializedProcedures.ReadBetween(), GetReadBetween, "Please select a column to bracket!"),
                GetProcedures(_bundle.SpecializedProcedures.ReadFor(), GetReadFor, "Please select a column as primary"),
                GetProcedures(_bundle.SpecializedProcedures.ReadForMax(), GetMaxFor, "Please select a column to find the max value for"),
                GetReadIdIn(),
                GetUpdate(),
                GetDelete(),
                GetProcedures(_bundle.SpecializedProcedures.DeleteFor(), GetDeleteFor, "Please select a column to delete for!"),
                GetUpsert()
            }.NotNull();
            foreach (var part in parts)
                yield return part;
        }

        protected override IEnumerable<IAppendable> RenderLinkTable(PopulatedLinkTable plt)
        {
            var parts = new List<IAppendable> { GetRead(plt), GetReadFor(plt), GetReadBetween(plt) };

            foreach (var part in parts)
                yield return part;
        }

        protected override void CombineTo(List<IAppendable> parts, IndentingStringBuilder sb) => sb.AppendObjects(parts, "\r\nGO\r\n");


        public IAppendable GetRead()
        {
            var keys = GetKeyDeclarations(true);
            var json = _bundle.Read?.OutputJson ?? false;
            if(json || keys.IsNullOrEmpty())
                return null;

            return _bundle.Read.CreateIfRequested(() => new Proc(_bundle.Read.Name, keys).With(new Select(_bundle)));
        }

        public IAppendable GetRead(PopulatedLinkTable plt) => new MultiAppendable(new[] { GetRead(plt, true), GetRead(plt, false) }, "\r\nGO\r\n");

        private Proc GetRead(PopulatedLinkTable plt, bool readA)
        {
            var col = (readA ? plt.PopA : plt.PopB).Field;
            return GetSelectProc(plt, readA, plt.Columns.Where(c => !c.Name.EqualsOic(col)));
        }

        public IAppendable GetReadFor(PopulatedLinkTable plt) =>
            new MultiAppendable(new[] { GetSelectProc(plt, true), GetSelectProc(plt, false) }, "\r\nGO\r\n");

        public IAppendable GetReadBetween(PopulatedLinkTable plt)
        {

            if (plt.Columns.Count <= 2) return null;
            var lst = new List<IAppendable>();

            var cols = plt.Columns
                .Where(c => !(c.Name.EqualsOic(plt.A.Field) || c.Name.EqualsOic(plt.B.Field)));
            foreach (var col in cols)
            {
                foreach (var b in new[] { true, false })
                {
                    var link = b ? plt.PopA.Field : plt.PopB.Field;
                    var other = b ? plt.PopB.Field : plt.PopA.Field;

                    var template = _cr.DeclareString(col, true);
                    var parameters = new List<string>() { _cr.ParameterString(plt.Columns.First(c => c.Name.EqualsOic(other))) };
                    parameters.AddRange(new[] { "min", "max" }.Select(s => template.Insert(1, s)));
                    var where = new[] { _cr.Where(col, "min", "<="), _cr.Where(col, "max", ">=") };
                    lst.Add(GetSelectProc(plt, b, parameters, where));
                }
            }
            return new MultiAppendable(lst.NotNull());
        }

        private Proc GetSelectProc(PopulatedLinkTable plt, bool readA, IEnumerable<ColumnProperties> ps = null, IEnumerable<string> where = null)
        {
            var t = readA ? plt.PopA : plt.PopB;
            var parameters = ps?.ToList() ?? plt.Columns.Where(c => !c.Name.EqualsOic(t.Field)).ToList();
            var whereStatements = where ?? parameters.Select(_cr.Where);
            return GetSelectProc(plt, readA, parameters.Select(p => _cr.DeclareString(p, false)), whereStatements); //is false correct here?
        }

        private Proc GetSelectProc(PopulatedLinkTable plt, bool readA, IEnumerable<string> ps, IEnumerable<string> where)
        {
            var t = readA ? plt.PopA : plt.PopB;
            var o = readA ? plt.PopB : plt.PopA;
            var (name, lName) = StringUtil.FixPluralization(plt.Name, t.Table);
            name = name.ToAlias().RemoveAll("[", "]");
            return GetProc($"sp{name}_{lName}For{StringUtil.ToSingle(o.Table)}", ps)
                .With(GetSelect(plt, readA, where));
        }

        private Select GetSelect(PopulatedLinkTable plt, bool readA, IEnumerable<ColumnProperties> cols)
            => GetSelect(plt, readA, cols.Select(_cr.Where));

        private Select GetSelect(PopulatedLinkTable plt, bool readA, IEnumerable<string> where)
        {
            var ltAlias = plt.Name.ToAlias();
            var l = readA ? plt.PopA : plt.PopB;
            var alias = l.Table.ToAlias();
            var fields = new[] { plt.A.Field, plt.B.Field };
            var from =
                $"{l.Table} AS {alias}\r\nINNER JOIN {plt.Name} AS {ltAlias} ON {alias}.Id = {ltAlias}.{l.Field}";
            return new Select(l.Columns.Union(plt.Columns.Where(c => !fields.ContainsOic(c.Name))), from, where);
        }

        private Proc GetProc(PopulatedLinkTable plt, string suffix) => GetProc($"sp{StringUtil.FixPluralization(plt.Name)}_{suffix}", plt);
        private Proc GetProc(string name, PopulatedLinkTable plt) => GetProc(name, _cr.ParameterStrings(plt.Columns));
        private Proc GetProc(string name, IEnumerable<string> parameters) => new Proc(name, parameters);


        public IAppendable GetReadIdIn()
        {
            if (!_bundle.ReadIdIn.ShouldCreate()) return null;
            var ret = new Proc(_bundle.ReadIdIn.Name, new[] { "@Ids nvarchar(max)" });
            if(_bundle.Read.OutputJson)
            {
                ret.With(new SelectJson(_bundle, null, true, "Id in (select value from string_split(@ids,','))"));
            }
            else
            {
               ret.With( new Select(_bundle.Columns, _bundle.Table.Name, "Id in (select value from string_split(@ids,','))"));
            }

            return ret;
        }
        public IAppendable GetReadJson()
        {
            if (!_bundle.Read.ShouldCreate(_bundle.Read?.OutputJson ?? false)) return null;

            var p = new Proc(_bundle.Read.Name, GetKeyDeclarations(true));
            if (_hasKeys)
                p.IfId(id => new SelectJson(_bundle, id ? _bundle.Columns.First(c => c.IsKey).Name : null, true));
            else
                p.With(new SelectJson(_bundle, null, true));
            return p;
        }

        public IAppendable GetCreate()
        {
            if (!_bundle.Create.ShouldCreate()) return null;

            var name = _bundle.Create.Name;
            var cols = _bundle.Columns.Where(c => !c.Name.EqualsIc2("id")).ToList();
            var parameters = _bundle.Create.InputJson
                ? new[] { "@json nvarchar(max)" }
                : cols.Select(c => _cr.DeclareString(c, true));
            var proc = new Proc(name, parameters).With(SqlInsert.FromBundle(_bundle));
            var singleId = _bundle.Columns.Count(c => c.IsKey) == 1;
            if (!singleId) return proc;
            if (_bundle.Read != null)
            {
                proc.With("DECLARE @id bigint", "SET @id = SCOPE_IDENTITY()");
                if (_bundle.Read.OutputJson)
                    proc.With(new SelectJson(_bundle, "Id", false));
                else
                    proc.With(new Select(_bundle));
            }
            else
                proc.With("SELECT SCOPE_IDENTITY() as Id");
            return proc;
        }
        public IAppendable GetUpdate()
        {
            if (!_bundle.Update.ShouldCreate()) return null;

            var name = _bundle.Update.Name;
            var parameters = _bundle.Update.InputJson
                ? new[] { "@json nvarchar(max)" }
                : _bundle.Columns.Select(c => _cr.DeclareString(c, true));

            return new Proc(name, parameters).With(new SqlUpdate(_bundle));
        }
        public IAppendable GetDelete()
        {
            if (!_bundle.Delete.ShouldCreate()) return null;
            var name = _bundle.Delete.Name;
            var keys = _bundle.Columns.Where(c => c.IsKey);

            return new Proc(_bundle.Delete.Name, keys.Select(k => _cr.DeclareString(k, false))).With(new SqlDelete(_bundle.RootNode.Name, keys.Select(k => k.Name), false));
        }

        public IAppendable GetUpsert()
        {
            if (!_bundle.Upsert.ShouldCreate()) return null;
            var name = _bundle.Upsert.Name;
            var parameters = _bundle.Columns.Select(c => _cr.DeclareString(c, true));
            var upsert = new Proc(name, parameters).With(new SqlUpsert(_bundle.RootNode.Name, _bundle.Columns));
            if (!_bundle.Upsert.InputJson) return upsert;
            var upsertJson = new Proc(name + "JSON", new[] { "@json nvarchar(max)" }).With(new SqlUpsertJson(name, _bundle.Columns, _cr));
            return new MultiAppendable(new[] { upsert, upsertJson }, "\r\nGO\r\n");
        }

        private IAppendable GetProcedures(List<ReadWithCol> rwcs, Func<ReadWithCol, ColumnProperties, Proc> r, string exceptionText)
            => GetProcedures(rwcs, (rwc, col) => new[] { r(rwc, col) }, exceptionText);
        private IAppendable GetProcedures(List<ReadWithCol> rwcs, Func<ReadWithCol, ColumnProperties, IEnumerable<Proc>> r, string exceptionText)
        {
            if (rwcs.IsNullOrEmpty()) return null;
            var procs = new List<Proc>();
            foreach (var rf in rwcs)
            {
                if (!rf.ShouldCreate()) continue;
                var col = _bundle.Columns.FirstOrDefault(c => c.Name.EqualsOic(rf.Column));
                if (null == col)
                {
                    throw new ArgumentException($"{rf.Name}.{rf.Column}\r\n{exceptionText}");
                }
                procs.AddRange(r(rf, col));
            }
            return procs.Any() ? new MultiAppendable(procs, "\r\nGO\r\n") : null;
        }

        private Proc GetReadProc(ReadWithCol rf, string extraParameters, string extraWhere, string qualifier = null)
            => GetReadProc(rf, new[] { extraParameters }, new[] { extraWhere }, qualifier);
        private Proc GetReadProc(ReadWithCol rf, IEnumerable<string> extraParameters, IEnumerable<string> extraWhere, string qualifier = null)
        {
            qualifier = qualifier ?? rf.GetParameters(ParameterMode.Qualifier).FirstOrDefault()?.Name;
            var parameters = rf.Filter(_bundle.Columns, ParameterMode.Parameter)
                .Select(col => _cr.DeclareString(col, false))
                .Union(extraParameters)
                .Union(rf.Filter(_bundle.Columns, ParameterMode.Qualifier).Select(col => _cr.DeclareString(col, true)))
                .NotNull()
                .ToList();
            var where = rf.AllParameters
                .Collect(_cr.Where)
                .Add(extraWhere)
                .ToList();

            var name = rf.Name + (rf.Number == 1 ? "" : rf.Number.ToString());
            if (qualifier.IsNullOrEmpty())
                return new Proc(name, parameters)
                    .With(rf.OutputJson
                      ? (IAppendable)new SelectJson(_bundle, null, rf.ReturnsList, where)
                      : new Select(_bundle.Columns, _bundle.Table.Name, where));
            if (!rf.OutputJson)
                return new Proc(name, parameters).WithQualifier(qualifier,
                    _ => new Select(_bundle.Columns, _bundle.Table.Name, where));
            return new Proc(name, parameters)
                .WithQualifier(qualifier, q => new SelectJson(_bundle, q, rf.ReturnsList, where));
        }
        private Proc GetReadBetween(ReadWithCol rf, ColumnProperties col)
        {
            var template = _cr.DeclareString(col, true);
            var parameters = new[] { "min", "max" }.Select(s => template.Insert(1, s));
            var where = new[] { _cr.Where(col, "min", "<="), _cr.Where(col, "max", ">=") };
            string qualifier = null;
            var qParam = rf.GetParameters(ParameterMode.Qualifier).FirstOrDefault();
            if (null != qParam)
                qualifier = $"@{qParam.Name}";
            return GetReadProc(rf, parameters, where, qualifier);
        }

        private Proc GetReadFor(ReadWithCol rf, ColumnProperties col) => GetReadProc(rf, _cr.DeclareString(col, true), _cr.Where(col, false));

        Proc GetMaxFor(ReadWithCol rf, ColumnProperties col)
        {
            var pNames = rf.GetParameterNames(ParameterMode.Parameter, ParameterMode.Qualifier);
            var ps = _bundle.Columns.Where(c => pNames.Contains(c.Name)).ToList();
            var qualifier = rf.GetParameterNames(ParameterMode.Qualifier).FirstOrDefault();
            var where = ps.Any()
                ? $"{rf.Column} in (SELECT Max({rf.Column}) FROM {_bundle.RootNode} WHERE \r\n\t{_cr.Where(rf.AllParameters, true).Join(" AND\r\n\t")})"
                : $"{rf.Column} in (SELECT Max({rf.Column}) FROM {_bundle.RootNode})";

            return GetReadProc(rf, null, where, qualifier);
        }
        Proc GetDeleteFor(ReadWithCol rf, ColumnProperties col)
        {
            var pNames = rf.GetParameterNames(ParameterMode.Parameter);
            var ps = new[] { col }.Union(_bundle.Columns.Where(c => pNames.Contains(c.Name))).ToList();
            return new Proc(rf.Name, ps.Select(k => _cr.DeclareString(k, false))).With(new SqlDelete(_bundle.RootNode.Name, ps.Select(c => c.Name), false));
        }
    }
}
