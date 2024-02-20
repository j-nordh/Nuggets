using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Converters;
using UtilClasses;
using UtilClasses.Extensions.Assemblies;
using UtilClasses.Extensions.DateTimes;
using UtilClasses.Extensions.Decimals;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.Integers;
using UtilClasses.Extensions.Nullables;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;

namespace WhatDidIBuy
{
    public partial class Form1 : Form
    {
        private readonly string _dbFile = "db.json";
        private string OUTPUT_FILE = "index.html";

        private string PUBLIC_OUTPUT_FILE =
            @"\\sp.se\RISE\Organisation\105105-Tid och optik\05-Arkiv_DigiMät\WhatDidIBuy.html";
        private string PRINT_FILE = "print.html";
        private List<Item> _items;
        public Form1()
        {
            InitializeComponent();

            var workDir = new DirectoryInfo(Environment.CurrentDirectory.Trim('\\'));
            if (workDir.FullName.EndsWithAnyOic("bin\\Debug", "bin\\Release"))
                workDir = workDir.Parent.Parent;
            _dbFile = Path.Combine(workDir.FullName, _dbFile);
            OUTPUT_FILE = Path.Combine(workDir.FullName, OUTPUT_FILE);
            PRINT_FILE = Path.Combine(workDir.FullName, PRINT_FILE);
            if (!File.Exists(_dbFile))
            {
                _items = new();
                return;
            }
            _items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(_dbFile));
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var str = Clipboard.ContainsText(TextDataFormat.Html)
                    ? Clipboard.GetText(TextDataFormat.Html)
                    : Clipboard.GetText();
                var sup = GetSupplier(str);
                var items = Parse(sup, str);
                if (items.IsNullOrEmpty()) return;
                var preCount = _items.Count;
                _items.AddRange(items);
                var comp = new ItemComparer();
                _items = new HashSet<Item>(_items, comp).ToList();
                UpdateStats();
                MessageBox.Show($"Imported {_items.Count - preCount} from {sup}", "Import succeeded");
            }
            catch (Exception a)
            {
                MessageBox.Show($"Caught an {a.GetType().Name} while importing:\n{a.Message}", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private List<Item> Parse(Supplier sup, string str)
        {
            var items = sup switch
            {
                Supplier.DigiKey => ParseDigiKey(str),
                Supplier.Farnell => ParseFarnell(str),
                Supplier.Electrokit => ParseElectroKit(str),
                Supplier.PurchaseOrder => ParsePurchaseOrder(str),
                _ => throw new ArgumentOutOfRangeException()
            };
            return items;
        }

        private Supplier IdentifyPurchaseOrderSupplier(string str)
        {
            var lines = str.SplitLines(true);
            string line = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].StartsWithOic("RISE"))
                    continue;
                line = lines[i];
                break;
            }

            if (line.IsNullOrEmpty())
                throw new ArgumentException("Could not find company line");

            var name = line.SubstringAfter("AB ");
            foreach (var sup in Enum<Supplier>.Values)
            {
                if (name.ContainsOic(sup.ToString())) return sup;
            }

