﻿//using ApiClient.Models;
//using ApiClient.OAuth2;
using BomHelper.Dto;
using BomHelper.Farnell;
using BomHelper.KiCad;
using CLAP;
using JetBrains.Annotations;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;
using UtilClasses;
using UtilClasses.Cli;
using UtilClasses.Extensions.Assemblies;
using UtilClasses.Extensions.DateTimes;
using UtilClasses.Extensions.Decimals;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.Lists;
using UtilClasses.Extensions.Strings;
using UtilClasses.Files;

Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

var p = new BomHelper.Program();
var r = new Parser<BomHelper.Program>();
r.RunAsync(args, p).GetAwaiter().GetResult();

namespace BomHelper
{
    class Program
    {
        private const string DESC_DECK_PATH = "Path to the file describing the Deck";
        private const string DESC_BOM_PATH = "The path to the BOM file used";

        private const string DESC_CSV_OUT_DIR =
            "The directory where the generated CSVs should be placed. Defaults to the dir of the Deck.";
        private readonly ConsoleWriter _wr;
        private Deck _deck;
        private DictionaryOic<Component> _components;
        private DictionaryOic<PricePoint> _prices;
        private DictionaryOic<List<InventoryItem>> _inventory;


        public Program()
        {
            _wr = new ConsoleWriter();
        }

        public Dictionary<string, List<string>> ParseBomRef(string path) => ParseBomRef(File.Open(path, FileMode.Open));
        public Dictionary<string, List<string>> ParseBomRef(Stream stream) => ParseBom(stream, comp => comp.Ref);

        public Dictionary<string, List<string>> ParseBomFootprint(string path) =>
            ParseBom(File.Open(path, FileMode.Open), comp => comp.Footprint);
        public Dictionary<string, List<string>> ParseBomFootprint(Stream stream) =>
            ParseBom(stream, comp => comp.Footprint);
        private void ParseBom(Stream stream, Action<Comp> a)
        {
            var ser = new XmlSerializer(typeof(Export));
            var exp = ser.Deserialize(stream) as Export;
            foreach (var comp in exp!.Components.Comp)
            {
                if (_deck.Ignore.Contains(comp.Value))
                    continue;
                a(comp);
            }
        }

        private Dictionary<string, List<string>> ParseBom(Stream stream, Func<Comp, string> valueFunc) =>
            ParseBom(stream, comp => comp.Value, valueFunc);
        private Dictionary<string, List<string>> ParseBom(Stream stream, Func<Comp, string> keyFunc, Func<Comp, string> valueFunc)
        {
            Dictionary<string, List<string>> values = new();
            ParseBom(stream, comp => values.GetOrAdd(keyFunc(comp)).Add(valueFunc(comp)));
            return values;
        }

        [Verb(Aliases = "Clip,Clipboard", Description = "Parses a BOM file generated by KiCAD and copies the result to the clipboard in an Excel-friendly format")]
        [UsedImplicitly]
        public void SetClipboard([Description(DESC_BOM_PATH)] string bomPath)
        {
            var values = ParseBomRef(bomPath);
            var sb = new IndentingStringBuilder("  ");
            foreach (var val in values.Keys.AsSorted())
            {
                var refs = values[val];
                sb.AppendLine($"{refs.Count}\t\"{refs.Join(",")}\"\t\"{val}\"");

            }
            Clipboard.SetText(sb.ToString());
        }

        [Verb(Description = "Sets a card in the deck, allowing for additions as well as updates.")]
        [UsedImplicitly]
        public void SetCard([Aliases("deck")][Description(DESC_DECK_PATH)] string deckPath,
            [Description("The name of the card to be added/modified")] string cardName,
            [Description(DESC_BOM_PATH)] string bomPath)
        {
            LoadDeck(deckPath);
            var card = _deck.Cards.GetOrAdd(c => c.Name.EqualsOic(cardName), () => new Card() { Name = cardName });
            card.BomPath = bomPath;
            card.References = ParseBomRef(bomPath);
            SaveDeck(deckPath);
        }

