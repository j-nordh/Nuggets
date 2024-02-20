using ScriptOMatic.Pages;
using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.CodeGeneration;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Lists;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

namespace ScriptOMatic.Generate
{
    public class DtoRenderer : Renderer<List<ColumnProperties>>
    {
        private readonly PopulatedBundle _bundle;

        public DtoRenderer(PopulatedBundle bundle)
        {
            _bundle = bundle;
        }

        protected override IEnumerable<IAppendable> Render(List<ColumnProperties> cols)
        {
            yield return new FileBuilder { Namespace = _bundle.DtoPop.Namespace, UsingBlock = Blocks[0] }
                .Add(new DtoBuilder(cols, _bundle));
        }

        protected override void CombineTo(List<IAppendable> parts, IndentingStringBuilder sb) => sb.AppendObject(parts.FirstOrDefault());
        public static HandCodedBlock[] Blocks => DtoBuilder.Blocks;
    }

    internal class DtoBuilder : SimpleClassBuilder
    {
        private readonly List<ColumnProperties> _allCols;
        private readonly List<ColumnProperties> _straightCols;
        private readonly List<ColumnProperties> _reverseCols;
        readonly PopulatedBundle _bundle;
        readonly ColumnRenderer _cr;
        private readonly HandCodedBlock _constructorHCB;
        private readonly HandCodedBlock _bodyHCB;
        private readonly HandCodedBlock _hasherHCB;

        public static HandCodedBlock[] Blocks = { HandCodedBlock.FromKeyword("Using"), HandCodedBlock.FromKeyword("Constructor"), new HandCodedBlock(), HandCodedBlock.FromKeyword("Hasher") };

        public DtoBuilder(List<ColumnProperties> cols, PopulatedBundle bundle) : base(bundle.Table.Singular)
        {
            _bundle = bundle;
            _allCols = cols;

            _straightCols = cols.Where(c => !_bundle.Aggregates.Any(c)).ToList();
            _reverseCols = cols.Where(c => bundle.Aggregates.Reversed().Any(ra => ra.ForeignKeyColumn.EqualsOic(c.Name))).ToList();
            _cr = new ColumnRenderer(bundle.JsonFields, _bundle.EnumFields) { TableName = _bundle.Table.Name };
            _constructorHCB = Blocks[1];
            _bodyHCB = Blocks[2];
            _hasherHCB = Blocks[3];
        }

        public override IEnumerable<string> Implements => _bundle.Implements(_allCols).ToList().MaybeInclude(_bundle.MatchColumns != null, $"IMatchable<{Name}>");
        public override IEnumerable<string> Requires => _bundle.Dto.Using().Union(new List<string> {
            { _bundle.Aggregates.Matching(AggregateTypes.Normal, AggregateTypes.Linked).IsNotNullOrEmpty() ,"System.Collections.Generic"},
            { _bundle.Aggregates?.Any(),"System.Linq"},
            {_bundle.Dto.DecimalPlaces!= null, "UtilClasses.Extensions.Decimals" },
            {_bundle.MatchColumns?.Any() ?? false, "UtilClasses.Extensions.Strings"},
            {_bundle.MatchColumns?.Any() ?? false,"UtilClasses"},
            "Common.Interfaces",
            //{_bundle.EnumFields.Any(), "Newtonsoft.Json"},
            //{_bundle.EnumFields.Any(), "Newtonsoft.Json.Converters"}
        }.NotNull());
        public override IEnumerable<string> ConstructorParameters => new string[] { };

