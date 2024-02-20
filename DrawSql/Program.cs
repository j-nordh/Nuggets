using CLAP;
using Newtonsoft.Json;
using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ScriptOMatic.Generate.Extensions;
using SourceFuLib;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.Db.Extensions;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;


namespace DrawSql
{
    public class Program
    {
        private bool _yesToAll = false;
        private bool _clearTables = false;
        private bool _force;
        private bool _includeSps;
        private string _outputPath;

        public static void Main(string[] args)
        {
            var p = new Parser<Program>();
            var prog = new Program();
            p.Run(args, prog);
            if (System.Diagnostics.Debugger.IsAttached && !prog._yesToAll)
            {
                Console.Write("--Execution finished, press enter to terminate...");
                Console.ReadLine();
            }

            if (prog._yesToAll)
            {
                Console.WriteLine("--Done!");
            }
        }

        [Verb(IsDefault = true)]
        public void Generate(string envName)
        {
            var env = GetEnv(envName);
            if (null == env)
                throw new ArgumentException("No environment specified.");
            var path = GetRealPath(env.Db.IncomingDiagram);
            var sb = new IndentingStringBuilder("  ");
            sb.AppendLines($"--Environment:  {env.Name}",
                $"--Server:      {env.Db.Server}",
                $"--Database:    {env.Db.Name}",
                $"--Restore dir: {env.Db.RestoreDir}",
                $"--File:        {Path.GetFileName(path)}",
                $"--FileDate:    {File.GetCreationTime(path)}");

            var parser = new DrawingParser(path, env.Db.Diagram);
            parser.Parse();

            var sql = GetSql(parser, env, sb);
            Console.WriteLine(sb.ToString());

            if (_outputPath.IsNotNullOrEmpty())
            {
                File.WriteAllText(_outputPath, sql);
                Console.WriteLine($"SQL saved to {_outputPath}.");
                return;
            }

            while (!_yesToAll)
            {
                Console.Write("--Run the generated sql? [y/n]: ");
                var l = Console.ReadLine();
                if (l.EqualsOic("n"))
                {
                    Console.WriteLine(sql);
                    return;
                }

                if (l.EqualsOic("y")) break;
            }



            if (sql.Length > 1)
            {
                using (var conn =
                    new SqlConnection($"Server={env.Db.Server};Database={env.Db.Name};Trusted_Connection=True;"))
                {
                    conn.Open();
                    conn.ExecuteScript(sql.ToString());
                    if (env.Db.RestoreDir.IsNotNullOrEmpty() && Directory.Exists(env.Db.RestoreDir))
                    {
                        var files = Directory.GetFiles(env.Db.RestoreDir)
                            .Where(f => !Path.GetFileName(f).StartsWith("_"));
                        foreach (var f in files)
                        {
                            var content = File.ReadAllText(f);
                            if (content.Contains(":r $(path)"))
                                continue;
                            conn.ExecuteScript(content);
                        }
                            
                    }
                }

            }

            Console.WriteLine("Updated successfully!");
        }


