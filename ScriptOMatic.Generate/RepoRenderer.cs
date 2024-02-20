using ScriptOMatic.Generate.Extensions;
using ScriptOMatic.Pages;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Lists;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

namespace ScriptOMatic.Generate
{
    public class RepoRenderer
    {
        readonly IndentingStringBuilder _sb;
        readonly CodeEnvironment _env;

        public RepoRenderer(IndentingStringBuilder sb, CodeEnvironment settings)
        {
            _env = settings;
            _sb = sb;
        }

        public void Append(PopulatedBundle bundle, bool permissionControlled)
        {
            if (bundle.Table.Name.IsNullOrWhitespace())
            {
                _sb.AppendLine("No table selected");
                return;
            }
            _sb.AppendObject(
                new FileBuilder() { Namespace = _env.Repo.Namespace, UsingBlock = Blocks[0] }
                .Add(new RepoClass(bundle, permissionControlled)));
        }
        public static HandCodedBlock[] Blocks = { HandCodedBlock.FromKeyword("Using"), new HandCodedBlock() };
    }

    partial class RepoClass : SimpleClassBuilder
    {
        private readonly PopulatedBundle _bundle;
        readonly bool _permissions;
        private readonly IndentingStringBuilder _body = new IndentingStringBuilder("\t");
        private readonly List<ColumnProperties> _keys;
        private readonly ColumnRenderer _cr;

        public RepoClass(PopulatedBundle bundle, bool permissions) : base(bundle.Table.Plural + "Repo")
        {
            _bundle = bundle;
            _permissions = permissions;
            _keys = _bundle.Columns.Where(c => c.IsKey).ToList();
            _cr = new ColumnRenderer(new Dictionary<string, JsonField>(), _bundle.EnumFields)
            {
                TableName = _bundle.Table.Name
            };
            AddSingle(_bundle.Create, GetCreate);
            Add(_bundle.Read, GetReadOne, GetReadAll);
            AddRwcM(_bundle.Specialized.ReadBetween, AddReadBetween, "ReadBetween");
            AddRwcM(_bundle.Specialized.ReadFor, AddReadFor, "ReadFor");
            AddRwcM(_bundle.Specialized.ReadForMax, AddReadForMax, "ReadForMax");
            Add(_bundle.ReadIdIn, GetReadIdIn);
            Add(_bundle.Update, GetUpdate);
            Add(_bundle.Upsert, GetUpsert);
            AddSingle(_bundle.Delete, GetDelete);
            AddRwcS(_bundle.Specialized.DeleteFor, GetDelFor, "DelFor");
            AddAggregates();
            _body.AppendObject(RepoRenderer.Blocks[1].Empty());

        }

        private IAppendable GetDelFor(ReadWithCol rwc, ColumnProperties col)
        {
            var ps = rwc.Filter(_bundle.Columns, col, ParameterMode.Parameter);
            return VoidBuilder(rwc.CodeName)
                .WithParameters(_cr.ParameterStrings(ps))
                .Inline($"_db.ExecuteNonQuery(def.{rwc.CodeName}({_cr.Join(ps).Casted()}));");
        }


