using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Exceptions;
using System.Diagnostics;
using UtilClasses.Extensions.Integers;

namespace VocabParser
{
    class Program
    {
        private readonly string _workDir;
        private readonly long _total;
        private readonly List<string> _words;
        private const string WORDS = "words.txt";
        private const string VOCAB = "vocab";
        private const string CACHE = "cache.txt";
        private const string TOTAL = "total";
        public Program(string workDir)
        {
            
            _workDir = workDir;
            EnsureExists(WORDS);
            EnsureExists(VOCAB);
            EnsureExists(TOTAL);

            _words = ReadAllLines(WORDS).ToList();
            _total = long.Parse(ReadAllLines(TOTAL).First());
        }
        private void EnsureExists(string file)
        {
            if (!File.Exists(GetWDPath(file))) throw new FileNotFoundException("Could not find " + file);
        }
        private string[] ReadAllLines(string file) => File.ReadAllLines(GetWDPath(file));
        private string GetWDPath(string file) => Path.Combine(_workDir, file);
        private string[] GenerateCache()
        {
            Console.Write("Generating cache...");
            var chars = new HashSet<char>(_words.Select(w => Char.ToLower(w[0])).Distinct()); //get the first character of each word and remove duplicates
            var lines = new List<string>();
            using (var rdr = new StreamReader(GetWDPath(VOCAB)))
            {
                while (!rdr.EndOfStream) //read the vocab file, one line at the time.
                {
                    var l = rdr.ReadLine();
                    if (!chars.Contains(Char.ToLower(l[0]))) continue;  //quick first check, does it start with a desired letter?
                    if (!_words.Any(l.StartsWithOic)) continue;         //only include words starting with our sought word
                    lines.Add(l);
                }
            }
            Console.WriteLine("Done!");
            var arr = lines.ToArray();
            File.WriteAllLines(GetWDPath(CACHE), arr);                  //save the cache for future use
            return arr;
        }

        private string[] GetLines()
        {
            var c = GetWDPath(CACHE);
            var v = GetWDPath(VOCAB);
            var w = GetWDPath(WORDS);
            if (File.Exists(c) &&                                       //there is no cache
                File.GetLastWriteTime(c) > File.GetLastWriteTime(v) &&  //vocabulary is newer than than cache
                File.GetLastWriteTime(c) > File.GetLastWriteTime(w))    //word file is newer than cache
                return File.ReadAllLines(c);
            return GenerateCache();
        }

        private void Run()
        {
            var lines = GetLines();
            using (var p = new ExcelPackage())
            {
                foreach (var word in _words)
                {
                    Console.WriteLine(word);
                    var ws = p.Workbook.Worksheets.Add(word);
                    //captions in the excel sheet
                    ws.Cells["A1"].Value = "Inkludera";
                    ws.Cells["B1"].Value = "Ord";
                    ws.Cells["C1"].Value = "Förekomster";
                    ws.Cells["E1"].Value = "Totalt";
                    using (var range = ws.Cells["A1:E1"])
                    {
                        range.Style.Font.Size = 14;
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }
                    //Formula
                    ws.Cells["E2"].Formula = $"SUMIF(A:A,1,C:C)/{_total}";
                    ws.Cells["E2"].Style.Numberformat.Format = "0.00000000%";
                    var i = 2;
                    foreach (var l in lines)
                    {
                        if (!l.StartsWithOic(word)) continue;   //does the line start with the correct word?
                        var w = l.SubstringBefore("\t");        //everything before the tab is the word
                        ws.Cells[$"B{i}"].Value = w;            
                        ws.Cells[$"C{i}"].Value = l.SubstringAfter("\t").AsInt(); //everything after the tab is the count
                        if (word.EqualsOic(w))
                            ws.Cells[$"A{i}"].Value = 1;        //pre set to 1 if the word matches exactly (ignoring case)
                        i += 1;
                    }
                }
                //save it!
                p.SaveAs(new FileInfo(GetWDPath("result.xlsx")));
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(@"
   _______________________________
 / \                              \.
|   |        VOCAB Parser         |.
 \_ |                             |.
    |This program parses a vocab- |.
    |file, looks for specified    |.
    |words and generates an excel |.
    |file with results.           |.
    |                             |.
    |Command line arguments:      |.
    | * A path to a directory with|.
    |   - A vocab file            |.
    |   - A words.txt file with   |.
    |     one word per line       |.
    |   - A total file containing |.
    |     only the total number of|.
    |     words in the vocab.     |.
    |   __________________________|___
    |  /                             /.
    \_/_____________________________/.");
                var p = new Program(args[0]);
                p.Run();
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.DeepToString());
            }
            if (Debugger.IsAttached)
                Console.ReadLine();
        }
    }
}

