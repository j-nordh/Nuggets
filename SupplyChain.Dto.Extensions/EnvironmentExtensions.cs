using UtilClasses.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;
using UtilClasses.Interfaces;

namespace SupplyChain.Dto.Extensions
{
    public static class EnvironmentExtensions
    {
        private static readonly Dictionary<string, DbDef> DefCache =
            new Dictionary<string, DbDef>(StringComparer.OrdinalIgnoreCase);

        public static DbDef GetDbDef(this CodeEnvironment env) => DefCache.GetOrAdd(env.Name, () => env.Db.LoadDbDef());

        public static DbDef LoadDbDef(this DbSettings s)
        {
            if (null == s?.DbDefinition) throw new ArgumentNullException();
            if (!File.Exists(s.DbDefinition))
                throw new FileNotFoundException("Could not find the DbDefinitions file", s.DbDefinition);

            return JsonConvert.DeserializeObject<DbDef>(File.ReadAllText(s.DbDefinition));
        }

        public static bool SaveDbDef(this DbSettings s, DbDef def)
        {
            Ensure.Argument.NotNull(s?.DbDefinition, "DbSettings.DbDefinition ");
            if (null == s?.DbDefinition) throw new ArgumentNullException();

            return new FileSaver(s.DbDefinition, OutputFormatting.Compactify(JsonConvert.SerializeObject(def, Formatting.Indented))).SaveIfChanged();
        }
        public class VerbDescriptor : IHasName
        {
            public string Name { get; set; }
        }
        public static Dictionary<string, List<VerbDescriptor>> GetVerbDescriptors(this RoutesSettings rs) =>
            JsonConvert.DeserializeObject<Dictionary<string, List<VerbDescriptor>>>(File.ReadAllText(rs.VerbDescriptorPath, Encoding.UTF8));
    }

    public static class OutputFormatting
    {
        public static string Compactify(string json)
        {
            int cursor = 0;
            while (true)
            {
                cursor = json.IndexOf("\"Calls\":", cursor) + 8;
                if (cursor < 8) break;
                var (start, len) = json.NextGroup(cursor);
                var group = json.Substring(start, len).Replace("\r", "").Replace("\n", " ").Replace("\t", " ");
                while (group.Contains("  ")) group = group.Replace("  ", " ");
                group = group.Replace("}, {", "},\r\n                    {").Replace("[ {", "[{").Replace("] }", "]}").Replace("} ]", "}]");
                json = json.Substring(0, start) + group + json.Substring(start + len);
            }

            json = Regex.Replace(json, @"(""[AB]"":) {\s*([^,]*,)\s*([^\n\r]*)\s*}", "$1 {$2\t$3}");

            return json.Replace("},\r\n        {", "},{");

        }
    }
}
