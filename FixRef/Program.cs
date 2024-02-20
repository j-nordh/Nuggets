using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace FixRef
{
    class Program
    {
        private readonly string _path;
        private List<string> _ignore;
        private Dictionary<string, ProjInfo> _nameLookup;
        private Dictionary<Guid, ProjInfo> _idLookup;

        private class ProjInfo
        {
            public string Name { get; }
            public string Path { get; }
            public string Content { get; set; }
            public Guid Id { get; }
            public ProjInfo(string path)
            {
                Path = path;
                Name = System.IO.Path.GetFileNameWithoutExtension(path);
                Content = File.ReadAllText(Path);
                var regexp = new Regex("<ProjectGuid>([^<]*)");
                var m = regexp.Match(Content);
                Id = Guid.Empty;
                if (!m.Success) return;
                Id = Guid.Parse(m.Groups[1].Value);
            }


        }

        static void Main(string[] args)
        {
            var prog = new Program();
            new CommandLineParser<Program>(prog)
                .With("path", p => p._path, true)
                .With("ignore", p => p._ignore)
                .WithVerb("fix", prog.Run, true)
                .Parse(args);
            if (Debugger.IsAttached)
            {
                Console.Write("Execution finished, press enter to exit...");
                Console.ReadLine();
            }
        }

        private Program()
        {
            _ignore = new List<string>();
        }

        private void Run()
        {
            _ignore = _ignore.Select(s => Path.IsPathRooted(s) ? s : Path.Combine(_path, s)).ToList();
            var files = Directory
                .EnumerateFiles(_path, "*.csproj", SearchOption.AllDirectories)
                .Where(f => !f.StartsWithAnyOic(_ignore))
                .Select(p => new ProjInfo(p))
                .ToList();
            _nameLookup = files.ToDictionary(p => p.Name);
            _idLookup = new Dictionary<Guid, ProjInfo>();
            foreach (var pi in files)
            {
                if (pi.Id == Guid.Empty) continue;
                if (_idLookup.ContainsKey(pi.Id))
                {
                    Console.WriteLine($"Duplicate guid found for ({pi.Id}):\r\nExisting: {_idLookup[pi.Id].Path}\r\nNew: {pi.Path}");
                    continue;
                }

                _idLookup[pi.Id] = pi;
            }
            _idLookup = files.Where(p => !p.Id.Equals(Guid.Empty)).ToDictionary(p => p.Id);

            foreach (var p in _nameLookup.Keys)
            {
                var path = _nameLookup[p].Path;
                var dir = Path.GetDirectoryName(path);
                var replacements = new List<(string Old, string New)?>();
                Console.Write($"{p}:");
                var regexp = _nameLookup[p].Content.ContainsOic("Sdk=\"Microsoft.NET.Sdk\"") 
                    ? new Regex(@"<ProjectReference\s*Include=""([^""]*)""") 
                    : new Regex(@"<ProjectReference\s*Include=""([^""]*)"">\s*<Project>({[^}]*})");

                foreach (var reference in regexp.Matches(_nameLookup[p].Content).OfType<Match>())
                {
                    var rel = reference.Groups[1].Value;
                    replacements.Add(CheckAndFixPath(rel, dir));
                    var refPath = PathUtil.GetAbsolutePath(rel, dir);
                    var name = Path.GetFileNameWithoutExtension(refPath);
                    if (reference.Groups.Count > 2)
                        replacements.Add(CheckAndFixGuid(name, reference.Groups[2].Value));
                }


                if (replacements.Count == 0)
                {
                    Console.WriteLine(" OK");
                    continue;
                }
                Console.WriteLine();
                Console.Write($"  Fixing {replacements.Count} items");
                string content = _nameLookup[p].Content;
                foreach (var r in replacements.NotNull())
                {
                    content = content.ReplaceOic(r.Old, r.New);
                }
                File.WriteAllText(_nameLookup[p].Path, content);
                Console.WriteLine("OK");
                var cachePath = path + "AssemblyReference.cache";
                if (File.Exists(cachePath))
                {
                    File.Delete(cachePath);
                    Console.WriteLine("  Deleted Assembly reference cache");
                }
            }
        }

        private (string Old, string New)? CheckAndFixGuid(string name, string id)
        {
            if (id.Trim().EqualsOic("{00000000-0000-0000-0000-000000000000}"))
            {
                Console.WriteLine($"  Reference to {name} has a zero-guid reference set.");
                return ("      <Project>{00000000-0000-0000-0000-000000000000}</Project>", "");
            }
            if (!Guid.TryParse(id, out var guid)) return null;
            _idLookup.TryGetValue(guid, out var pi);

            if (null != pi)
            {
                if (pi.Name.EqualsOic(name)) return null;
                Console.WriteLine($"  The provided guid ({id}) references {pi.Name} but the supplied name was {name}");
                return null;
            }
            Console.WriteLine();
            _nameLookup.TryGetValue(name, out pi);
            if (null == pi)
            {
                Console.Write($"  Could not find the referenced project ({name}) by name.");
                return null;
            }
            Console.WriteLine($"  Updating GUID for reference to {name}");
            return (id, pi.Id.ToString("B"));
        }
        private (string Old, string New)? CheckAndFixPath(string rel, string dir)
        {
            var refPath = PathUtil.GetAbsolutePath(rel, dir);
            bool ok = File.Exists(refPath);
            if (!refPath.StartsWithOic(_path)) ok = false;
            if (Path.IsPathRooted(rel)) ok = false;
            if (ok) return null;
            var name = Path.GetFileNameWithoutExtension(refPath);
            Console.WriteLine();
            Console.Write($"  Reference to {name} is invalid");
            if (!_nameLookup.ContainsKey(name))
            {
                Console.Write("... UNKNOWN!!!!");
                return null;
            }
            Console.Write("... Fixing");
            return (rel, PathUtil.GetRelativePath(_nameLookup[name].Path, dir));
        }
    }
}
