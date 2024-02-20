using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptOMatic.Generate;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Procs
{
    class Generator
    {
        private readonly bool _force;
        private readonly CodeEnvironment _env;

        public Generator(CodeEnvironment env,  bool force)
        {
            _force = force;
            _env = env;

            //_applyDbPatches = applyDbPatches;
        }
        public void Run()
        {
            //var dbDir = Path.Combine(BasePath, "Server\\GTMSDatabase");
            //if (_useTfs)
            //{
            //    Console.WriteLine("Updating database scripts from TFS");
            //    UpdateFromTfs(dbDir);
            //}
            //if (_applyDbPatches)
            //{
            //    dbDir = Path.Combine(dbDir, "Version");
            //    Console.WriteLine("Applying updates...");
            //    var version = new DirectoryInfo(dbDir)
            //        .EnumerateDirectories()
            //        .Select(d => new Version(d.Name))
            //        .AsSorted()
            //        .Last()
            //        .ToString(3);
            //    var connStr = $"Server=localhost;Database={dbName};Trusted_Connection=True;";
            //    using (var updater = new DbUpgrader(connStr, Path.Combine(dbDir, version)))
            //    {
            //        updater.DirectoryCompleted +=
            //            (dir, count) => Console.WriteLine($"Applied {count} patches from [{dir}].");
            //        updater.Upgrade("GTMS");
            //    }
            //}

            if (_env.Db.Name.IsNotNullOrEmpty())
            {
                Console.WriteLine("Generating RECS database definitions");
                Generate(_env);
            }
            Console.WriteLine("Db definition generation complete!");
        }

        //internal void Clean()
        //{
        //    var path = Path.Combine(BasePath, "Servers", "Recs", "DbDefinitions.json");
        //    var content = File.ReadAllText(path);
        //    var classes = content.GetChunks('{', '}', true);
        //    var callStack = new Stack<(int start, int stop)>();
        //    foreach(var chunk in classes)
        //    {
        //        var procs = content.Substring(start + 1, stop - 1).GetChunks('{','}',true);
        //        foreach(var(procStart, procStop) in procs)
        //        {
        //            var callStart = content.IndexOfOic("\"Calls\"") + 7;
        //            var calls = content.Substring(callStart, procStop).GetChunks('[',']',true);
        //            calls.ForEach(callStack.Push);
        //        }
        //    }
        //    var sb = new StringBuilder();
        //    sb.Append(content);
        //    while(callStack.Any())
        //    {
        //        var (start, stop) = callStack.Pop();
        //        new ChunkReplacer(sb, start, stop)
        //            .Replace("\r", "")
        //            .Replace("\n", "");
        //    }
        //}

       
        //private void UpdateFromTfs(string path)
        //{
        //    var uri = new Uri("http://hstfs2013:8080/tfs/Logistics/");  // Get the version control server
        //    var workspaceInfo = Workstation.Current.GetLocalWorkspaceInfo(path);
        //    if (null == workspaceInfo)
        //    {
        //        Console.WriteLine("WARNING!");
        //        Console.WriteLine("Could not update db definitions from TFS. DO NOT check in changes unless you're sure everything is up to date.");
        //        return;
        //    }
        //    using (var server = new TfsTeamProjectCollection(uri))
        //    {
        //        var workspace = workspaceInfo.GetWorkspace(server);
        //        var res = workspace.Get(new GetRequest(path, RecursionType.Full, VersionSpec.Latest), GetOptions.None);
        //        foreach (var fail in res.GetFailures())
        //        {
        //            Console.WriteLine($"{fail.LocalItem}: {fail.Message}");
        //        }
        //        Console.WriteLine($"Done. {res.NumFiles} files, {res.NumUpdated} updated.");
        //    }
        //}
        private void Generate(CodeEnvironment env)
        {
            //List<string> afNamespaces = null;
            //switch (t)
            //{
            //    case SystemName.Recs:
            //        nameSpace = "Recs.Repo.DbDef";
            //        dir = @"Servers\Recs\Recs.Repo\DbDef";
            //        afPath = @"Servers\Recs\Recs.Repo\CrudAccessorFactory.cs";
            //        afNamespace = "Recs.Repo";
            //        repoProj = @"Servers\Recs\Recs.Repo\Recs.Repo.csproj";
            //        afNamespaces = new List<string>() { nameSpace };
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(t), t, null);
            //}
            var targets = LoadTargets();
            targets.Generate(env, _force);

            
            if(env.Repo.Project.IsNotNullOrEmpty())
            {
                var files = targets.Select(c => Path.Combine(env.Repo.Def.Dir, c.Bundle.Table.Plural + ".cs"));
                new CsProject(Path.Combine(env.Repo.Project)).Add_Compile(files.ToArray()).Save();
            }

            //if(_env.Route?.Accessor?.Factory?.Enabled??false)
            //{
            //    var types = targets.Select(cg =>cg.Bundle .Def).Where(d => d.IsCrudDef()).Select(d => (Globals.FixForbidden(d.CrudType()), d.ClassName)).ToList();
            //    var gen = new AccessorFactoryGenerator(env, _force, afNamespaces, types);
            //    gen.Generate("hej", _env.Route.Accessor.Factory.Path);
            //}
        }
        private List<ClassGenerator> LoadTargets()
        {
            var loader = new DbLoader(_env);
            loader.Init(3);
            var bundles = _env.GetDbDef().Bundles;
            return bundles.Select(b=>new ClassGenerator(_env, loader.Populate(b))).ToList();
        }

    }
}