        /*        private long _dpThresholdId;
        public long DPThresholdId
        {
            get => _dpThresholdId;
            set
            {
                _dpThresholdId = value;
                DPThresholds = null;
            }
        }*/
        protected override void ConstructorBody(IndentingStringBuilder sb)
        {
            sb
                .AppendObjects(_bundle.Aggregates.Normal(), a => $"{a.Alias} = new List<{StringUtil.ToSingle(a.Table)}>();")
                .AppendObject(_constructorHCB.Empty());
        }
        protected override void Preamble(IndentingStringBuilder sb) => sb
            .AppendObjects(_straightCols, c => _cr.DtoString(c, _bundle.DtoPop.Properties))
            .Maybe(_bundle.Aggregates.Any(), (Func<IndentingStringBuilder>)(() => sb
               .AppendLine("#region Aggregates")
               .AppendObjects(_bundle.Aggregates.Normal(), a => $"public List<{StringUtil.ToSingle(a.Table)}> {a.Alias} {{ get; set; }}")
               .AppendObjects(_bundle.Aggregates.Reversed(), (a =>
               {
                   var n = StringUtil.ToSingle(a.Table);
                   var col = _reverseCols.Where(c => c.Name.EqualsOic(a.ForeignKeyColumn)).Single();
                   var varName = $"_{_cr.ParameterName(col)}";
                   sb.AppendLine($"private long {varName};")
                   .AppendLines($"public long {col.CodeName}",
                   "{",
                   $"get => {a.Alias}?.Id ??{varName};",
                   "set",
                   "{",
                   $"{varName} = value;",
                   $"{a.Alias} = null;",
                   "}", "}", $"public {StringUtil.ToSingle(a.Table)} {a.Alias} {{ get; set;}}");
               }))
               .AppendObjects(_bundle.Aggregates.Linked().Where(this.IsEnum), la => $"public HashSet<{_bundle.EnumFields[la.Table]}> {la.Alias} {{ get; set; }}")

               .AppendObjects(_bundle.Aggregates.Linked().Where(this.IsNotEnum), la =>
               {
                   var (me, other) = la.Link.Split(_bundle.Table.Name);
                   return $"public List<{StringUtil.ToSingle(other.Table)}> {la.Alias} {{get; set;}}";
               })
               .AppendLine("#endregion")));

        private bool IsEnum(LinkedAggregate la) => _bundle.EnumFields.ContainsKey(la.Table);
        private bool IsNotEnum(LinkedAggregate la) => !IsEnum(la);
        protected override void ClassBody(IndentingStringBuilder sb) =>
            sb
                .Maybe(_bundle.DtoPop.Cloneable, CloneWithConstructor)
                .Maybe(_bundle.Aggregates.Any(), AggregatesFlags, Assumer)
                .Maybe(_bundle.DtoPop.Stateful, Hasher)
                .AppendObject(_bodyHCB.Empty(true))
                .Maybe(_bundle.MatchColumns != null, Matchable);

        private IndentingStringBuilder CloneWithConstructor(IndentingStringBuilder sb) => sb
            .AppendLines($"public {_bundle.Table.Singular}({_bundle.Table.Singular} o): this()", "{")
            .AppendObjects(_straightCols, c => $"{c.CodeName} = o.{c.CodeName};")
            .AppendObjects(_bundle.Aggregates.Reversed(), ra => $"{ra.Alias} = o.{ra.Alias};")
            .AppendObjects(_bundle.Aggregates.Normal(), a => $"{a.Alias} = o.{a.Alias} != null ? new List<{StringUtil.ToSingle(a.Table)}>(o.{a.Alias}) : null;")
            .AppendLines("}", $"public virtual {_bundle.Table.Singular} Clone() => new {_bundle.Table.Singular}(this);");