        private class TableRow
        {
            public string Status { get; set; }
            public string Name { get; set; }
            public int Used { get; set; }
            public int Stored { get; set; }
            public int Needed => Math.Max(0, Used - Stored);
            public int InStock { get; set; }
            public decimal? Price { get; set; }
            public decimal? Cost => Needed * Price;
        }

        public enum TableOrder
        {
            Name,
            Cost,
            Price,
            Needed
        }
        [Verb(Description = "Prints a table with the latest know status for all components")]

        public void Table([Aliases("deck")][Description(DESC_DECK_PATH)] string deckPath, [Description("Determines how the table should be sorted, valid values are: Name, Cost, Price and Needed")]string order = "")
        {
            Console.WriteLine(GetType().Assembly.GetResourceString("table.txt"));
            LoadDeck(deckPath);
            var tbl = new ConsoleTable("Status", "Name", "Used", "Stored", "Needed", "Available", "Price", "Total");
            var okCount = 0;
            var totalTypes = 0;
            var compCount = 0;
            var totalCost = 0m;

            var rows = new List<TableRow>();
            foreach (var c in _deck.Components)
            {
                var used = _deck.CountComponents(c.Name);
                if (used == 0) continue;
                var row = new TableRow()
                {
                    Used = used,
                    Stored = _inventory.Maybe(c.Name)?.Sum(i => i.Amount) ?? 0,
                    Name = c.Name
                };
                var price = c.Prices.GetBest(row.Needed);
                bool ok = null != price;
                row.Price = price?.GetPricePoint(row.Needed)?.Price;
                row.Status = ok ? "OK" : "";
                row.InStock = price?.InStock ?? -1;

                rows.Add(row);

                okCount += ok ? 1 : 0;
                totalTypes += 1;
                totalCost += row.Cost ?? 0;
                compCount += row.Needed;
            }
            
            Func<TableRow, IComparable> f = Enum<TableOrder>.Parse(order) switch
            {
                TableOrder.Name => r => r.Name,
                TableOrder.Cost => r => r.Cost,
                TableOrder.Price => r => r.Price,
                TableOrder.Needed => r => r.Needed,
                _ => r=>r.Name
            };
            rows = rows.OrderBy(f).ToList();

            foreach (var row in rows)
            {
                tbl.AddRow(row.Status,
                    row.Name,
                row.Used,
                    row.Stored,
                row.Needed,
                    row.InStock,
                    row.Price?.ToString() ?? "",
                    row.Cost == null ? "" : row.Cost.Value.ToString("F").PadLeft(10));
            }

            tbl.Spacer();
            tbl.AddRow($"{okCount}/{totalTypes}", "", compCount, "", "", "", "", Math.Round(totalCost, 2));

            Console.WriteLine(tbl.Draw());

            tbl = new ConsoleTable("Card", "Count", "Components", "Types", "Price", "Cost");
            foreach (var card in _deck.Cards)
            {
                var count = 0;
                var cost = 0m;
                foreach (var r in card.References)
                {
                    count += r.Value.Count;
                    cost += _prices.Maybe(r.Key)?.Price * r.Value.Count ?? 0;
                }

                tbl.AddRow(
                    card.Name,
                    card.Count,
                    count,
                    card.References.Count,
                    $"{cost:F2} kr".PadLeft(10),
                    $"{cost * card.Count:F2} kr".PadLeft(12));
            }
            Console.WriteLine(tbl.Draw());

            tbl = new ConsoleTable("Item", "Total");
            foreach (var c in _inventory.Keys.OrderBy(o => o))
            {
                tbl.AddRow(c, _inventory[c].Sum(ii => ii.Amount));
            }
            Console.WriteLine("Current inventory");
            Console.WriteLine(tbl.Draw());
        }
        [Verb(Description = "Creates a zip-file containing fabrication files.")]
        public void Build([Aliases("deck")][Description(DESC_DECK_PATH)] string deckPath, string outDir)
        {
            LoadDeck(deckPath);
            var path = $"{_deck.Name}_{DateTime.UtcNow.ToFileString()}.zip";
            path = Path.Combine(outDir, path);
            using var zip = new ZipArchive(File.Open(path, FileMode.CreateNew), ZipArchiveMode.Create);
            var baseDir = Path.GetDirectoryName(deckPath);
            Console.WriteLine("Generating zip-file");
            foreach (var card in _deck.Cards)
            {
                var cardPath = Path.GetDirectoryName(card.BomPath);
                var count = 0;
                foreach (var file in Directory.EnumerateFiles(Path.Combine(cardPath, "Fabrication")))
                {
                    zip.CreateEntryFromFile(file, PathUtil.GetRelativePath(file, baseDir).RemoveAllOic("\\Fabrication").Replace("%20", " "));
                    count++;
                }
                Console.WriteLine($"  {card.Name}: {count} files.");
            }
            Console.WriteLine($"Generation completed, wrote {new FileInfo(path).Length / 1024}kB");
        }

