using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CLAP;
using Newtonsoft.Json;
using ScriptOMatic.Generate;
using ScriptOMatic.Generate.Extensions;
using SourceFuLib;
using SupplyChain.Constants;
using SupplyChain.Dto;
using SupplyChain.Procs;
using UtilClasses;
using UtilClasses.Cli;
using UtilClasses.CodeGen;
using UtilClasses.Db.Extensions;

namespace SupplyChain
{
    class Program
    {
        private bool _force;
        private CodeEnvironment _env;

        private Program()
        {
            //AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += Routes.Generator.ReflectionAssemblyResolve;
        }




        static void Main(string[] args)
        {
            Console.WriteLine(@"
                                  |
                                  |
                                  |
                          |       |
                          |      ---
                         ---     '-'
                         '-'  ____|_____
                      ____|__/    |    /
                     /    | /     |   /
                    /     |(      |  (
                   (      | \     |   \
                    \     |  \____|____\   /|
                    /\____|___`---.----` .' |
                .-'/      |  \    |__.--'    \
              .'/ (       |   \   |.          \
           _ /_/   \      |    \  | `.         \
            `-.'    \.--._|.---`  |   `-._______\
               ``-.-------'-------'------------/
                   `'._______________________.'   
        SupplyChain - the sane way of generating code.
");

            var p = new Program();
            Parser.Run(args, p);

            if (Debugger.IsAttached)   
            {
                Console.Write(@"Done. Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Done!");
            }

        }


        //[Verb(Aliases = "Routes")]
        //public void GenerateRecsRoutes() => new Routes.Generator(_env, _force).GenerateRoutes();
        
        public void GenerateProcedures(string dbName = null)
        {
            _env.Db.Name = dbName ?? _env.Db.Name;
            var gen = new Procs.Generator(_env, _force);
            gen.Run();
        }

        [Verb(Aliases = "With")]
        public void WithExtensions([Aliases("t")]string typeName, [Aliases("file")]bool generateFile =false)
        {
            var tl = new TypeLoader(Globals.Map);
            var t = tl.Get(typeName);
            var gen = new WithExtensionGenerator(_env, t);
            Console.WriteLine(gen.Generate());
            if (!generateFile) return;
            var cw = new ConsoleWriter();
            gen.GenerateFile();
            cw.WithColor(ConsoleColor.Yellow, $"Generated {gen.FileName}");
        }
        //[Verb]
        //public void Clean()
        //{
        //    var gen = new Procs.Generator(_basePath, _force);
        //    gen.Clean();
        //}
        //[Verb]
        //public void Chain()
        //{
        //    IndentingStringBuilder sb;
        //    var chain = JsonConvert.DeserializeObject<Chain>(File.ReadAllText(_env.ChainFile));


        //    var parts = new List<string>();                                                      //////////////////
        //    foreach (var link in chain.Links)                                                    //
        //    {                                                                                    //
        //        parts.AddRange(Sql.CrudSPs(link.Procedures).Select(a => a.Render()));            // Stored procedures
        //    }                                                                                    //
        //    using (var conn = _env.OpenConnection())                                             //
        //        conn.ExecuteNonQuery(parts);                                                     //////////////////

        //    foreach (var link in chain.Links)                                                    //////////////////
        //    {                                                                                    // 
        //        sb = new IndentingStringBuilder("\t");                                           // Data
        //        new DtoRenderer(sb, link.Dto, link.Aggregates, _env.GetEnumFields())             // Transfer
        //            .Append(link.Procedures.Columns);                                            // Objects
        //                                                                                         //
        //        _env.UpdateDtoClass(link.Singular, sb.ToString());                               //
        //    }                                                                                    //////////////////

        //    _env.UpdateDbDefinitions(chain.Links.Select(l => l.Procedures.GetDbDefinition()));   // Procedure definitions

        //    GenerateProcedures();                                                                // Generate procedures from information generated above

        //    sb = new IndentingStringBuilder("\t");                                               //////////////////
        //    var r = new RepoRenderer(sb, _env);                                                  //
        //    foreach (var link in chain.Links)                                                    // Repositories
        //    {                                                                                    //
        //        r.Append(link.Procedures, false);                                                //
        //        _env.UpdateRepoFile(link.Plural + "Repo", r.ToString());                         //
        //        sb.Clear();                                                                      //
        //    }                                                                                    //////////////////
        //}

        [Verb(Aliases = "All")]
        public void GenerateAll(string dbName = null)
        {
            _env.Db.Name = dbName ?? _env.Db.Name;
            //GenerateRecsRoutes();
            //GenerateApiRoutes();
            //GenerateMonitorRoutes();
            GenerateProcedures();
            Constants();
            //LangSid();
        }

        [Verb]
        public void Constants(string dbName = null)
        {
            _env.Db.Name = dbName ?? _env.Db.Name;
            Console.WriteLine("Updating constants");
            var updater = new ConstantsUpdater(_env);
            updater.Update(_force);
            Console.WriteLine("Constant generation completed.");
        }

        [Global]
        public void Recs() => LoadEnvironment("Recs");
        [Global]
        public void Eco() => LoadEnvironment("Eco");
        [Global]
        public void Macs() => LoadEnvironment("Macs");
        private void LoadEnvironment(string name)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScriptOMatic", "Settings.json");
            var settings = JsonConvert.DeserializeObject<SourceFuSettings>(File.ReadAllText(path, Encoding.UTF8));
            _env = settings.GetEnvironment(name);
            Globals.Init(_env);
        }

        [Global]
        public void Force() => _force = true;
    }
}