        private IndentingStringBuilder Matchable(IndentingStringBuilder sb) => sb
            .AppendLines("public int GetMatchHash()", "{", "int hash = 23;", "unchecked", "{")
            .AppendObjects(_bundle.MatchColumns.Select(_bundle.Columns.ByName), c=>_cr.HashString(c, _bundle.Dto.DecimalPlaces, _bundle.Dto.MatchNormalizeLineBreak, _bundle.Dto.MatchIgnoreCase, _bundle.Dto.MatchIgnoreWhitespace))
            .AppendLines("}", "return hash;", "}", "", $"public bool Matches({Name} other) =>")
            .Indent(x=>x
                .Append(_bundle.MatchColumns.Select(_bundle.Columns.ByName).Select(c=>   _cr.EqualsString(c, c.Precision, _bundle.Dto.MatchNormalizeLineBreak, _bundle.Dto.MatchIgnoreCase, _bundle.Dto.MatchIgnoreWhitespace)).Join("\n&& "))
                .AppendLine(";"));

        
        private IndentingStringBuilder Hasher(IndentingStringBuilder sb) =>
            sb.AppendLines("public int GetStateCode()", "{", "var hash = 23;", "unchecked", "{")
            .AppendObjects(_allCols, _cr.HashString(_bundle.Dto.DecimalPlaces))
            .AppendObjects(_bundle.Aggregates.Matching(AggregateTypes.Linked, AggregateTypes.Normal), _cr.HashString)// ignore changes in reverse aggregates
            .AppendObject(_hasherHCB.Empty())
            .AppendLines(
                "}",
                "return hash;",
                "}",
                "[JsonIgnore]",
                "public int OriginalStateCode { get; private set; }",
                "public void MarkAsClean() => OriginalStateCode = GetStateCode();",
                "[OnDeserialized]",
                "internal void OnDeserialized(StreamingContext sc) => MarkAsClean();");
        private IndentingStringBuilder AggregatesFlags(IndentingStringBuilder sb) => sb
            .AppendLines("[Flags]", "public enum Aggregates", "{")
            .AppendLine("None = 0,")
            .AppendObjects(_bundle.Aggregates.Select(a => a.Alias).Union(_bundle.Aggregates.Linked().Select(la => la.Alias)), (a, i) => $"{a} = {Math.Pow(2, i)},")
            .AppendLine($"All = {Math.Pow(2, _bundle.Aggregates.Count()) - 1},")
            .AppendLine($"Recursive = {Math.Pow(2, _bundle.Aggregates.Count)}")
            .AppendLine("}");


        private IndentingStringBuilder Assumer(IndentingStringBuilder sb)
        {

            var aggs = _bundle?.Aggregates?.Where(a => !a.ReadOnly)?.ToList() ?? new List<Aggregate>();
            if (!aggs.Any()) return sb;

            return sb.AppendFunction("AssumeAggregates", $"{_bundle.Table.Singular} obj", fb => fb
                .Returning($"{_bundle.Table.Singular}")
                .With(aggs.Normal(), a => $"{a.Alias} = obj.{a.Alias}?.Select(o => new {StringUtil.ToSingle(a.Table)}(o)).ToList();") //create copies of the aggregates
                .With(aggs.Normal(), a => $"{a.Alias}?.ForEach(o => o.{_bundle.Table.Singular}Id = Id);") //ensure that the owners ID is set
                .With(aggs.Reversed(), a => $"{a.Alias} = obj.{a.Alias};")//reuse the object, it should, normally, not be changed anyway
                .With(aggs.Linked(), a => $"{a.Alias} = obj.{a.Alias}?.Select(o=>new {StringUtil.ToSingle(a.Alias)}(o)).ToList();")
                .With("return this;"));
        }

        protected override string AccessModifier => "public";
    }
    static class DtoInfoExtensions
    {
        public static IEnumerable<string> Implements(this PopulatedBundle b, List<ColumnProperties> cols) => new List<string>()
        {
            (b.DtoPop.HasWriteId, "IHasWriteId"),
            (!b.DtoPop.HasWriteId && cols.Any(c=>c.Name.EqualsOic("Id")), "IHasId"),
            (b.DtoPop.Stateful, "IStateful"),
            (b.DtoPop.Cloneable, $"ICloneable<{b.Table.Singular}>")
        }.Union(b.DtoPop.Implements.Select(id => id.Name.Replace("<>", $"<{id.Type}>")));
        public static IEnumerable<string> Using(this DtoInfo i) => new List<string>()
        {
            "System",
            (i.HasWriteId || i.Stateful || i.Cloneable, "Common.Interfaces"),
            (i.Stateful, new[]{"System.Runtime.Serialization", "Newtonsoft.Json" }),
        }.Union(i.Implements.Select(id => id.Namespace));
    }
}
