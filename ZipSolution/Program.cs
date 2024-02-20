using CLAP;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UtilClasses;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Exceptions;
using System.Diagnostics;

namespace ZipSolution
{
    class Program
    {
        string _outPath;
        private string _outFilePath;
        private bool _pause;
        private List<string> _solutions=new();
        static void Main(string[] args)
        {
            bool threw=false;
            var p = new Program();
            try
            {
                args.ForEach(Console.WriteLine);
                var parser = new Parser<Program>();
                parser.Run(args, p);
                
                Console.WriteLine(new IndentingStringBuilder("  ").AppendLine("Zipped:").Indent().AppendLines(p._solutions).Outdent().AppendLine($"To: {p._outFilePath}"));
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.DeepToString());
                threw = true;
            }
            if (Debugger.IsAttached || threw|| p._pause)
            {
                Console.Write("Done. Press enter to terminate!");
                Console.ReadLine();
            }
        }

        [Verb(IsDefault = true, Aliases ="zip,zipsolution,solution")]
        private void ZipSolution([Aliases("path")][Description("The path to a single solution file")]string sln)
        {
            _solutions.Add(sln);
            new SolutionZip()
                .AddSolution(sln)
                .WriteToFile(GetOutPath(Path.GetFileNameWithoutExtension(sln)));
        }

        private string GetOutPath(string filename, bool withDateTime = true)
        {
            var ret = Path.Combine(_outPath ?? Environment.CurrentDirectory, filename);
            if (withDateTime)
                ret += $"_{DateTime.Now:yyyyMMdd_HHmm}";
            ret += ".zip";
            _outFilePath = ret;
            return ret;
        }

        [Verb]
        public void ZipAll([Aliases("path")][Description("A directory containing .sln files")]string dir)
        {
            _solutions.AddRange(Directory.GetFiles(dir, "*.sln"));
            new SolutionZip()
                .AddSolutions(_solutions)
                .WriteToFile(GetOutPath("Solutions"));
        }

        class SolutionZip
        {
            List<string> _solutions;
            List<string> _projectDirs;
            string _baseDir;

            public SolutionZip()
            {
                _solutions = new List<string>();
                _projectDirs = new List<string>();
            }

            public SolutionZip AddSolutions(IEnumerable<string> slns)
            {
                foreach (var sln in slns) AddSolution(sln);
                return this;
            }
            public SolutionZip AddSolution(string sln)
            {
                if (!sln.EndsWithOic("sln")) throw new ArgumentException($"The file [{sln}] does not seem to be a solution.");
                if (!File.Exists(sln)) throw new ArgumentException($"The file [{sln}] does not exist.");
                var dir = Path.GetDirectoryName(sln);
                var projects = GetProjectDirs(sln);
                var newBase = Path.Combine(Path.GetDirectoryName(sln), Enumerable.Repeat("..\\", projects.Select(p => p.CountOic("..\\")).Max()).Join(""));
                newBase = new Uri(newBase).LocalPath;
                projects = projects.Select(p => new Uri(Path.Combine(dir, p)).LocalPath).ToList();
                _projectDirs = _projectDirs.Union(projects).Distinct().ToList();
                _solutions.Add(sln);

                if (_baseDir != null && !_baseDir.EqualsOic(newBase)) _baseDir = _baseDir.Length < newBase.Length ? _baseDir : newBase;
                if (null == _baseDir) _baseDir = newBase;
                return this;
            }

            public void WriteToFile(string destinationFile)
            {
                using (var stream = new MemoryStream())
                {
                    using (var z = new ZipArchive(stream, ZipArchiveMode.Create))
                    {
                        var bz = new BasedZip(z, _baseDir);
                        _solutions.ForEach(bz.AddFile);
                        _projectDirs.ForEach(bz.AddDir);
                    }
                    stream.Flush();
                    File.WriteAllBytes(destinationFile, stream.ToArray());
                }
            }
            private List<string> GetProjectDirs(string sln)
            {

                return File.ReadAllLines(sln)
                    .Where(l => l.Trim().StartsWithOic("project"))
                    .Select(l => l.SubstringAfter(",").SubstringBefore(",").Trim(new[] {' ', '"'}))
                    .Where(l=>!l.ContainsOic("ProjectSection"))
                    .Select(p => Path.GetDirectoryName(p))
                    .ToList();
            }
        }
        [Global]
        public void OutPath(string p)
        {
            _outPath = p;
        }
        [Global]
        public void Pause()
        {
            _pause = true;
        }
    }

    class BasedZip
    {
        private ZipArchive _zip;
        private readonly string _baseDir;
        private List<Func<string, bool>> _fileFilters = new List<Func<string, bool>>()
        {
            {s=>!s.EndsWithOic("db.lock")}
        };
        private List<Func<string, bool>> _dirFilters = new List<Func<string, bool>>()
        {
            {d=>!d.EndsWithAnyOic("\\bin", "\\obj", "sqlite3","packages") }
        };


        public BasedZip(ZipArchive zip, string baseDir)
        {
            _zip = zip;
            _baseDir = baseDir;
        }
        public void AddFile(string file)
            => _zip.CreateEntryFromFile(file, PathUtil.GetRelativePath(file, _baseDir));
        public void AddDir(string dir)
        {
            Directory.GetDirectories(dir)
                .Where(_dirFilters.All)
                .ForEach(AddDir);
            Directory.GetFiles(dir)
                .Where(_fileFilters.All)
                .ForEach(f => AddFile(f));
        }
    }
}
