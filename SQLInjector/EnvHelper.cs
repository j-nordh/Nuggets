using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ScriptOMatic.Generate;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

namespace SQLInjector
{
    public static class EnvHelper
    {
        public static string RunSql(string sql, CodeEnvironment env)
        {
            string lastPart = "";
            try
            {

                using (var conn = new SqlConnection($"Data Source={env.Db.Server}; Database={env.Db.Name}; Integrated Security=True;"))
                {
                    conn.Open();
                    var parts = sql.SplitREE("GO\n", "GO\r\n");
                    foreach (var part in parts)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            lastPart = part;
                            cmd.CommandText = part;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return $"Executed {parts.Count()} sql statements successfully";
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex, Formatting.Indented) + " Caught while executing:_\r\n" + lastPart;
            }

        }

        public static string RunDto(string sourceCode, PopulatedBundle bundle, CodeEnvironment env)
        {
            var cls = bundle.Table.Singular;
            var file = $"{cls}.cs";
            var path = Path.Combine(env.Dto.Dir, file);
            bool fileChanged = true;

            fileChanged = new FileSaver(path, sourceCode).WithBlocks(DtoRenderer.Blocks).SaveIfChanged();
            bool projChanged = new CsProject(env.Dto.Project).Add_Compile(file).Save();
            var map = new TextFileMap(env);
            map.Set(cls, env.Dto.Namespace);
            var mapChanged = map.Save();

            return $@"Dto generation completed:
                    * File Generated: {fileChanged}
                    * Project updated: {projChanged}
                    * Map updated: {mapChanged}";
        }

        public static string RunRepo(string sourceCode, PopulatedBundle bundle, CodeEnvironment env)
        {
            var sb = new StringBuilder();
            sb.AppendLine(RunProjFile(bundle, env));
            if (env.Repo.Creator.IsNullOrEmpty()) return sb.ToString();

            sb.Append(UpdateCreator(env));
            return sb.ToString();
        }

        private static string RunProjFile(PopulatedBundle bundle, CodeEnvironment env)
        {
            var proj = env.Repo;
            var repoName = bundle.Table.Plural;
            var filePath = Path.Combine(proj.Dir, $"{repoName}Repo.cs");
            var fileUpdated = new FileSaver(filePath, GetContent(bundle, SQLInjector.ContentType.Repo, env))
                .WithBlocks(RepoRenderer.Blocks)
                .SaveIfChanged() ? "Added/Updated" : "Unchanged";
            var projUpdated = new CsProject(proj.Project).Add_Compile(filePath).Save() ? "Added" : "Unchanged";
            return $@"{repoName}:
* Content {fileUpdated}
* Project entry {projUpdated}";
        }

        private static string UpdateCreator(CodeEnvironment env, bool includeContent = false)
        {
            if (env.Repo.Creator.IsNullOrEmpty()) return "";
            var def = env.GetDbDef();

            var cs = def.Bundles.Select(b => b.Table.Plural).ToList();
            var nonEnums = cs
                .Where(c => def.Enumerations.Any(e => e.Name.EqualsOic(c)))
                .ToList();

            var hcbs = new (string Keyword, string Text)[]
                {
                    ("ScriptOMaticConstructors", Run(cs, CreatorConstructor)),
                    ("ScriptOMaticProperties", Run(cs, CreatorProp, "\r\n")),
                    ("ScriptOMaticTypeLookup", Run(nonEnums, CreatorTypeLookup))
                }
                .Select(t => HandCodedBlock.ShortComment(t.Keyword, t.Text)).ToList();

            var content = File.ReadAllText(env.Repo.Creator);
            content = hcbs.Aggregate(content, (txt, b) => b.ApplyTo(txt));
            var changed = new FileSaver(env.Repo.Creator, content).SaveIfChanged();
            if (!includeContent)
                return "Creator: " + (changed ? "Updated" : "Unchanged") + "\r\n";
            var sb = new StringBuilder();
            sb
                .AppendLine(content)
                .AppendLine("#################################")
                .AppendLine(changed ? "Updated" : "Unchanged");
            return sb.ToString();
        }
        private static string Run(IEnumerable<string> classes, Func<string, string> f, string separator = ",\r\n") => classes.Select(f).Join(separator);
        private static string CreatorConstructor(string c) => $"                [typeof({c}Repo)] = ()=>new {c}Repo(this)";
        private static string CreatorProp(string c) => $"        public {c}Repo {c} => Get<{c}Repo>();";
        private static string CreatorTypeLookup(string c) => $"                [typeof({StringUtil.ToSingle(c)})] = {c}";

        public static string GetContent(PopulatedBundle b, SQLInjector.ContentType ct, CodeEnvironment env)
        {
            var sb = new IndentingStringBuilder("  ");
            switch (ct)
            {
                case SQLInjector.ContentType.Bundle:
                    return JsonConvert.SerializeObject(new Bundle(b), Formatting.Indented);
                case SQLInjector.ContentType.StoredProcedures:
                    sb.AppendObjects(null == b.LinkTable ? Sql.CrudSPs(b) : Sql.LinkTable(b.LinkTable), "\r\nGO\r\n");
                    break;
                case SQLInjector.ContentType.DTO:
                    new DtoRenderer(b)
                        .Append(b.Columns)
                        .RenderTo(sb.SetIndentChars("\t"));
                    break;
                case SQLInjector.ContentType.Repo:
                    new RepoRenderer(sb.SetIndentChars("\t"), env)
                        .Append(b, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sb.ToString();
        }

    }
}
