using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using SharpSvn;
using UtilClasses.Extensions.Strings;
using UtilClasses.WebClient;
using UtilClasses.Extensions.Exceptions;
using UtilClasses.Extensions.Integers;
using UtilClasses.Cli;

namespace CaptainHook
{
    class Program
    {
        class HookConfig
        {
            public HookConfig()
            {
                Allowed = new List<string>();
                Denied = new List<string>();
            }
            public List<string> Allowed { get; set; }
            public List<string> Denied { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string HookUri { get; set; }
            public string LogDir { get; set; }
        }

        class Message
        {
            [JsonProperty("revision")]
            public int Revision { get; set; }
            [JsonProperty("author")]
            public string Author { get; set; }
            [JsonProperty("commitLog")]
            public string LogMessage { get; set; }
            [JsonProperty("reposPath")]
            public string RepoPath { get; set; }
            [JsonProperty("commitBrowserUrl")]
            public string BrowseUrl { get; set; }
        }
        string _exeDir;
        private readonly HookConfig _cfg;
        private ConsoleWriter _wr = new ConsoleWriter();
        Program(string exeDir, HookConfig cfg)
        {
            _exeDir = exeDir;
            _cfg = cfg;

        }

        void Run(string[] args)
        {
            if (args.Count() < 2)
            {
                _wr.Error($"Expected at least 2 command line parameters, found {args.Count()}");
                return;
            }
            var path = args[0];
            var rev = args[1].AsInt();

            if (!_cfg.Allowed.Any(a => path.ContainsOic(a)) && !_cfg.Allowed.Any(a => a.Equals("*"))) return;
            if (_cfg.Denied.Any(d => path.Contains(d))) return;

            var client = new SvnClient();
            client.Authentication.UserNamePasswordHandlers += (o, e) =>
            {
                e.UserName = _cfg.Username;
                e.Password = _cfg.Password;
            };
            var msg = new Message()
            {
                BrowseUrl = "n/a",
                RepoPath = path,
                Revision = rev
            };


            client.GetLog(path, new SvnLogArgs(new SvnRevisionRange(rev, rev)), out var logItemCol);
            var lis = logItemCol.ToList();
            if (lis.Count != 0)
            {
                _wr.Error($"Expected 1 LogItem, found{lis.Count}");
                return;
            }

            var li = lis.Single();
            msg.Author = li.Author;
            msg.LogMessage = li.LogMessage;
            var res = new HttpCom(_cfg.HookUri).WithBody(msg).Post().GetAwaiter().GetResult();
            _wr.WriteLine($"Done! Result:\r\n{res}");
        }
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.UnhandledException += (s, e) => Console.WriteLine((e.ExceptionObject as Exception)?.DeepToString());
            var exeDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var cfg = JsonConvert.DeserializeObject<HookConfig>(File.ReadAllText(Path.Combine(exeDir, "Config.json"), Encoding.UTF8));
            var wr = new ConsoleWriter();
            wr.WriteLine("Started with command line: " + Environment.CommandLine);
            try
            {
                var prog = new Program(exeDir, cfg);
                prog.Run(args);
            }
            catch (Exception e)
            {
                wr.Error(e);
            }

            if (Debugger.IsAttached)
            {
                Console.Write("Done, press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