        [Verb(Description = "Initializes a new deck with example data")]
        public void Init([Aliases("deck")][Description(DESC_DECK_PATH)] string deckPath)
        {
            var card = new Card() { Name = "Example", Count = 2 };
            card.AddReference("470R", "R1", "R2")
                .AddReference("100k", "R3", "R4");
            var ret = new Deck()
            {
                Cards = new List<Card>() { card }
            };
            ret.AddComponent("470R", "https://www.digikey.se/en/products/detail/riedon/S4-470RF1/1834174");
            _deck.ExchangeRates["EUR"] = 10.69m;
            SaveDeck(deckPath);
        }
        [Verb]
        public void Csv(
            [Aliases("deck")][Description(DESC_DECK_PATH)] string deckPath,
            [Aliases("out,output")][Description(DESC_CSV_OUT_DIR)] string outDir = null,
            [Aliases("delim,d")][Description("The delimiter used between fields in the file")] string delimiter = ";",
            [Aliases("headers,h")][Description("Include column headers in the file")] bool? includeHeader = true,
                [Aliases("fab,f")][Description("Copy files to fabrication folder next to kicad bom file, if exists")] bool? copyToFab = false)

        {
            delimiter ??= ";";
            includeHeader ??= true;
            copyToFab ??= false;
            LoadDeck(deckPath);
            var quantities = _components.Values.ToDictionaryOic(c => c.Name, c => _deck.CountComponents(c.Name));
            outDir ??= Path.GetDirectoryName(deckPath);
            var wr = new ConsoleWriter();
            foreach (var card in _deck.Cards)
            {
                var csv = new Csv<KeyValuePair<string, List<string>>>(delimiter, includeHeader.Value,
                    ("Qty", kvp => kvp.Value.Count.ToString()),
                    ("References", kvp => kvp.Value.Join(", ")),
                    ("Value", kvp => kvp.Key),
                    ("Footprint", kvp => _components[kvp.Key].Footprint),
                    ("Price", kvp => _prices[kvp.Key]?.Price.ToSaneString(2) ?? ""),
                    ("Cost", kvp => (kvp.Value.Count * _prices[kvp.Key]?.Price ?? 0).ToSaneString(2)),
                    ("Description", kvp => _components[kvp.Key].Description),
                    ("Part number", kvp => _components[kvp.Key].PartNumber),
                    ("Url", kvp => _components[kvp.Key].Prices.GetBest(quantities[kvp.Key])?.Source ?? _components[kvp.Key].Urls.First())
                    );
                csv.AddRange(card.References.Where(r => !(_components.Maybe(r.Key)?.NoCsv ?? false)));
                csv.AddEmptyRow().AddEmptyRow().AddRaw(card.Name);
                var file = Path.Combine(outDir!, $"{card.Name}.csv");
                Console.Write($"Generating {file}...");
                try
                {
                    var res = FileSaver.SaveIfChanged(file, csv.ToString()) ? "OK" : "Unchanged";
                    Console.WriteLine(res);
                    if (copyToFab.Value)
                    {
                        var fabDir = Path.Combine(Path.GetDirectoryName(card.BomPath), "Fabrication");
                        if (Directory.Exists(fabDir))
                        {
                            Console.Write($"Generating fabrication BOM...");
                            res = FileSaver.SaveIfChanged(Path.Combine(fabDir, "BOM.csv"), csv.ToString()) ? "OK" : "Unchanged";
                            Console.WriteLine(res);
                        }
                    }
                    Console.WriteLine(res);
                }
                catch (Exception e)
                {
                    wr.WithColor(ConsoleColor.DarkRed, $"Failed. {e.Message}");
                }


            }
        }
        //[Verb(Description = "Updates the tokens for the DigiKey API.")]
        //public async Task UpdateDigiKeyTokens()
        //{
        //    var settings = ApiClientSettings.CreateFromConfigFile();
        //    var svc = new OAuth2Service(settings);
        //    Console.WriteLine("Please paste the following URL in a browser:");
        //    Console.WriteLine(svc.GenerateAuthUrl());
        //    Console.Write("Please paste code (extracted from the url in the callback) here: ");
        //    var code = Console.ReadLine().Trim();
        //    var token = await svc.FinishAuthorization(code);
        //    svc.ClientSettings.UpdateAndSave(token);
        //    Console.WriteLine("Done!");
        //}