        public override string BaseConstructorParameters => "creator";
        public override IEnumerable<string> ConstructorParameters => new[] { "RepoCreator creator", _permissions ? "PermissionGroup." + _bundle.Table.Name : null }.NotNull();
        public override IEnumerable<string> Implements => new[] {
            _bundle.RepoPop.Base + (_bundle.RepoPop.BaseIsGeneric ? $"<{_bundle.Table.Singular}>" : ""),
            _bundle.IsCrud ? $"ICrudRepo<{_bundle.Table.Singular}>" : null,
            _bundle.RepoPop.ImplementUpsertRepo ? $"IUpsertRepo<{_bundle.Table.Singular}>":null,
            _bundle.RepoPop.ImplementMultiUpsertRepo?$"IMultiUpsertRepo<{_bundle.Table.Singular}>":null
            }.NotNull();
        public override IEnumerable<string> Requires
        {
            get
            {
                var ret = new List<string>();
                ret.AddRange(
                    _bundle.DtoPop.Namespace,
                    $"def = {_bundle.RepoPop.DefNamespace}.{_bundle.Table.Plural}",
                    "System",
                    "System.Linq");

                if (_bundle.Read != null ||
                    _bundle.Specialized.ReadBetween.IsNotNullOrEmpty() ||
                    _bundle.Specialized.ReadFor.IsNotNullOrEmpty())
                    ret.Add("System.Collections.Generic");

                ret.Add(_bundle.IsCrud, "UtilClasses.Db");
                ret.Add(_bundle.RenderRepoAggregates(), "UtilClasses.Extensions.Enumerables");


                if (_bundle.Repo.ImplementUpsertRepo || _bundle.Repo.ImplementMultiUpsertRepo || (_bundle.Upsert?.InputJson ?? false))
                {
                    ret.AddRange("UtilClasses.Db", "UtilClasses.Extensions.Enumerables");
                    if (_bundle.Aggregates.Any())
                        ret.Add("UtilClasses.Extensions.Matchables");
                }

                ret.Add(_bundle.ReadIdIn.ShouldCreate(), "UtilClasses.Extensions.Strings");
                ret.AddRange((_bundle.Upsert?.InputJson ?? false) || (_bundle.Upsert?.InputJson ?? false), "Newtonsoft.Json", "UtilClasses.Extensions.Strings");
                ret.Add(_bundle.Dto.Stateful, "Common.Interfaces");
                return ret.Distinct(StringComparer.OrdinalIgnoreCase);
            }
        }

        protected override void ClassBody(IndentingStringBuilder sb) => sb.AppendLine(_body.ToString());



        private void AddRwcM(IEnumerable<ReadWithCol> specs, Func<ReadWithCol, ColumnProperties, IEnumerable<IAppendable>> renderer, string marker) =>
            ForEachRwc(specs, (rwc, col) => _body.AppendObjects(renderer(rwc, col)), marker);
        private void AddRwcS(IEnumerable<ReadWithCol> specs, Func<ReadWithCol, ColumnProperties, IAppendable> renderer, string marker) =>
           ForEachRwc(specs, (rwc, col) => _body.AppendObject(renderer(rwc, col)), marker);
        private void ForEachRwc(IEnumerable<ReadWithCol> specs, Action<ReadWithCol, ColumnProperties> a, string marker)
        {
            if (specs.IsNullOrEmpty())
                return;
            foreach (var spec in specs)
            {
                if (!spec.ShouldCreate()) continue;
                var col = _bundle.Columns.ByName(spec.Column);
                if (null == col)
                    throw new ArgumentException($"Could not generate a {marker} function since no column matching the specification could be found.");
                a(spec, col);
            }
        }



        private void Add(SpInfo info, params Func<IEnumerable<IAppendable>>[] fs)
        {
            if (!info.ShouldCreate()) return;
            foreach (var f in fs)
                _body.AppendObjects(f());
        }
        private void AddSingle(SpInfo info, params Func<IAppendable>[] fs)
        {
            if (!info.ShouldCreate()) return;
            foreach (var f in fs)
                _body.AppendObject(f());
        }
        private IEnumerable<IAppendable> AddReadFor(ReadWithCol spec, ColumnProperties col) => GetRead(spec, col, null, true);
        private IEnumerable<IAppendable> AddReadBetween(ReadWithCol spec, ColumnProperties col) =>
            GetRead(spec, col, new[] { _cr.ParameterString(col, "min", true), _cr.ParameterString(col, "max", true) }, false, null, s => s.SubstringBefore("="));
        private IEnumerable<IAppendable> AddReadForMax(ReadWithCol spec, ColumnProperties col) => GetRead(spec, col, null, false);

