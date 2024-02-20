using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Newtonsoft.Json;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using SupplyChain.Procs;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Objects;

namespace ScriptOMatic.Generate.Extensions
{
    public static class BundleExtensions
    {
        public static PopulatedBundle Populate(this Bundle b, CodeEnvironment env, List<ColumnProperties> columns, TableNode root)
        {
            var def = env.Db.LoadDbDef();
            var ret = new PopulatedBundle(b)
            {
                DtoPop = new PopulatedDtoInfo(b.Dto).Populate(env.Dto),
                RepoPop = new PopulatedRepoInfo(b.Repo).Populate(env.Repo),
                Columns = columns,
                RootNode = root,
                EnumFields = env.GetEnumFields(),
                LinkTable = def.LinkTables.FirstOrDefault(t=>t.Name.EqualsOic(b.Table.Name))?.Populate(root)
            };
            return ret;
        }

        public static IEnumerable<SubQuery> SubQueries(this PopulatedBundle b)
        {
            if (!((b.Procedures.Maybe(Bundle.SpType.Create)?.OutputJson??false) 
                || (b.Procedures.Maybe(Bundle.SpType.Read)?.OutputJson??false)) )
                yield break;
            
            if (b.RootNode.Alias.IsNullOrEmpty())
                b.RootNode.Alias = $"[{b.RootNode.Name.Where(char.IsUpper).AsString()}]";
            foreach (var agg in b.Aggregates)
            {
                var child = b.RootNode.Children.First(c => c.Name.Equals(agg.Table));
                if (child.Alias.IsNullOrEmpty()) child.Alias = $"[{child.Name.Where(char.IsUpper).AsString()}]";
                yield return agg.ToSubQuery(child, b.RootNode.Name, b.RootNode.Alias);
            }
        }
        public static PopulatedBundle Populate(this Bundle b, CodeEnvironment env, TableNode root) =>
            Populate(b, env, root.Columns, root);

        public static bool RenderRepoAggregates(this PopulatedBundle b)
        {
            if (!b.Aggregates.Any()) return false;
            if (b.Read != null && !b.Read.OutputJson) return true;
            if (b.Upsert != null) return true;
            if (b.Update != null) return true;
            var c = b.Create;
            if (null != c)
            {
                return !c.InputJson || !c.OutputJson;
            }
            return false;
        }
        public static ClassDef GetDbDefinition(this PopulatedBundle b)
        {
            if (b?.Columns == null) return null;
            var typename = b.Table.Singular;
            var cls = new ClassDef { ClassName = b.Table.Plural };

            cls.Procedures.Add(b.Create)
                ?.WithCall(c => c
                    .WithType(typename)
                    .WithNullable(b.NullableColumns())
                    .Returns(typename, b.Create.OutputJson));


            var g = cls.Procedures.Add(b.Read);
            if (null != g)
            {
                if (b.Columns.Keys().Any())
                    g.WithCall(c => c.Returns(typename, b.Read.OutputJson));
                g.WithCall(c => c
                    .Hiding(b.Columns.Keys())
                    .ReturnsList(typename, b.Read.OutputJson));
            }

            bool returnJson = b.Read?.OutputJson ?? false;

            foreach (var rf in b.SpecializedProcedures.ReadBetween())
                cls.Procedures.Add(rf)
                    ?.WithCall(c => c
                        .Returns(typename, returnJson, rf.ReturnsList)
                        .WithNullable("min" + rf.Column, "max" + rf.Column)
                        .WithNullable(rf.GetParameters(ParameterMode.Qualifier)));

            foreach (var rf in b.SpecializedProcedures.ReadFor())
                cls.Procedures.Add(rf)
                    ?.WithCall(c => c
                        .Returns(typename, returnJson, rf.ReturnsList)
                        .WithNullable(rf.NonColumnParameters())
                        .WithNullable(rf.GetParameters(ParameterMode.Qualifier)));
            foreach (var rf in b.SpecializedProcedures.ReadForMax())
                cls.Procedures.Add(rf)
                    ?.WithCall(c => c
                        .Returns(typename, returnJson, rf.ReturnsList)
                        .WithNullable(rf.NonColumnParameters())
                        .WithNullable(rf.GetParameters(ParameterMode.Qualifier)));
            cls.Procedures.Add(b.ReadIdIn)
                ?.WithCall(c => c.Returns(typename, returnJson, true));
            cls.Procedures.Add(b.Update)
                ?.WithCall(c => c
                    .Void()
                    .WithType(typename)
                    .WithNullable(b.NullableColumns()));
            cls.Procedures.Add(b.Upsert)
                ?.WithCall(c => c
                    .WithType(typename)
                    .Returns(typename)
                    .WithNullable(b.NullableColumns()));
            if (b.Upsert?.InputJson ?? false)
            {
                var info = new SpInfo(b.Upsert);
                info.Name += "JSON";
                info.CodeName = "UpsertJson";
                cls.Procedures.Add(info).WithCall(c => c.ReturnsList(typename, false));
            }
                

            cls.Procedures.Add(b.Delete)
                ?.WithCall(c => c.Void());
            foreach (var rf in b.SpecializedProcedures.DeleteFor())
                cls.Procedures.Add(rf)
                    ?.WithCall(c => c.Void());

            cls.Procedures.AddRange(b.HandCoded);
            return cls;
        }

        public static IEnumerable<ColumnProperties> NullableColumns(this PopulatedBundle b) =>b.Columns.Where(c => c.Nullable);
        public static List<ReadWithCol> ReadBetween(
            this Dictionary<Bundle.SpecializedProcedureType, List<ReadWithCol>> dict) =>
            dict[Bundle.SpecializedProcedureType.ReadBetween];
        public static List<ReadWithCol> ReadFor(
            this Dictionary<Bundle.SpecializedProcedureType, List<ReadWithCol>> dict) =>
            dict[Bundle.SpecializedProcedureType.ReadFor];
        public static List<ReadWithCol> ReadForMax(
            this Dictionary<Bundle.SpecializedProcedureType, List<ReadWithCol>> dict) =>
            dict[Bundle.SpecializedProcedureType.ReadForMax];
        public static List<ReadWithCol> DeleteFor(
            this Dictionary<Bundle.SpecializedProcedureType, List<ReadWithCol>> dict) =>
            dict[Bundle.SpecializedProcedureType.DeleteFor];

        public static IEnumerable<string> SpNames(this Bundle b)
        {
            foreach (var kvp in b.Procedures)
            {
                yield return kvp.Value.Name;
                if (kvp.Key == Bundle.SpType.Upsert && kvp.Value.InputJson)
                    yield return kvp.Value.Name + "JSON";
            }

            foreach (var sp in b.SpecializedProcedures.SelectMany(kvp => kvp.Value.NotNull()))
            {
                yield return sp.Name + (sp.Number > 1 ? sp.Number.ToString() : "");
            }
        }
    }
}
