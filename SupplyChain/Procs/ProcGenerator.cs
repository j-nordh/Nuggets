using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ScriptOMatic.Generate.Extensions;
using ScriptOMatic.Generate.SQL;
using SupplyChain.Dto;
using SupplyChain.Dto.Extensions;
using SupplyChain.Properties;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace SupplyChain.Procs
{
    public class ProcGenerator:IAppendable
    {
        public ProcDef Def { get; }

        public List<ParameterDef> Parameters { get; }
        public List<CallGenerator> Calls;
        public ProcGenerator( ProcDef def): this()
        {
            
            Def = def;
            Parameters = new List<ParameterDef>();
            Calls = Def.Calls.Select(c => new CallGenerator(c)).ToList();

            if (!def.Name.ContainsOic("upsert")) return;
            

            //Calls.Add(new CallGenerator(new CallDef(){Parameters = def.}));
            
            //    jsonUpsert = new CallDef(jsonUpsert);
            //    jsonUpsert.ReturnsList(jsonUpsert.Typename, false);
            //    jsonUpsert.Parameters.Clear();
            //    jsonUpsert.Parameters.Add($"IEnumerable<{jsonUpsert.Typename}> json");
            //    Calls.Add(new CallGenerator(jsonUpsert));
        }

        private ProcGenerator()
        {
            Calls= new List<CallGenerator>();
        }

        public static IEnumerable<ProcGenerator> FromLinks(IEnumerable<LinkTable> lts)
        {
            
            var ret = new ProcGenerator();
            foreach (var lt in lts)
            {
                yield return GetGenerator(lt, lt.A);
                yield return GetGenerator(lt, lt.B);
            }
        }

        private static ProcGenerator GetGenerator(LinkTable lt, Link parent) =>
            new ProcGenerator(new ProcDef($"spLink{lt.Name(parent)}", lt.Name(parent))
            {
                Calls = new List<CallDef>()
                {
                    new CallDef{Name = lt.Name(parent),Returns = "void"}
                }
            });

        public string Prop=> $"public static {Def.Name}Proc {Def.Name} => new {Def.Name}Proc();";

        public override string ToString() => AppendObject(new IndentingStringBuilder("\t")).ToString();
        
        public void Init(SqlConnection conn, string basePath)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = string.Format(Resources.ProcExistsQuery, Def.SpName);
                var name = cmd.ExecuteScalar()?.ToString();
                if (null == name)
                {
                    throw new Exception($"The specified procedure [{Def.SpName}] does not exist!");
                }
            }
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = string.Format(Resources.ParameterQuery, Def.SpName);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var typeName = rdr["Type"].ToString();
                    if (!ClassGenerator.Translator.ContainsKey(typeName))
                    {
                        Console.WriteLine("Could not determine DbType for " + typeName);
                        continue;
                    }
                    var t = ClassGenerator.Translator[typeName];

                    Parameters.Add(new ParameterDef(rdr["Parameter_name"].ToString(), t,
                        int.Parse(rdr["Length"].ToString()), rdr["IsOutput"].ToString().AsBoolean()));
                }
                rdr.Close();
            }
            Calls.ForEach(c => c.Init(this, basePath));
        }

        public IndentingStringBuilder AppendObject(IndentingStringBuilder sb)
        {
            return sb
                .AppendLine($"public class {Def.Name} : IStoredProcedure")
                .AppendLine("{")
                .AppendLine($"public string Name => \"{Def.SpName}\";")
                .AppendLines(Parameters.Select(p => p.ToString()))
                .AppendLine("}");
        }
    }
}