        private IEnumerable<IAppendable> GetRead(ReadWithCol spec, ColumnProperties col, IEnumerable<string> extraParameters = null, bool includeColumn = true, string methodName = null, Func<string, string> extraFormatter = null)
        {
            var paramCols = spec.Filter(_bundle.Columns, ParameterMode.Parameter).ToList();
            var name = spec.CodeName;
            var r = new ReadRenderer(_cr, _bundle, spec.CodeName, col)
            {
                ParameterColumns = spec.Filter(_bundle.Columns, ParameterMode.Parameter)?.ToList(),
                QualifyingColumn = spec.Filter(_bundle.Columns, ParameterMode.Qualifier).FirstOrDefault(),
                ExtraParameters = extraParameters?.ToList(),
                ExtraFormatter = extraFormatter
            };
            return r.GetRead(spec.ReturnsList, includeColumn, methodName);
        }
        private IEnumerable<IAppendable> GetRead(List<ColumnProperties> paramCols, string name, bool returnsList, IEnumerable<string> extraParameters = null, string methodName = null)
            => new ReadRenderer(_cr, _bundle, name, null)
            {
                ParameterColumns = paramCols,
                ExtraParameters = extraParameters?.ToList()
            }.GetRead(returnsList, false, methodName);
        private IEnumerable<IAppendable> GetRead(string name, bool returnsList, IEnumerable<string> extraParameters = null, string methodName = null)
            => new ReadRenderer(_cr, _bundle, name, null) { ExtraParameters = extraParameters?.ToList() }.GetRead(returnsList, false, methodName);
        private IEnumerable<IAppendable> GetRead(string name, bool returnsList, string extraParameter, string methodName = null, Func<string, string> extraFormatter = null)
            => new ReadRenderer(_cr, _bundle, name, null)
            {
                ExtraParameters = new List<string> { extraParameter },
                ExtraFormatter = extraFormatter,
            }.GetRead(returnsList, false, methodName);

        private IEnumerable<IAppendable> GetReadOne()
        {
            var keys = _bundle.Columns.Where(c => c.IsKey).ToList();
            return keys.Any()
                ? GetRead(keys.ToList(), "Get", false)
                : null;
        }

        private IEnumerable<IAppendable> GetReadAll() => GetRead("Get", true, methodName: "All");
        private IEnumerable<IAppendable> GetReadIdIn() => GetRead("GetForIds", true, "IEnumerable<long> ids", "GetForIds", _ => "ids.Select(i=>i.ToString()).Join(\",\")");
        private IEnumerable<IAppendable> GetUpsert()
        {
            return GetUpsertSingle().Union(GetUpsertMulti());

        }

        private IEnumerable<IAppendable> GetUpsertMulti()
        {
            FunctionBuilder GetFb() => FunctionBuilder("", "Upsert")
                .ReturningList(_bundle.Table.Singular)
                .WithParameter($"IEnumerable<{_bundle.Table.Singular}> items");



            var ret = new List<IAppendable>();
            if (!_bundle.RenderRepoAggregates())
            {
                ret.Add(FunctionBuilder(_bundle.Table.Singular, "Upsert")
                    .WithParameter($"{_bundle.Table.Singular} o")
                    .Inline(" _db.QueryOne(def.Upsert(o));"));
                if (_bundle.Upsert.InputJson)
                    ret.Add(GetFb()
                        .Inline("items",
                        ".Paginate(500, page => _db.Query(def.UpsertJson(JsonConvert.SerializeObject(page).EscapeForSql())))",
                        ".Union()",
                        ".ToList();"));

                return ret;
            };

            ret.Add(GetFb().Inline($"Upsert(items, {_bundle.Table.Singular}.Aggregates.All);"));

            ret.Add(GetFb()
                .WithParameter($"{_bundle.Table.Singular}.Aggregates save")
                .WithBuilder(sb => sb
                    .AppendLines(
                        "foreach(var item in items)",
                        "{",
                        "PreSave(item);",
                        "}",
                        "var newItems = _db.Query(def.UpsertJson(JsonConvert.SerializeObject(items).EscapeForSql()));",
                        "var res = newItems.MatchWith(items);",
                        "if (!res.Perfect)",
                        "{",
                        "var sb = new IndentingStringBuilder(\"  \")")
                    .Indent(x => x.AppendLines(
                            ".AppendLine(\"Could not achieve a perfect match\")",
                            ".AppendLine(\"UnmatchedFirst:\")",
                            ".Indent(x=>x.AppendLines(res.UnmatchedFirst.Select(o=>$\"{o} MatchHash: {o.GetMatchHash()}\")))",
                            ".AppendLine(\"UnmatchedSecond:\")",
                            ".Indent(x=>x.AppendLines(res.UnmatchedSecond.Select(o=>o.ToString())));"))
                    .AppendLines(
                        "throw new Exception(sb.ToString());",
                        "}",
                        "var ret = res.Merge((a, b) => a.AssumeAggregates(b)).ToList();", 
                        "", 
                        "PostSave(ret);", 
                        "return ret;")
                ));
            return ret;
        }