            throw new ArgumentException("Could not find the supplier");
        }
        private List<Item> ParsePurchaseOrder(string str)
        {
            var ret = new List<Item>();
            var sup = IdentifyPurchaseOrderSupplier(str);
            var date = str.SubstringAfter("fakturering:").SubstringBefore("Inköpsorder").SplitLines()
                .First(l => l.StartsWithOic("2")).AsDateTime();
            str = str.SubstringAfter("Nettobel.").SubstringBefore("Summa");

            foreach (var part in str.SplitREE("@ri.se").Trim().NotNullOrWhitespace())
            {
                if (part.Contains("IBAN USD"))
                    continue;
                var l = part.ReplaceOic("\r", " ").ReplaceOic("\n", " ").ReplaceOic("  ", " ").Trim().SubstringAfter(" ");
                var pn = l.SubstringBefore(" ");
                var desc = l.SubstringAfter(pn).SubstringBefore(" ST ").SubstringBeforeLast(",").SubstringBeforeLast(" ").Trim();
                var count = l.SubstringAfter(desc).SubstringBefore(" ST ").Trim().AsDecimal().AsInt();
                var price = l.SubstringBefore("Kontaktperson").SubstringAfter("ST ").SubstringBefore(" ").AsDecimal();

                var itm = new Item()
                {
                    Ordered = date,
                    InventoryNumber = pn,
                    Supplier = sup,
                    Description = desc,
                    Count = count,
                    Price = price
                };
                ret.Add(itm);
            }

            return ret;
        }

        private static Supplier GetSupplier(string str)
        {
            if (!str.ContainsOic("SourceUrl:"))
                return Supplier.PurchaseOrder;
            var supplier = str.SubstringAfter("SourceUrl:").SubstringBefore("\n");
            if (supplier.ContainsOic("www."))
                supplier = supplier.SubstringAfter("www.");
            else if (supplier.Contains("se."))
                supplier = supplier.SubstringAfter("se.");

            if (supplier.Contains(".com"))
                supplier = supplier.SubstringBefore(".com");
            if (supplier.Contains(".se"))
                supplier = supplier.SubstringBefore(".se");
            var sup = Enum<Supplier>.Parse(supplier);
            return sup;
        }

        private List<Item> ParseElectroKit(string str)
        {
            var ret = new List<Item>();
            var ordered = str.SubstringAfter("gjordes den").SubstringBefore("</mark>").SubstringAfterLast(">").AsDateTime();
            str = str.SubstringAfter("<tbody");
            var rows = str.SplitREE("</tr>");
            foreach (var row in rows)
            {
                if (row.Contains("</tbody>")) continue;
                var cells = row.SplitREE("</td>");
                var count = cells[0].SubstringAfter("×").SubstringBefore("<").Trim();
                if (!int.TryParse(count, out _)) continue;
                var itm = new Item()
                {
                    Link = cells[0].SubstringAfter("href=\"").SubstringBefore("\""),
                    Description = cells[0].SubstringAfter("<a").SubstringAfter(">").SubstringBefore("<"),
                    Ordered = ordered,
                    Supplier = Supplier.Electrokit,
                    Count = count.AsInt(),
                    Price = cells[1].SubstringAfter("<bdi").SubstringBefore("<").SubstringAfter(">").AsDecimal()
                };
                if (itm.Description.EqualsOic("BESTÄLL IGEN")) continue;
                if (!itm.Link.Contains("http")) continue;
                ret.Add(itm);
            }

            return ret;
        }
        private List<Item> ParseDigiKey(string str)
        {
            var imgDir = Path.Combine("img", "digikey");
            var ordered = str.SubstringAfter("Placerad").SubstringAfter("<p").SubstringAfter(">").SubstringBefore("<").MaybeAsDateTime();
            str = str.SubstringAfter("ro-products")
                .SubstringAfterLast("<table class=\"mud-table-root\">")
                .SubstringAfter("<tbody");
            var rows = str.SplitREE("</tr>");
            var ret = new List<Item>();
            foreach (var row in rows)
            {
                if (row.Contains("</tbody>")) continue;
                var cells = row.SplitREE("</td>");
                if (cells.Length < 6) continue;
                DictionaryOic<string> _keywords = new();
                foreach (var cell in cells)
                {
                    var key = cell.SubstringAfter("data-label=\"").SubstringBefore("\"");
                    _keywords[key] = cell.SubstringAfter(">");
                }

                var prodInfo = _keywords["Produktinformation"];
                var img = prodInfo.SubstringAfter("src=\"").SubstringBefore("\" ");
                var parts = prodInfo.SplitREE("<p").Skip(1).Select(s => s.SubstringAfter(">").RemoveAll("</p>")).ToList();
                var itm = new Item()
                {
                    Supplier = Supplier.DigiKey,
                    Link = parts[0].SubstringAfter("href=\"").SubstringBefore("\" "),
                    Description = parts[2],
                    ProductNumber = parts[1],
                    InventoryNumber = parts[0].SubstringAfter(">").SubstringBefore("<"),
                    ImageUrl = img,
                    Ordered = ordered,
                    Count = _keywords["Antal"].AsInt(),
                    Price = _keywords["Enhetspris"].RemoveAll("kr").Trim().AsDecimal()
                };

                ret.Add(itm);

            }
            return ret;
        }

        private List<Item> ParseFarnell(string str)
        {
            var ret = new List<Item>();
            var bodies = str.SplitREE("</tbody>");
            var orderDate = str.SubstringAfter("Orderdatum").SubstringBefore("</p").SubstringAfterLast(">")
                .AsDateTime();
            foreach (var body in bodies)
            {
                var b = body.SubstringAfter("<tbody").SubstringAfter(">");
                foreach (var row in b.SplitREE("</tr>"))
                {
                    if (row.ContainsOic("Radkommentar")) continue;
                    if (row.ContainsOic("</form>")) continue;
                    var cells = row.SplitREE("</td>");
                    var itm = new Item()
                    {
                        Link = cells[0].SubstringAfter("href=\"").SubstringBefore("\""),
                        InventoryNumber = cells[0].SubstringBefore("</a>").SubstringAfterLast(">"),
                        ProductNumber = cells[0].SubstringAfter("Tillverkarnummer").SubstringBefore("</a>")
                            .SubstringAfterLast(">"),
                        Supplier = Supplier.Farnell,
                        Description = cells[1].SubstringAfter("<span class=\"desc").SubstringAfter(">")
                            .SubstringBefore("</span>"),
                        Ordered = orderDate,
                        Count = cells[2].SubstringAfter("Beställd kvantitet").SubstringBefore("</div>").Trim().Trim('>').SubstringBeforeLast("</span>").SubstringAfterLast(">").AsInt(),
                        Price = cells[3].SubstringAfter("Enhetspris").SubstringBefore("</div>").Trim().Trim('>').SubstringAfterLast(">").SubstringBefore("kr").Trim().AsDecimal()
                    };
                    ret.Add(itm);
                }
            }
            return ret;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_dbFile, JsonConvert.SerializeObject(_items, Formatting.Indented));
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            var result = new KeywordReplacer()
                .Add("ROWS", RenderRows(_items))
                .Run(GetType().Assembly.GetResourceString("index.template"));

            File.WriteAllText(OUTPUT_FILE, result);
            File.WriteAllText(PUBLIC_OUTPUT_FILE, result);
        }

        private string RenderRows(IEnumerable<Item> items)
        {
            IndentingStringBuilder sb = new("  ");
            string Cell(string s) => s.IsNullOrEmpty() ? "<td></td>" : $"<td>{s}</td>";
            string Img(string s) => s.IsNullOrEmpty() ? Cell(null) : Cell($"<img src=\"{s}\"/>");

            string Link(string text, string url) =>
                url.IsNullOrEmpty() ? Cell(text) : Cell($"<a href=\"{url}\">{text}</a>");

            foreach (var i in items)
            {
                sb.AppendLine("<tr>").Indent(_ =>
                        sb.AppendLines(Img(i.ImageUrl),
                            Link(i.Description, i.Link),
                            Cell(i.Supplier.ToString()),
                            Cell($"{i.InventoryNumber}<br/>{i.ProductNumber}"),
                            Cell(i.Ordered.ToSaneString())
                        ))
                    .AppendLine("</tr>");
            }

            return sb.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateStats();
        }
        private void UpdateStats()
        {
            lvStats.BeginUpdate();
            lvStats.Items.Clear();
            var totalOrdered = 0;
            var totalItems = 0;
            var totalCount = 0;
            var totalCost = 0m;
            foreach (var s in Enum<Supplier>.Values)
            {
                var lst = _items.Where(i => i.Supplier == s).ToList();
                var ordered = lst.Select(i => i.Ordered).Distinct().Count();
                decimal sum = 0;


                var lastOrdered = lst.Max(i => i.Ordered).TryGet(out var max)
                    ? max.ToShortDateString()
                    : "";

                foreach (var @decimal in lst.Select(i => i.Price * i.Count)) sum += @decimal;
                var lvi = new ListViewItem(new[]
                    {s.ToString(), ordered.ToString(), lst.Count.ToString(), lst.Sum(i=>i.Count).ToString(), sum.ToSaneString(2), lastOrdered});
                lvStats.Items.Add(lvi);
                totalOrdered += ordered;
                totalItems += lst.Count;
                totalCount += lst.Sum(i => i.Count);
                totalCost += lst.Sum(i => i.Price * i.Count);
            }

            lvStats.Items.Add(new ListViewItem(new[] { "Totalt", totalOrdered.ToString(), totalItems.ToString(), totalCount.ToString(), totalCost.ToSaneString(2) }));
            lvStats.EndUpdate();
        }

        private void lvStats_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var str = Clipboard.ContainsText(TextDataFormat.Html)
                ? Clipboard.GetText(TextDataFormat.Html)
                : Clipboard.GetText();
            var supplier = GetSupplier(str);
            var items = Parse(supplier, str);
            var result = new KeywordReplacer()
                .Add("ROWS", RenderRows(items))
                .Run(GetType().Assembly.GetResourceString("print.template"));

            File.WriteAllText(PRINT_FILE, result);
            MessageBox.Show($"Wrote {items.Count} items to {PRINT_FILE}");
        }

        private void lvStats_DoubleClick(object sender, EventArgs e)
        {
            var name = lvStats.SelectedItems.Cast<ListViewItem>().FirstOrDefault()?.Text;
            if (null == name)
                return;

            var supplier = Enum<Supplier>.Parse(name);
            var dates = _items
                .Where(i => i.Supplier == supplier)
                .Select(i => i.Ordered)
                .NotNull()
                .Distinct()
                .OrderByDescending(d => d)
                .Select(d => d.ToShortDateString())
                .Join("\n");
            MessageBox.Show($"Stuff was ordered on the following occasions\n{dates}");
        }
    }


    enum Supplier
    {
        DigiKey,
        Farnell,
        Electrokit,
        PurchaseOrder,
        Kjell,
        Elfa
    }
    class Item
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Supplier Supplier { get; set; }
        public string InventoryNumber { get; set; }
        public string ProductNumber { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Ordered { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
    class ItemComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.Ordered.HasValue ^ y.Ordered.HasValue) return false;
            if (x.Ordered.HasValue && y.Ordered.HasValue)
                if (Math.Abs((x.Ordered.Value - y.Ordered.Value).TotalHours) > 12)
                    return false;
            

            return x.Supplier == y.Supplier && x.InventoryNumber == y.InventoryNumber;
        }

        public int GetHashCode(Item obj)
        {
            unchecked
            {
                var hashCode = (int)obj.Supplier;
                hashCode = (hashCode * 397) ^ (obj.InventoryNumber != null ? obj.InventoryNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (obj.Ordered?.Date).GetHashCode();
                return hashCode;
            }
        }
    }
}
