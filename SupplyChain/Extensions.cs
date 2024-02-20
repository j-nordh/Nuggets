using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SupplyChain.Dto;
using SupplyChain.Procs;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;
using UtilClasses.Files;

namespace SupplyChain
{
    static class Extensions
    {
        public static void Add(this Dictionary<string, List<string>> dict, string type, params string[] statements)
        {
            var list = new List<string>(statements);
            dict[type] = list;
        }

        public static string CodeName(this Type type, string controllerName=null)
        {
            if (type.Name.StartsWith("Task"))
                if (type.IsGenericType)
                    type = type.GenericTypeArguments.First();
                else
                    return null;
            var ret = type.IsGenericType
                ? $"{type.Name.SubstringBefore("`").MapTypeName()}<{type.GenericTypeArguments.Select(t => t.MapTypeName()).Join(", ")}>"
                : type.DeclaringType==null? type.MapTypeName() : $"{type.DeclaringType.CodeName()}.{type.MapTypeName()}";
            
            if (null == ret) return null;
            ret = Regex.Replace(ret,"Nullable<([^>]*)>", "$1?");
            ret = ret.MapTypeName();
            if (controllerName.IsNullOrEmpty()) return ret;
            if (controllerName.Equals(ret)) return $"{type.Namespace}.{type.Name}";
            var stripped = ret.StripAllGenerics().MapTypeName();
            
            return controllerName.Equals(stripped) ? ret.Replace(stripped, "DTO." + stripped): ret;
        }

        //public static void Add(this List<ClassGenerator> lst, string className, IEnumerable<ProcGenerator> procs)
        //{
        //    lst.Add(new ClassGenerator(className, procs));
        //}
        //public static void Add(this List<ProcGenerator> lst, string spName, string name)
        //{
        //    lst.Add(new ProcGenerator(spName, name));
        //}

        public static void Generate(this IEnumerable<ClassGenerator> targets, CodeEnvironment env, bool force)
        {
            var conn = env.OpenConnection();
            var ns = env.Repo.Def.Namespace;
            foreach (var targetClass in targets)
            {
                Generate(targetClass, targetClass.Bundle.Table.Plural, c=>c.Init(conn, ns, env.BasePath), env, force);
            }
            var linkClass = new ClassGenerator(env, null);
            Generate(linkClass, "Links", c=>c.InitLinks(conn, ns, env.BasePath), env, force);
            conn.Close();
        }

        private static void Generate(ClassGenerator target, string name, Action<ClassGenerator>  init, CodeEnvironment env, bool force)
        {
            
            Console.Write($"{name}");
            Console.Write(new string('.', 40 - name.Length));
            init(target);
            var p = Path.Combine(env.Repo.Def.Dir, name + ".cs");
            var saved = new FileSaver(p, target.ToString()).Forced(force).SaveIfChanged();

            Console.WriteLine(saved ? "Done!" : "Unchanged.");
        }

        public static SqlConnection OpenConnection(this CodeEnvironment env)
        {
            var conn = new SqlConnection($"Server={env.Db.Server};Database={env.Db.Name};Trusted_Connection=True;");
            conn.Open();
            return conn;
        }
    }
}