        private IEnumerable<IAppendable> GetUpsertSingle()
        {
            if (!_bundle.RenderRepoAggregates()) return new IAppendable[] { };
            var t = _bundle.Table.Singular;
            return FunctionBuilder(t, "Upsert")
                .WithParameter($"{t} o")
                .Split(
                    a => a.Inline($"Upsert(o, {t}.Aggregates.All);"),
                    b => b
                        .WithParameter($"{t}.Aggregates save")
                        .With("PreSave(o, save);",
                            "var ret = _db.QueryOne(def.Upsert(o));",
                            "ret.AssumeAggregates(o);",
                            "PostSave(new []{ret}, save);",
                            "return ret;"));
        }

        private FunctionBuilder FunctionBuilder(string returns, string name) => new FunctionBuilder().WithName(name).Returning(returns);
        private FunctionBuilder TBuilder(string name) => new FunctionBuilder().WithName(name).Returning(_bundle.Table.Singular);
        private FunctionBuilder VoidBuilder(string name) => new FunctionBuilder().WithName(name).Returning("void");
        private IAppendable GetCreate()
        {
            var builder = TBuilder("Create")
                .WithParameter($"{_bundle.Table.Singular} obj");
            if (!_bundle.RenderRepoAggregates())
                return builder.Inline("_db.QueryOne(def.Create(obj));");

            return builder.With(
                "PreSave(obj);",
                "var ret = _db.QueryOne(def.Create(obj));",
                "ret.AssumeAggregates(obj);",
                "PostSave(new []{ret});",
                "return ret;");
        }

        private IEnumerable<IAppendable> GetUpdate()
        {
            var t = _bundle.Table.Singular;
            var fb = VoidBuilder("Update").WithParameter($"{t} obj");
            if (!_bundle.RenderRepoAggregates())
            {
                return new[] { fb.Inline("_db.ExecuteNonQuery(def.Update(obj));") };
            }

            return fb.Split(
                a => a.Inline($"Update(obj,{t}.Aggregates.All);"),
                b => b.WithParameter($"{t}.Aggregates save")
                      .With("PreSave(obj, save);",
                            "_db.ExecuteNonQuery(def.Update(obj));",
                            "PostSave(new[]{obj}, save);"
                ));
        }

        private IAppendable GetDelete() =>
            VoidBuilder("Delete")
            .WithParameters(_cr.ParameterStrings(_keys))
            .Inline($"_db.ExecuteNonQuery(def.Delete({_cr.Join(_keys).Casted()}));");


        #region Aggregates

        private void AddAggregates()
        {
            if (!_bundle.RenderRepoAggregates()) return;

            AppendLine("#region Aggregates")
                .AddLoader()
                .AddSaver()
                .AppendLine("#endregion");
        }
        private RepoClass AddLoader() => this.Do(_bundle.RenderRepoAggregates(), () =>
        {
            if (_bundle.Read.OutputJson) return;
            var t = _bundle.Table.Singular;
            _body
                .AppendFunction($"private {t} LoadAggregates", cfg => cfg
                    .WithObj(t)
                    .WithParameter($"{t}.Aggregates load")
                    .With(_bundle.Aggregates.Normal(), AppendLoad)
                    .With(_bundle.Aggregates.Reversed(), AppendLoad))
                .AppendFunction($"private List<{t}> LoadAggregates", $"List<{t}> objs, {t}.Aggregates load", cfg => cfg
                    .Inline($"objs?.Select(o => LoadAggregates(o, load)).ToList() ??  new List<{t}>();"));
        });


