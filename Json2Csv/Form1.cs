using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UtilClasses.Extensions.Types;
using UtilClasses.Extensions.Enumerables;
using System.Collections.Generic;
using OfficeOpenXml;

namespace Json2Csv
{
    public partial class Form1 : Form
    {
        List<Page> _result;
        public Form1()
        {
            InitializeComponent();
            flpParsers.Controls.AddRange(
                typeof(Form1).Assembly.DefinedTypes
                .Implementing<IParser>()
                .RequireNew()
                .Select(t => new RadioButton()
                {
                    Text = t.Name.Replace("Parser", ""),
                    Tag = t.GenerateConstructor<IParser>()
                })
                .ForEach(rb=> rb.CheckedChanged += Rb_CheckedChanged)
                .ToArray());
        }

        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            var rb = (RadioButton)sender;
            if (!rb.Checked) return;
            OnChange();
        }

        private void rtxtInput_TextChanged(object sender, EventArgs e)
        {
            OnChange();
        }
        private void OnChange()
        {
            btnSave.Enabled = false;
            if (rtxtInput.Text == "") { rtxtInput.BackColor = SystemColors.Window; return; }
            var rb = flpParsers.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
            bool ok = true;
            if(null == rb)
            {
                flpParsers.BackColor = Color.Salmon;
                ok = false;
            }
            flpParsers.BackColor = SystemColors.Control;
            JToken root=null;
            try
            {
                root = JToken.Parse(rtxtInput.Text);
                rtxtInput.BackColor = Color.LightGreen;
            }
            catch
            {
                rtxtInput.BackColor = Color.Salmon;
                ok = false;
            }

            if (!ok) return;
            var p = ((Func<IParser>)rb.Tag)();
            
            _result = p.Parse(root).ToList();
            btnSave.Enabled = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                OverwritePrompt = true,
                Title = "Please select a file name",
                FileName = "ConvertedJson " + DateTime.Now.ToShortDateString() + ".xlsx"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (var ep = new ExcelPackage())
            {
                foreach(var page in _result)
                {
                    var ws = ep.Workbook.Worksheets.Add(page.Name);
                    page.Headings.ForEach((i, h) => ws.Cells[GetCell(i)].Value = h);
                    using (var range = ws.Cells[GetCell(0)+":"+GetCell(page.Headings.Count-1)])
                    {
                        range.Style.Font.Size = 14;
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    }
                    int c = 2;
                    foreach(var r in page.Rows)
                    {
                        r.ForEach((i, v) => ws.Cells[GetCell(i, c)].Value = v);
                        c++;
                    }
                }
                ep.SaveAs(new System.IO.FileInfo(sfd.FileName));
            }
            /*
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
             */
        }

        private string GetCell(int i, int row=1)
        {
            if (i < 26) return ((char)('A'+i))+ (row>0?row.ToString():"");
            return GetCell(i / 26,0) + GetCell(i%26, row);
        }
    }
}
