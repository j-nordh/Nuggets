using CLAP;
using System.Text;
using System.Text.RegularExpressions;
using UtilClasses.Extensions.Strings;

namespace MultiReplacer
{
    internal class Runner
    {
        private bool _overwrite = false;
        private Replacer _replacer = new ();
        private string? _outPath;

        [Verb]
        public void Run([Aliases("in,input")] string inputFile, [Aliases("replacements,rep")] string replacementFile)
        {
            _replacer.ParseText(File.ReadAllText(replacementFile, Encoding.UTF8));
            _replacer.Run(File.ReadAllText(inputFile, Encoding.UTF8));

            var outPath = _outPath ?? (_overwrite ? inputFile : null);
            Console.WriteLine($"Executed {_replacer.Replacements.SelectMany(l=>l).Count()} statements resulting in {_replacer.Count} replacements.");
            if (null != outPath)
            {
                File.WriteAllText(outPath, _replacer.Result, Encoding.UTF8);
                Console.WriteLine($"Wrote result to {outPath}");
            }
                
            else
                Console.WriteLine(_replacer.Result);
        }

        [Global] void Overwrite() => _overwrite = true;
        [Global] void UseTemp() => _replacer.UseTemp = true;
        [Global] void Regex() => _replacer.UseRegex = true;
        [Global] private void CaseSensitive() => _replacer.CaseSensitive = true;
        [Global]void OutPath(string p) => _outPath = p;
    }

    class Replacer
    {
        public List<List<(string a, string b)>> Replacements = new();
        public string Result { get; private set; }
        public int Count { get; private set; }

        public bool UseTemp { get; set; }
        public bool UseRegex { get; set; }
        public bool CaseSensitive { get; set; }

        public string Run(string input)
        {
            Count = 0;
            Result = input;
            var replacer = GetReplacer();
            foreach (var iteration in Replacements)
            {
                foreach (var replacement in iteration)
                {
                    replacer(replacement.a, replacement.b);
                }
            }

            return Result;
        }

        private Action<string, string> GetReplacer()
        {
            if (UseRegex)
            {
                var opt = RegexOptions.Multiline;
                if (!CaseSensitive)
                    opt |= RegexOptions.IgnoreCase;
                return (a, b) =>
                {
                    var r = new Regex(a, opt);
                    Result = r.Replace(Result,  m =>
                    {
                        Count += 1;
                        return m.Result(b.RestoreControlChars());
                    });
                };
            }
            else
            {
                return CaseSensitive
                    ? (a, b) =>
                    {
                        Result = Result.ReplaceO(a, b, out var c);
                        Count += c;
                    }
                    : (a, b) =>
                    {
                        Result = Result.ReplaceOic(a, b, out var c);
                        Count += c;
                    };
            }
        }

        public void ParseTuples(IEnumerable<(string a, string b)> items)
        {
            if (!UseTemp)
            {
                Replacements.Add(items.ToList());
                return;
            }
            var first = new List<(string, string)>();
            var second = new List<(string, string)>();
            foreach (var pair in items)
            {
                var x = Guid.NewGuid().ToString();
                first.Add((pair.Item1, x));
                second.Add((x, pair.Item2));
            }
            Replacements.AddRange(new[] { first, second });
        }
        public void ParseText(string text)
        {
            var tuples = text.SplitLines().Select(l => l.Split(":")).Where(a => a.Length == 2).Select(a => (a[0], a[1]));
            ParseTuples(tuples);
               
        }
    }
}