        private void AppendLoad(IndentingStringBuilder sb, Aggregate a)
        {
            if (a is LinkedAggregate) return;
            sb.AppendLines($"if (load.HasFlag({ _bundle.Table.Singular}.Aggregates.{a.Alias}))", "{",
                $"var flags = {a.TargetType}.Aggregates.None;",
                $"if (load.HasFlag({_bundle.Table.Singular}.Aggregates.All) && load.HasFlag({_bundle.Table.Singular}.Aggregates.Recursive))")
                .IndentLine($"flags = {a.TargetType}.Aggregates.All & {a.TargetType}.Aggregates.Recursive");
            switch (a)
            {
                case ReverseAggregate ra:
                    var col = _bundle.Columns.Single(c => c.Name.EqualsOic(ra.Column));
                    sb.IndentLine($"obj.{a.Alias} = _creator.{a.Repo}.Get(obj.{col.CodeName});");
                    break;
                default:
                    sb.IndentLine($"obj.{a.Alias} = _creator.{a.Repo}.GetFor{_bundle.Table.Singular}(obj.Id);");
                    break;
            }

            sb.AppendLine("}");
            /*
             * var flags = ProcessCall.Aggregates.None;
                if (load.HasFlag(Recommendation.Aggregates.All) && load.HasFlag(Recommendation.Aggregates.Recursive))
                    flags = ProcessCall.Aggregates.All & ProcessCall.Aggregates.Recursive;
                obj.ProcessCall = _creator.ProcessCalls.Get(obj.ProcessCallId, flags) ;
             */
        }

        private void AppendLoad(IndentingStringBuilder sb, LinkedAggregate aggregate) =>
            sb.AppendLine($"if (load.HasFlag({ _bundle.Table.Singular}.Aggregates.{aggregate}))");
        private RepoClass AddSaver()
        {
            if (!_bundle.RenderRepoAggregates() || _bundle.IsReadOnly()) return this;

            var t = _bundle.Table.Singular;
            _body
                .AppendFunction($"private List<{t}> PostSave", fb => fb
                    .WithObjs(t)
                    .Inline($"PostSave(objs, {t}.Aggregates.All);"))
                .AppendFunction($"private List<{t}> PostSave", fb => fb
                .WithObjs(t)
                .WithParameter($"{t}.Aggregates save")
                .WithBuilder(sb => sb
                    .AppendObjects(_bundle.Aggregates.Where(a => !a.ReadOnly), agg =>
                      {
                          sb.AppendLines(
                            $"if(save.HasFlag({t}.Aggregates.{agg.Alias}))", "{",
                            $"var {agg.Alias.ToLower()} = new List<{agg.TargetType}>();")
                        .AppendForEach("obj", "lst", $"{agg.Alias.ToLower()}.AddRange(obj.{agg.Alias});")
                        .AppendLine($"var q = _creator.{agg.Repo}.Upsert({agg.Alias.ToLower()}).ToQueue();")
                        .AppendForEach("obj", "lst", b => b
                            .AppendLine($"obj.{agg.Alias} = q.DequeueUpTo(obj.{agg.Alias}.Count()).ToList();")
                            .MaybeAppendLine(agg.Type == AggregateTypes.Linked, $"_creator.Links.SetLinks(obj, obj.{agg.Alias});"))
                        .AppendLine("}");
                      })));
            _body.AppendFunction($"private {t} PreSave", fb => fb
                    .WithObj(t)
                    .Inline($"PreSave(obj, {t}.Aggregates.All);"))
                .AppendFunction($"private {t} PreSave", fb => fb
                    .WithObj(t)
                    .WithParameter($"{t}.Aggregates save")
                .With(_bundle.Aggregates.Reversed(),
                    a => $"obj.{a.Alias} = _creator.{a.Repo}.Upsert(obj.{a.Alias});"));
            return this;
        }



        #endregion
        private IndentingStringBuilder ShouldCreate(SpInfo info) => info.ShouldCreate() ? _body : null;
        private RepoClass AppendLine(string line) => this.Do(() => _body.AppendLine(line));
        private IEnumerable<IAppendable> Maybe(SpInfo info, IEnumerable<IAppendable> objs) => info.ShouldCreate() ? objs : new IAppendable[0];
        private IEnumerable<IAppendable> Should(SpInfo info, IAppendable obj) => info.ShouldCreate() ? new[] { obj } : new IAppendable[0];

    }
    static class ColumnPropertyExtensions
    {
        public static bool IsString(this ColumnProperties c) => c.GetDtoType().EqualsOic("string");
        public static ColumnProperties ByName(this IEnumerable<ColumnProperties> cs, string name) => cs.FirstOrDefault(c => c.Name.EqualsOic(name));
    }
}