        public string GetSql(DrawingParser parser, CodeEnvironment env, IndentingStringBuilder stats = null)
        {
            stats?.AppendLines(
                $"Tables:      {parser.Tables.Count}",
                $"Columns:     {parser.Tables.Values.Sum(t => t.Columns.Count)}",
                $"ForeignKeys: {parser.Tables.Values.Sum(t => parser.GetForeignKeys(t).Count)}");
            var toPlace = new List<Table>(parser.Tables.Values);
            var drops = new List<string>();
            var placed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var tc = new TableComparer(env, parser.Tables.Values, _force || _clearTables);
            stats?.AppendLine("Change detector result:")
                .Indent()
                .AppendLines(
                    $"New:       {tc.New}",
                    $"Dropped:   {tc.Dropped}",
                    $"Changed:   {tc.Changed}",
                    $"Unchanged: {tc.Unchanged}")
                .Outdent();
            drops.AddRange(tc.DropNames);
            toPlace = tc.Create.ToList();
            placed.AddRange(tc.UnchangedNames);


            var createSb = new IndentingStringBuilder("\t");

            var f = new Formatter(createSb);

            var tierCount = 1;
            while (toPlace.Count > 0)
            {
                createSb.AppendLine($"--Tier {tierCount}");
                foreach (var t in toPlace)
                {
                    var fks = parser.GetForeignKeys(t);
                    var deps = fks
                        .Select(fk => fk.ReferencedTable)
                        .NotNull()
                        .Except(placed)
                        .Except(new[] { t.Name });

                    if (deps.Any()) continue;

                    placed.Add(t.Name);
                    f.Append(t, fks);
                    drops.Add(t.Name);
                }

                tierCount += 1;
                if (toPlace.RemoveAll(t => placed.Contains(t.Name)) == 0)
                {
                    createSb.AppendLine("ERROR!!! Empty tier detected with the following tables remaining:")
                        .AppendLines(toPlace.Select(t => t.Name));
                    break;
                }
            }

            //createSb.AppendLines(tc.KeysToRecreate.Select(f.Add));

            var unknownCols = parser.Tables.Values.SelectMany(t => t.Columns.Values
                    .Where(c => c.DataType.Equals(Column.UNKNOWN_DATATYPE))
                    .Select(c => $"{t.Name}.{c.Name}"))
                .ToList();
            if (unknownCols.Any())
            {
                throw new Exception(new IndentingStringBuilder("")
                    .AppendLine("ERROR!!! The data type could be deduced for the following columns:")
                    .AppendLines(unknownCols).ToString());
            }


            drops.Reverse();
            var res = new IndentingStringBuilder("\t");

            res
                .AppendObjects(tc.KeysToDrop, f.Drop)
                .AppendObjects(drops, d => $"DROP TABLE IF EXISTS {d};")
                .MaybeAppendLine(res.Length > 1, "GO")
                .MaybeAppendLine(createSb.Length > 1, createSb.ToString());

            if (env.Db.TableSqlFile.IsNotNullOrEmpty())
                FileSaver.SaveIfChanged(env.Db.TableSqlFile, res.ToString());

            if (env.Db.ProcedureSqlFile.IsNullOrEmpty() && env.Db.CombinedSqlFile.IsNullOrEmpty() && !_includeSps) return res.ToString();
            
            var sps = new IndentingStringBuilder("\t");
            using (var conn = GetConn(env))
            {
                var procs = conn.QueryDirect<StoredProcedure>(Properties.Resources.ProcedureQuery).ToDictionary(sp => sp.Name);
                var dbDef =env.GetDbDef();
                //C:\Source\EnergyCostOptimizer\DbDefinitions.json
                sps.AppendLine(dbDef.Bundles.SelectMany(b=>b.ProcedureNames())
                    .Distinct()
                    .Select(x =>
                    {
                        if (procs.ContainsKey(x)) return procs[x].ToString();
                        Console.WriteLine($"WARNING: {x} is specified in dbDefs but does not exist in the database");
                        return null;
                    })
                    .NotNull()
                    .Join("\r\nGO\r\n"));
            }

            if (env.Db.ProcedureSqlFile.IsNotNullOrEmpty())
            {
                FileSaver.SaveIfChanged(env.Db.ProcedureSqlFile, sps.ToString());
            }

            var combined = new IndentingStringBuilder("\t")
                .AppendLine(res.ToString())
                .AppendLine("\r\nGO\r\n")
                .AppendLine(sps.ToString())
                .ToString();
            if (env.Db.CombinedSqlFile.IsNotNullOrEmpty())
            {
                FileSaver.SaveIfChanged(env.Db.CombinedSqlFile, combined);
            }

            return _includeSps ? combined : res.ToString();
        }

        private SqlConnection GetConn(CodeEnvironment env) =>
            new SqlConnection($"Server={env.Db.Server};Database={env.Db.Name};Trusted_Connection=True;");

        void AppendClearer(IndentingStringBuilder sb, SqlConnection conn)
        {
            var tables = conn.QueryDirect<string>("select TABLE_NAME from INFORMATION_SCHEMA.TABLES");
            var refs = conn.QueryDirect<(string FkTable, string PkTable)>(
                @"SELECT cu.TABLE_NAME AS ReferencedTable, ku.TABLE_NAME AS PkTable
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME");
            var orderedTables = new List<string>();
            while (tables.Any())
            {
                foreach (var t in tables)
                {
                    if (refs.Any(r => r.PkTable.EqualsOic(t))) continue;
                    orderedTables.Add(t);
                    refs = refs.Where(r => !r.FkTable.EqualsOic(t)).ToList();
                }

                tables = tables.Where(t => !orderedTables.ContainsOic(t)).ToList();
            }

            sb.AppendObjects(orderedTables, t => $"DROP TABLE IF EXISTS {t};");
        }

        private CodeEnvironment GetEnv(string name)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ScriptOMatic", "Settings.json");
            var settings = JsonConvert.DeserializeObject<SourceFuSettings>(File.ReadAllText(path, Encoding.UTF8));

            var env = settings.GetEnvironment(name);
            if (null == env)
                throw new ArgumentException("No environment specified.");
            return env;
        }

