using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using SupplyChain.Procs;
using UtilClasses;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Files;

namespace ScriptOMatic.Generate.Extensions
{
    public static class CodeEnvironmentExtensions
    {
        public static Dictionary<string, string> GetEnumFields(this CodeEnvironment env) =>
            env.Db.LoadDbDef()
                .Enumerations
                .BuildDictionary(e => e.Name, StringComparer.OrdinalIgnoreCase)
                .WithKeyFunc(e => StringUtil.ToSingle(e.Table) + "Id")
                .WithKeyFunc(e => e.Name + "Id")
                .WithKeyFunc(e => e.Name)
                .WithKeyFunc(e => e.Table)
                .WithKeyFunc(e => e.Table + "Id")
                .Build();

        public static (bool FileChanged, bool ProjectChanged, bool MapChanged) UpdateDtoClass(this CodeEnvironment env, string name, string content)
        {
            var file = $"{name}.cs";
            var path = Path.Combine(env.Dto.Dir, file);
            return (
                new FileSaver(path, content).SaveIfChanged(),
                new CsProject(env.Dto.Project).Add_Compile(file).Save(),
                new TextFileMap(env).Set(name, env.Dto.Namespace).Save());
        }
        public static bool UpdateDbDefinitions(this CodeEnvironment env, IEnumerable<Bundle> bs)
        {
            var dbDefs = File.Exists(env.Db.DbDefinition)
                ? env.Db.LoadDbDef()
                : new DbDef();
            var bundles = (dbDefs.Bundles.IsNotNullOrEmpty())
                ? dbDefs.Bundles.ToDictionary(b => b.Table.Name, StringComparer.OrdinalIgnoreCase)
                : new Dictionary<string, Bundle>(StringComparer.OrdinalIgnoreCase);
            foreach (var b in bs)
            {
                bundles[b.Table.Name] = b;
            }

            dbDefs.Bundles = bundles.Values.ToList();
            return env.Db.SaveDbDef(dbDefs);
        }

        public static bool UpdateRepoFile(this CodeEnvironment env, string name, string content)
        {
            var filePath = Path.Combine(env.Repo.Dir, $"{name}.cs");
            var fileUpdated = new FileSaver(filePath, content)
                .WithBlocks(RepoRenderer.Blocks)
                .SaveIfChanged() ? "Added/Updated" : "Unchanged";
            return new CsProject(env.Repo.Project).Add_Compile(filePath).Save();
        }
    }
}
