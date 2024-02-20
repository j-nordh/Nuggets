using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UtilClasses;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
using UtilClasses.Winforms.Extensions;

namespace FreePin
{
    public partial class Form1 : Form
    {
        ChipDef _def;
        private Dictionary<string, DrawPin> _pins = new DictionaryOic<DrawPin>();
        private readonly string _configDir;
        public Form1()
        {
            InitializeComponent();
            _configDir = Path.Combine(Environment.CurrentDirectory, "ChipDefinitions");
        }

        private void SetDef(string path)
        {
            if(!File.Exists(path))
            {
                _def = null;
                _pins = null;
                return;
            }
            _def = JsonConvert.DeserializeObject<ChipDef>(
                File.ReadAllText(path));
            flpPredefined.Controls.Clear();
            flpPredefined.Controls.AddRange(_def.Functions
                .Select(f => new CheckBox { Text = f.Name, Checked = true, Tag = f })
                .ForEach(chk => chk.CheckedChanged += Refresh));
            _pins = _def.Pins.Select(kvp => new DrawPin { Name = kvp.Key, Text = kvp.Value.Comment, Color = Color.Black }).ToDictionary(x=>x.Name);
            int y = 0;
            var c = 0;
            foreach (var line in _def.Header)
            {
                int x = -1;
                foreach (var txt in line)
                {
                    x += 1;

                    var dp = _pins.Maybe(txt);
                    if (null == dp)
                    {
                        dp = new DrawPin() { Name = $"{txt}_{c++}", Color = Color.Blue };
                        _pins.Add(dp.Name, dp);
                    }
                        

                    dp.X = x;
                    dp.Y = y;
                }

                y += 1;
            }
            Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cbModule.Items.AddRange(Directory.GetFiles(_configDir, "*.json").Select(Path.GetFileName).Cast<object>().ToArray());

            var fonts = new InstalledFontCollection();
            cbFont.Items.AddRange(fonts.Families.Where(ff => ff.IsStyleAvailable(FontStyle.Regular))
                .Select(ff => ff.Name).AsSorted().ToArray());
            for (int i = 0; i < cbFont.Items.Count; i++)
            {
                if (!Font.FontFamily.Name.EqualsOic(cbFont.Items[i].ToString()))
                    continue;
                cbFont.SelectedIndex = i;
                break;
            }

            MouseDoubleClick += Refresh;
            nudFontSize.ValueChanged += Refresh;
            nudSmallFontSize.ValueChanged += Refresh;
            cbFont.SelectedIndexChanged += Refresh;
            cbModule.SelectedIndexChanged += DefChanged;
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            Refresh();
        }

        private void DefChanged(object sender, EventArgs e)
        {
            SetDef(Path.Combine(_configDir, cbModule.SelectedItem.ToString()));
        }

        private void Refresh(object sender, EventArgs e) => Refresh();

        private void SetColors()
        {
            foreach (var p in _pins.Values)
            {
                p.Color = Color.Black;
                var pin = _def.Pins.Maybe(p.Name);
                if (null == pin)
                {
                    if (p.Name.Contains("_"))
                    {
                        p.Color = Color.Blue;
                    }
                    continue;
                }
                
                if (pin.In.EqualsOic("ok") && pin.Out.EqualsOic("ok"))
                {
                    p.Color = Color.Green;
                    continue;
                }
                p.Color = Color.Gold;
            }

            var colorDict = new Dictionary<int, Color>();
            foreach (var f in flpPredefined.Controls.OfType<CheckBox>().Where(chk => chk.Checked).Select(chk => chk.Tag).Cast<Function>())
            {
                var c = f.Color.MaybeAsColor() ?? Color.Red;

                foreach (var pin in f.Pins)
                {
                    _pins.Maybe(pin)?.With(c);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Control);
            base.OnPaint(e);
            if (_pins.IsNullOrEmpty()) return;
            SetColors();
            
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(40,40,40)), 0, 0, flpRight.Left, ClientRectangle.Height);

            
            var rect = new Rectangle(flpRight.Left / 10, ClientRectangle.Height / 10, (int)(flpRight.Left * 0.8),
                (int)(ClientRectangle.Height * 0.8));

            var h = rect.Height / (_pins.Values.Max(p => p.Y) - _pins.Values.Min(p => p.Y )+1);
            var w = rect.Width / (_pins.Values.Max(p => p.X) - _pins.Values.Min(p => p.X) + 1);
            var format = StringFormat.GenericDefault;
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Far;
            var familyName = cbFont.SelectedItem.ToString();
            var largeFont = new Font(familyName, (float)nudFontSize.Value);
            var smallFont = new Font(familyName, (float)nudSmallFontSize.Value);

            void DrawPin(DrawPin drawPin, int width)
            {
                var x = rect.X + drawPin.X * w;
                var y = rect.Y + drawPin.Y * h;
                var r = new Rectangle(x, y, w-width+1, h - width+1);
                var offset = (int) (e.Graphics.MeasureString(drawPin.Name, smallFont).Width + 10);
                using (var pen = new Pen(drawPin.Color, width))
                    e.Graphics.DrawRectangle(pen, r);
                using (var brush = new SolidBrush(drawPin.Color))
                    e.Graphics.DrawString(drawPin.Text, largeFont, brush, new Rectangle(x + offset, y, w - offset, h));

                e.Graphics.DrawString(drawPin.Name.SubstringBefore("_"), smallFont, Brushes.LightGray, r.X + 1, r.Y + 2);
            }

            _pins.Values.Where(p => p.Color == Color.Black).ForEach(p => DrawPin(p, 1));
            _pins.Values.Where(p => p.Color != Color.Black).ForEach(p => DrawPin(p, 3));
        }

        private void btnBrowseFuncDef_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                DefaultExt = "json",
                Multiselect = false,
                Title = "Please select a project function definition"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var funcs = JsonConvert.DeserializeObject<List<Function>>(File.ReadAllText(ofd.FileName));
            lvProjectFunctions.Items.Clear();
            lvProjectFunctions.Items.AddRange(
                funcs.Select(f =>
                {
                    var ret = new ListViewItem(new[] { f.Name, f.Pins.Select(p => p.ToString()).Join(", ") });
                    ret.SubItems.Add(new ListViewItem.ListViewSubItem { BackColor = f.GetColor() });
                    ret.Tag = f;
                    return ret;
                }));

        }
    }

    static class FreePinExtensions
    {
        public static Color GetColor(this Function f) => f.Color.IsNullOrEmpty() ? Color.Red : f.Color.AsColor();
    }
}