        [Verb]
        public void Count(string path = null, string envname = null)
        {
            if (path.IsNullOrEmpty() && envname.IsNotNullOrEmpty()) path = GetEnv(envname).Db.IncomingDiagram;
            var _counters = new Dictionary<string, Dictionary<string, int>>(StringComparer.OrdinalIgnoreCase);
            path = GetRealPath(path);
            var p = new DrawingParser(path, null);
            p.Parse();

            p.Tables.Values
                .SelectMany(t => t.Columns.Values)
                .ForEach(c =>
                    _counters.GetOrAdd(c.DataType, () => new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase))
                        .Increment(c.Name));


            var unknowns = _counters.Maybe(Column.UNKNOWN_DATATYPE);
            Console.WriteLine(
                new IndentingStringBuilder("\t").AppendLines(
                        $"File:        {Path.GetFileName(path)}",
                        $"File date:   {File.GetCreationTime(path)}",
                        $"Tables:      {p.Tables.Count}",
                        $"Columns:     {p.Tables.Values.Sum(t => t.Columns.Count)}",
                        $"ForeignKeys: {p.Tables.Values.Sum(t => p.GetForeignKeys(t).Count)}")
                    .Maybe(!unknowns.IsNullOrEmpty(), sb => sb.AppendLine("Columns with unknown datatype:")
                        .Indent(sb2 => sb2.AppendObjects(unknowns, kvp => $"{kvp.Key}: {kvp.Value}"))));
        }

        private string GetRealPath(string path)
        {
            var dir = Path.GetDirectoryName(path);
            var file = Path.GetFileNameWithoutExtension(path);
            return Directory.EnumerateFiles(dir, $"*{Path.GetExtension(path)}")
                .Where(f => Path.GetFileName(f).StartsWithOic(file))
                .Select(f => new FileInfo(f))
                .OrderBy(f => f.CreationTime)
                .Last()
                .FullName;
        }

        [Global(Aliases = "Y", Description = "Answer yes to any questions")]
        public void Yes()
        {
            _yesToAll = true;
        }

        [Global(Aliases = "Clear", Description = "Drops ALL tables in the database")]
        public void ClearTables() => _clearTables = true;

        [Global(Description = "Forces (re)creation of all tables")]
        public void Force() => _force = true;

        [Global(Aliases = "Procs,Procedures,AllProcedures,IncludeProcedures", Description = "Includes all Stored Procedures stored in the CodeEnvironment DbDefinition")]
        public void IncludeSps() => _includeSps = true;
        [Global(Aliases = "Out,Output", Description = "Saves generated SQL to the specified path.")]
        public void OutputPath(string val) => _outputPath = val;
        //[Verb]
        //public void TestBlob()
        //{
        //    TestBlobAsync().Wait();
        //}

        //public async Task TestBlobAsync()
        //{
        //    var msw = new MultiStopWatch(true);
        //    var bs = new GuidStorageRoot(@"c:\Temp\BlobStorage\");
        //    GuidStorageNode.MaxFilesPerDir = 40;
        //    var ids = new List<Guid>();
        //    msw.DoneWith("Init");
        //    for (int i=0;i<10000;i++)
        //    {
        //        var name = $"{i}.txt";
        //        ids.Add(await bs.Create(Encoding.UTF8.GetBytes($"{i}"), name));
        //    }
        //    msw.DoneWith("Create");
        //    foreach (var id in ids) Console.WriteLine(Encoding.UTF8.GetString(bs.Read(id)));
        //    msw.DoneWith("Read");
        //    await Task.WhenAll(ids.Select((id, i) => bs.Update(id, Encoding.UTF8.GetBytes($"{i + 10000}"))));
        //    msw.DoneWith("Update");
        //    foreach (var id in ids) Console.WriteLine(Encoding.UTF8.GetString(bs.Read(id)));
        //    msw.DoneWith("Read");
        //    await Task.WhenAll(ids.Select(bs.Delete));
        //    msw.DoneWith("Delete");
        //    Console.WriteLine(msw);
        //}
    }
}