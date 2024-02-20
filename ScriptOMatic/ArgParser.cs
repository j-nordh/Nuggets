using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic
{
    static class ArgParser
    {
        public static string Server => GetArgument("server");
        public static string Db => GetArgument("db");
        public static string Env => GetArgument("env");
        private static string GetArgument( string key)
        {
            var r = new Regex($@"[//-]{key}=", RegexOptions.IgnoreCase);
            return Environment.GetCommandLineArgs().FirstOrDefault(a => r.IsMatch(a))?.SubstringAfter("=");
        }
    }
}