        [UsedImplicitly]
        [Verb(Description = "Updates all components with data from the internet")]
        public async Task UpdateComponents([Aliases("deck,path")][Description(DESC_DECK_PATH)] string deckPath, bool? force = false)
        {
            LoadDeck(deckPath);
            DictionaryOic<string> partNumbers = new();
            force ??= false;
            var i = 0;
            foreach (var c in _components.Values)
            {
                try
                {
                    var needed = _deck.CountComponents(c.Name);
                    if (needed == 0)
                        continue;
                    var updated = (c.Prices?.Select(p => p?.Updated).Min()) ?? DateTime.UtcNow.AddDays(-1);
                    if ((DateTime.UtcNow - updated).TotalHours < 8 && !force.Value)
                        continue;

                    //if (c.Prices?.Any(p => null != p) ?? false)
                    //    continue;

                    i += 1;
                    if (i % 5 == 0)
                        SaveDeck(deckPath);

                    _wr.Write($"{c.Name}... ");
                    try
                    {
                        await UpdateComponent(c, partNumbers);
                    }
                    catch (Exception e)
                    {
                        _wr.WithColor(ConsoleColor.Red, $"Failed: {e.Message}");
                        continue;
                    }

                    var price = c.Prices.GetBest(needed);

                    var inStock = price?.InStock ?? 0;
                    _wr.WithColor(inStock >= needed ? ConsoleColor.Green : ConsoleColor.Red, $"{inStock}/{needed}", false);
                    var pp = price.GetPricePoint(needed);
                    if (null == pp)
                    {
                        _wr.WriteLine();
                        continue;
                    }
                    _wr.WriteLine($" á {pp.Price} (min {pp.BreakPoint}) = {pp.Price * needed}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            SaveDeck(deckPath);
            foreach (var c in _components.Values)
            {
                var url = c.Prices.GetBest(_deck.CountComponents(c.Name))?.Source;
                if (null == url) continue;
                if (!partNumbers.ContainsKey(url))
                    continue;
                c.PartNumber = partNumbers[url];
            }
            SaveDeck(deckPath);
        }

        [Verb(Description = "Updates all data from their associated BOM-files")]
        [UsedImplicitly]
        public void UpdateCards([Aliases("deck,path")][Description(DESC_DECK_PATH)] string deckPath)
        {
            LoadDeck(deckPath);
            foreach (var c in _deck.Cards)
            {
                _wr.WriteLine($"Card: {c.Name}")
                    .WriteLine($"BomFile: {c.BomPath}")
                    .WriteLine($"BOM Updated: {File.GetLastWriteTimeUtc(c.BomPath).ToSaneString()}");

                c.References = ParseBomRef(c.BomPath);
                _wr.WriteLine($"Components:{c.References.Count}").WriteLine("------------------------------------");
            }
            SaveDeck(deckPath);
            var missing = _deck.Missing();
            if (missing.Any())
                Console.WriteLine("The following components are missing in the database:\n\t" + missing.Select(kvp => $"{kvp.Key} ({kvp.Value.Join(", ")})").Join("\n\t"));
            var unused = _deck.Unused();
            if (unused.Any())
                Console.WriteLine("The following components are defined in the database but not used:\n\t" + unused.Join("\n\t"));
        }

        [Help]
        [Empty]
        [UsedImplicitly]
        public void ShowHelp(string help)
        {
            Console.WriteLine(GetType().Assembly.GetResourceString("bom.txt"));
            Console.WriteLine(help);
        }
        [UsedImplicitly]
        private async Task UpdateComponent(Component c, DictionaryOic<string> partNumbers)
        {
            c.Prices = new List<PriceInfo>();
            foreach (var f in GetFetchers(c, partNumbers))
            {
                c.Prices.Add(await f.GetPrice());
            }
        }
        [Verb(Description = "Updates the footprints from the BOM-files")]
        [UsedImplicitly]
        private void UpdateFootprints([Aliases("deck,path")][Description(DESC_DECK_PATH)] string deckPath, bool listUntranslated)
        {
            LoadDeck(deckPath);
            var dict = new DictionaryOic<List<string>>();
            foreach (var d in _deck.Cards.Select(c => ParseBomFootprint(c.BomPath)))
            {
                foreach (var key in d.Keys)
                {
                    dict.GetOrAdd(key).AddRange(d[key]);
                }
            }

            var footprintTranslations =
                JsonConvert.DeserializeObject<DictionaryOic<string>>(File.ReadAllText("footprints.json"));

            var ignored = new List<string>();
            var untranslated = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var key in dict.Keys)
            {
                var footprints = dict[key].Distinct().ToList();
                if (footprints.Count == 1)
                {
                    var fp = footprints.Single();
                    if (null == fp)
                    {
                        ignored.Add(key);
                        continue;
                    }
                    if (footprintTranslations.ContainsKey(fp))
                        fp = footprintTranslations[fp];
                    else if (fp.ContainsOic(":"))
                        untranslated.Add(fp);
                    _wr.WithColor(ConsoleColor.DarkGreen, $"{key}: {fp}");

                    _components[key].Footprint = fp;
                }
                else
                    _wr.WithColor(ConsoleColor.DarkRed,
                        $"{key} is associated with multiple footprints: {footprints.Join(", ")}");
            }

            if (ignored.Count > 0)
            {
                _wr.WithColor(ConsoleColor.Yellow,
                    () => _wr
                        .WriteLine("The following components have been ignored:")
                        .SetIndent("  ")
                        .WriteLine(ignored.Join("\n"))
                        .SetIndent(""));
            }

            if (listUntranslated && untranslated.Count > 0)
            {
                _wr.WriteLine("The following footprints are not translated:").SetIndent("  ")
                    .WriteLine(untranslated.ToList().OrderBy(a => a).Join("\n")).SetIndent("");
            }

            var noFootprints = _deck.Components.Where(c => c.Footprint.IsNullOrEmpty()).ToList();
            if (noFootprints.Any())
                _wr.WriteLine("The following components lack a footprint:")
                    .SetIndent("  ")
                    .WriteLine(noFootprints.Select(c => c.Name).Join("\n"))
                    .SetIndent("");

            SaveDeck(deckPath);
        }

        private List<IPriceFetcher> GetFetchers(Component c, DictionaryOic<string> partNumbers) => c.Urls.Select(url => _deck.GetFetcher(url, c.ProductId, partNumbers)).ToList();

        [Verb(Description = "Runs checks to determine the state of the BOM.")]
        [UsedImplicitly]
        public void Check([Aliases("deck,path")][Description(DESC_DECK_PATH)] string deckPath, bool purge = false)
        {
            LoadDeck(deckPath);
            var orphans = _deck.FindOrphans();
            if (orphans.Any())
            {
                _wr.WithColor(ConsoleColor.Yellow, "The following identifiers lack a component definition:");

                foreach (var card in orphans)
                {
                    _wr.WriteLine(card.Key);
                    _wr.Indent = "  * ";
                    card.Value.ForEach(_wr.WriteLine);
                    _wr.Indent = "";
                }
            }

            void PrintTrouble(ConsoleColor color, string caption, IEnumerable<Component> comps)
            {
                var lst = comps.ToList();
                if (lst.IsNullOrEmpty()) return;
                _wr.WithColor(color, caption);
                _wr.Indent = "  * ";
                lst.ForEach(c => _wr.WriteLine($"{c.Name} ({_deck.CountComponents(c.Name)} needed)."));
                _wr.Indent = "";
            }

            var noPrice = _deck.Components
                .Where(c => _deck.CountComponents(c.Name) > 0)
                .Where(c => c.GetPricePoint(_deck.CountComponents(c.Name)) == null)
                .ToList();

            PrintTrouble(ConsoleColor.Yellow, "The following components lack a valid purchase link:",
                noPrice.Where(c => c.Urls.IsNullOrEmpty()));

            PrintTrouble(ConsoleColor.Red, "The following components are not available for purchase:",
                noPrice.Where(c => c.Urls.Any()));

            var unused = _deck.Components.Where(c => _deck.CountComponents(c.Name) == 0).ToList();
            if (purge)
            {
                foreach (var c in unused)
                    _components.Remove(c.Name);
                SaveDeck(deckPath);
            }
            else
            {
                PrintTrouble(ConsoleColor.DarkYellow, "The following defined components are not used:",
                    unused);
            }
        }

        private void LoadDeck(string deckPath)
        {
            _deck = JsonConvert.DeserializeObject<Deck>(File.ReadAllText(deckPath, Encoding.UTF8));
            if (null == _deck)
                throw new NullReferenceException($"Could not load a valid deck from {deckPath}");
            _deck.ExchangeRates ??= new();
            _components = _deck.Components.ToDictionaryOic(c => c.Name);
            _prices = new DictionaryOic<PricePoint>();
            foreach (var c in _deck.Components)
            {
                var needed = _deck.CountComponents(c.Name);
                var price = c.Prices.GetBest(needed);
                _prices[c.Name] = price.GetPricePoint(needed);
            }

            var inventoryPath = Path.Join(Path.GetDirectoryName(deckPath), "inventory.json");
            if (!File.Exists(inventoryPath))
            {
                _inventory = new();
                return;
            }

            _inventory = JsonConvert.DeserializeObject<Dictionary<string, List<InventoryItem>>>(
                File.ReadAllText(inventoryPath, Encoding.UTF8)).ToDictionaryOic();
        }

        private void SaveDeck(string deckPath)
        {
            _deck.Components = _components.Values.OrderBy(c => c.Name).ToList();
            var updated = new FileSaver(deckPath, JsonConvert.SerializeObject(_deck, Formatting.Indented)).SaveIfChanged() ? "Updated" : "Unchanged";
            Console.WriteLine($"{Path.GetFileNameWithoutExtension(deckPath)}: {updated}");
            var inventoryPath = Path.Join(Path.GetDirectoryName(deckPath), "inventory.json");
            updated = FileSaver.SaveIfChanged(inventoryPath,
                JsonConvert.SerializeObject(_inventory, Formatting.Indented))
                ? "Updated"
                : "Unchanged";
            Console.WriteLine($"{Path.GetFileNameWithoutExtension(inventoryPath)}: {updated}");
        }
    }
}