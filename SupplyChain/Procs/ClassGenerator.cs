using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using ScriptOMatic.Generate;
using ScriptOMatic.Generate.Extensions;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Procs
{
    public class ClassGenerator
    {
        private readonly CodeEnvironment _env;

        public static readonly Dictionary<string, DbType> Translator;
        private static readonly string[] DefaultNamespaces;
        public PopulatedBundle Bundle { get; }
        public List<ProcGenerator> Procedures;
        [JsonIgnore]
        public string Namespace;

        [JsonIgnore] private string _name;

        public List<string> Namespaces;
        static ClassGenerator()
        {
            Translator = new Dictionary<string, DbType>(StringComparer.OrdinalIgnoreCase)
            {
                {"xml", DbType.Xml},
                {"varchar", DbType.String},
                {"datetime", DbType.DateTime},
                {"int", DbType.Int32},
                {"bit", DbType.Boolean},
                {"bigint", DbType.Int64},
                {"smallint", DbType.Int16 },
                {"nvarchar", DbType.String },
                {"uniqueidentifier", DbType.Guid },
                {"varbinary", DbType.Binary },
                {"text", DbType.String },
                {"date", DbType.DateTime },
                {"decimal", DbType.Decimal },
                {"tinyint", DbType.Byte }
            };
            DefaultNamespaces = new[]
                {"System.Data", "UtilClasses.Db", "UtilClasses.Db.Extensions", "System.Collections.Generic"};
        }


        public ClassGenerator(CodeEnvironment env, PopulatedBundle bundle)
        {
            _env = env;
            Bundle = bundle;
            Procedures = new List<ProcGenerator>();
            _name = bundle?.Table.Plural;
        }

        public void Init(SqlConnection conn, string nameSpace, string basePath)
        {
            Namespace = nameSpace;
            Dictionary<string, int> nameCounters = new Dictionary<string, int>();
            var cls = Bundle.GetDbDefinition();
            
            foreach (var proc in cls.Procedures)
            {
                var c = nameCounters.Increment(proc.Name);
                if (c > 1)
                    proc.Name += $"_{c}";


                var gen = new ProcGenerator(proc);
                gen.Init(conn, basePath);
                Procedures.Add(gen);
            }
        }

        public void InitLinks(SqlConnection conn, string nameSpace, string basePath)
        {
            Namespace = nameSpace;
            _name = "Links";
            Procedures = ProcGenerator.FromLinks(_env.GetDbDef().LinkTables).ToList();
            foreach (var proc in Procedures)
            {
                proc.Init(conn, basePath);
            }

        }

        public override string ToString()
        {
            var namespaces = Procedures
                .SelectMany(p => 
                    p.Calls.Select(c => c.Namespace)
                    .Union(p.Calls.Select(c => Globals.Map.GetNamespace(c.Def.Returns)))
                    .Union(p.Calls.SelectMany(c => c.IsJson ? new[] { "Newtonsoft.Json" } : new string[] { })))
                .Where(s => !s.IsNullOrWhitespace())
                .Union(Namespaces ?? new List<string>())
                .Union(DefaultNamespaces)
                .Distinct()
                .AsSorted();
            return new IndentingStringBuilder("\t").AutoIndentOnCurlyBraces()
                .AppendLines(namespaces.Select(s => $"using {s};"))
                .AppendLines(
                    @"//                                                                     ____            ",
                    @"// This file has been generated using SupplyChain.                    /\' .\    _____  ",
                    @"// Please do not modify it directly, instead:                        /: \___\  / .  /\ ",
                    @"//  * Make the appropriate changes in the database,                  \' / . / /____/..\",
                    @"//  * Update the file Servers\Recs\DbDefinitions.json                 \/___/  \'  '\  /",
                    @"//  * Run SupplyChain again.                                                   \'__'\/ ",
                    "",
                    $"namespace {Namespace}",
                    "{",
                    $"public static class {_name}",
                    "{",
                    "public static class Procs",
                    "{")
                .AppendLines(Procedures.Select(p => p.ToString()))
                .AppendLine("}")
                .AppendObjects(Procedures.SelectMany(p => p.Calls))
                //.AppendObject(new AccessorGenerator(Def))
                .AppendLine("}")
                .AppendLine("}")
                .ToString();
        }
    }
}

