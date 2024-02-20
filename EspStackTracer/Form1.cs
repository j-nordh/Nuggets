using JetBrains.Annotations;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UtilClasses;
using UtilClasses.Extensions.Assemblies;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Lists;
using UtilClasses.Extensions.Processes;
using UtilClasses.Extensions.Strings;
using UtilClasses.Winforms;
using UtilClasses.Winforms.Extensions;

namespace EspStackTracer
{
    public partial class Form1 : Form
    {
        private bool _changing;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Show(string s)
        {
            _changing = true;
            rtxt.Text = s;
            rtxt.SelectAll();
            rtxt.SelectionFont = rtxt.Font;

            rtxt
                .Regex("^[0-9]+$").Size(14)
                .Regex("0x[^:]+").Color(Color.DarkGreen)
                .Regex(": ([^ ]*) at").Color(Color.DarkBlue).Bold()
                .Regex(@"[0-9a-f]+: ([^\(\n]*\([^\):]*\)) at").Color(Color.DarkBlue).Bold()
                .Regex("/([^$/]*)$").Bold()
                .Regex(@"^[0-9]+\s(.*)stack:", RegexOptions.Singleline).Color(Color.DarkRed);
            rtxt.DeselectAll();
            rtxt.SelectionStart = rtxt.TextLength;
            rtxt.ScrollToCaret();
            _changing = false;
        }

       
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_changing) return;
            var regex = new Regex(@"Backtrace:\s*(0x[0-9a-f]{8}:0x[0-9a-f]{8}\s*)+", RegexOptions.Multiline);
            var trim = new Regex(@"\s+", RegexOptions.Multiline);
            var traces = regex.Matches(rtxt.Text).OfType<Match>().Select(m => trim.Replace(m.Value, " ")).Distinct().ToList();
            if (traces.IsNullOrEmpty())
                return;
            var path = Path.GetTempFileName();
            var scriptPath = Path.Combine(txtWorkDir.Text, "decoder.py");



            var script = GetType().Assembly.GetResourceString("decoder.py");
            if (!Directory.Exists(txtWorkDir.Text))
                Directory.CreateDirectory(txtWorkDir.Text);

            if (!(File.Exists(scriptPath) && File.ReadAllText(scriptPath).Equals(script)))
                File.WriteAllText(scriptPath, script);
            var lines = rtxt.Text.SplitLines(true).ToList();
            
            
            var sb = new IndentingStringBuilder("  ");
            var count = 1;
            foreach (var t in traces)
            {

                File.WriteAllText(path, t);

                var i = lines.LastIndexOf(s => s.Contains(t.Trim()));
                sb.AppendLine(count.ToString()).Indent()
                    .AppendLines(lines.Skip(i-6).Take(5));
                
                var proc = getProc(scriptPath, path);
                proc.Start();
                proc.WaitForExit();
                sb.AppendLine(proc.StandardOutput.ReadToEnd());
                sb.Outdent();
                Show(sb.ToString());
                count++;
            }
            File.Delete(path);
        }

        Process getProc(string scriptPath, string tracePath)
        {
            var args = new[]
            {
                scriptPath,
                "-p", "ESP32",
                "-t", $"\"{txtToolPath.Text}\"",
                "-e", $"\"{txtElfPath.Text}\"",
                tracePath
            }.Join(" ");
            var proc = new Process();
            proc.StartInfo
                .FileName(txtPythonPath.Text)
                .Arguments(args)
                .NoShellExec()
                .RedirectStdOut();
            return proc;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
