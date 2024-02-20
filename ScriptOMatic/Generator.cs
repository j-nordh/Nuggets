using Newtonsoft.Json;
using SupplyChain.Dto;
using SupplyChain.Procs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptOMatic.Generate;
using ScriptOMatic.Generate.Extensions;
using ScriptOMatic.Generate.SQL;
using ScriptOMatic.Pages;
using SourceFuLib;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.CodeGen;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Exceptions;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Types;
using UtilClasses.Files;
using UtilClasses.Winforms.Extensions;
namespace ScriptOMatic
{
    public partial class Generator : Form
    {
        SourceFuSettings _settings;
        CodeEnvironment _currentEnv;
        private PopulatedBundle _currentBundle;
        string _settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScriptOMatic", "Settings.json");
        private Dictionary<string, Bundle> _bundles;
        private CrudSpPage _spPage;

        public enum ContentType
        {
            Bundle,
            StoredProcedures,
            DTO,
            Repo

        }

        public Generator()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            txtResult.AllowDrop = true;
            LoadSettings();
        }



        private void LoadSettings()
        {

            if (!File.Exists(_settingsPath))
            {
                var s = new CodeEnvironment()
                {
                    Db = new DbSettings
                    {
                        Server = "localhost",
                        Name = "Recs",
                        DbDefinition = @"c:\Source\Shared\Dev\Servers\Recs\DbDefinitions.json"
                    },
                    Dto = new DtoSettings
                    {
                        Dir = @"c:\Source\Shared\Dev\Shared\Recs\Recs.Dto\",
                        Namespace = "Recs.Dto",
                        NamespaceMap = @"c:\Source\Shared\Dev\Servers\Recs\Namespaces.txt",
                        Project = @"c:\Source\Shared\Dev\Shared\Recs\Recs.Dto\Recs.Dto.csproj"
                    },
                    Repo = new RepoSettings
                    {
                        Dir = @"c:\Source\Shared\Dev\Servers\Recs\Recs.Repo\",
                        Namespace = "Recs.Repo",
                        Project = @"c:\Source\Shared\Dev\Servers\Recs\Recs.Repo\Recs.Repo.csproj"
                    },
                    Controller = new ProjectSettings
                    {
                        Dir = @"c:\Source\Shared\Dev\Servers\Recs\Recs.Server\Controllers\",
                        Namespace = "Recs.Server.Controllers",
                        Project = @"c:\Source\Shared\Dev\Servers\Recs\Recs.Server\Recs.Server.csproj"
                    },
                    ServerSolution = @"c:\Source\Shared\Dev\Solutions\Servers.sln",
                };
                SaveSettings(s);
            }

            _settings = JsonConvert.DeserializeObject<SourceFuSettings>(File.ReadAllText(_settingsPath, Encoding.UTF8));
            cbEnvironment.Items.AddRange(_settings.Environments.Keys.ToArray());
        }

        private void SaveSettings(CodeEnvironment env = null)
        {
            if (null != env)
                _settings.SaveEnvironment(env);

            var dir = Path.GetDirectoryName(_settingsPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(_settingsPath, JsonConvert.SerializeObject(_settings));
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var env = ArgParser.Env;
            _spPage = new CrudSpPage() { Dock = DockStyle.Fill };
            _spPage.NewContent += OnNewContent;
            if (null != env)
                cbEnvironment.SelectItem(env);
            btnRun.Enabled = _settings != null;
            btnCompile.Enabled = _settings != null;
            tcContent.TabPages.Clear();
            tcContent.TabPages.AddRange(new[]
            {
                GetPage("Bundle", ContentType.Bundle),
                GetPage("Stored procedures", ContentType.StoredProcedures),
                GetPage("Data Transfer Object", ContentType.DTO),
                GetPage("Repository", ContentType.Repo)
            });

            MainSplit.Panel1.Controls.Add(_spPage);

        }

        private TabPage GetPage(string text, ContentType ct) => new TabPage { Text = text, Tag = ct };
        private ContentType CurrentCT => (ContentType)tcContent.TabPages[tcContent.SelectedIndex].Tag;



        private void OnNewContent(PopulatedBundle b)
        {
            try
            {
                txtResult.Text = GetContent(b, CurrentCT);
                _currentBundle = b;
                _bundles[b.Table.Name] = new Bundle(b);
                txtResult.ForeColor = SystemColors.WindowText;
            }
            catch (Exception e)
            {
                txtResult.ForeColor=Color.DarkRed;
                txtResult.Text = e.DeepToString();
            }
        }

        public string GetContent(PopulatedBundle b, ContentType ct)
        {
            var sb = new IndentingStringBuilder("  ");
            switch (ct)
            {
                case ContentType.Bundle:
                    return JsonConvert.SerializeObject(new Bundle(b), Formatting.Indented);
                case ContentType.StoredProcedures:
                    sb.AppendObjects(null == b.LinkTable ? Sql.CrudSPs(b) : Sql.LinkTable(b.LinkTable), "\r\nGO\r\n");
                    break;
                case ContentType.DTO:
                    new DtoRenderer(b)
                        .Append(b.Columns)
                        .RenderTo(sb.SetIndentChars("\t"));
                    break;
                case ContentType.Repo:
                    new RepoRenderer(sb.SetIndentChars("\t"), _currentEnv)
                        .Append(b, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sb.ToString();
        }


        private void btnRun_Click(object sender, EventArgs e)
        {
            if (_currentBundle == null) return;
            btnRun.Enabled = false;
            var def = _currentEnv.GetDbDef();
            def.Bundles = _bundles.Values
                .Select(b => new Bundle(b)) //strip populated values
                .AsSorted(b => b.Table.Name);
            var sb = new IndentingStringBuilder("\t");
            sb.Encapsulate("SQL", RunSql(GetContent(_currentBundle, ContentType.StoredProcedures)))
                .Encapsulate("DTO", RunDto(GetContent(_currentBundle, ContentType.DTO)))
                .Encapsulate("Repo", RunRepo(GetContent(_currentBundle, ContentType.Repo)))
                .AppendLines();
            

            _currentEnv.Db.SaveDbDef(def);
            txtResult.Text = sb.ToString();
            MessageBox.Show("Done!");
            btnRun.Enabled = true;
        }
        private string RunSql(string sql)
        {
            string lastPart = "";
            try
            {
                 
                using (var conn = new SqlConnection($"Data Source={_currentEnv.Db.Server}; Database={_currentEnv.Db.Name}; Integrated Security=True;"))
                {
                    conn.Open();
                    var parts = sql.SplitREE("GO\n", "GO\r\n");
                    foreach (var part in parts)
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            lastPart = part;
                            cmd.CommandText = part;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return $"Executed {parts.Count()} sql statements successfully";
                }
            }
            catch (Exception ex)
            {
                return  JsonConvert.SerializeObject(ex, Formatting.Indented) + " Caught while executing:_\r\n"+lastPart;
            }

        }
        private string RunDto(string sourceCode)
        {
            var cls = _currentBundle.Table.Singular;
            var file = $"{cls}.cs";
            var path = Path.Combine(_currentEnv.Dto.Dir, file);
            bool fileChanged = true;

            fileChanged = new FileSaver(path, sourceCode).WithBlocks(DtoRenderer.Blocks).SaveIfChanged();
            bool projChanged = new CsProject(_currentEnv.Dto.Project).Add_Compile(file).Save();
            var map = new TextFileMap(_currentEnv);
            map.Set(cls, _currentEnv.Dto.Namespace);
            var mapChanged = map.Save();

            return $@"Dto generation completed:
* File Generated: {fileChanged}
* Project updated: {projChanged}
* Map updated: {mapChanged}";
        }

        const string _separator = "------------------------------------------------------------------------------------------";
        class Proc
        {
            readonly string _name;
            readonly string _fileName;
            readonly string _args;
            readonly int _timeout;
            readonly string _workDir;
            private Process _p;
            public IndentingStringBuilder Output { get; }
            public event Action<string> Done;
            public event Action<string> Log;

            public Proc(string name, string fileName, string workDir, string args, int timeout = 20000)
            {
                _workDir = workDir;
                _timeout = timeout;
                _args = args;
                _fileName = fileName;
                _name = name;
                Output = new IndentingStringBuilder("\t");

            }
            private void LogLine(string line)
            {
                Output.AppendLine(line);
                Log?.Invoke(line);
            }
            private void LogLines(params string[] lines)
            {
                Output.AppendLines(lines);
                Log?.Invoke(lines.Join("\r\n"));
            }

            public Task<string> RunAsync()
            {
                var tcs = new TaskCompletionSource<string>();
                Done += message => tcs.SetResult(message);
                Run();
                return tcs.Task;
            }
            public void Run()
            {
                LogLines(_separator, _name.PadLeft(45 - _name.Length / 2, ' '));
                _p = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = _workDir,
                        UseShellExecute = false,
                        FileName = _fileName,
                        Arguments = _args,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                    },
                    EnableRaisingEvents = true
                };

                _p.OutputDataReceived += (s, e) =>
                {
                    LogLine(e.Data);
                    //txtResult.InvokeSetTextScrollToEnd(sb.ToString());
                };
                var errSb = new StringBuilder();
                _p.ErrorDataReceived += (s, e) => errSb.AppendLine(e.Data);
                _p.Exited += (s, e) =>
                {
                    if (_p.ExitCode == 0)
                    {
                        LogLines(_separator, "Result: OK");
                        Done?.Invoke(null);
                    }
                    else
                    {
                        var err = _p.StandardError.ReadToEnd();
                        LogLines(_separator,
                            "#######################",
                            "##  ERROR DETECTED  ###",
                            "#######################", err);
                        Done?.Invoke(err);
                    }

                    _p.Close();

                };

                try
                {
                    _p.Start();
                    _p.BeginOutputReadLine();
                }
                catch (Exception ex)
                {
                    LogLines(
                        "*********************",
                        "*     EXCEPTION     *",
                        "*********************",
                        JsonConvert.SerializeObject(ex, Formatting.Indented));
                }
            }
        }

        private string RunDbDefinition()
        {
            var b = JsonConvert.DeserializeObject<Bundle>(txtResult.Text);
            return _currentEnv.UpdateDbDefinitions(new[] { b })
                ? $"DbDefinition: {b.Table.Plural} updated"
                : "DbDefinition: Unchanged";
        }

        private string RunEnum()
        {
            var e = JsonConvert.DeserializeObject<EnumDef>(txtResult.Text.SubstringBefore("*****"));
            var dbDefs = JsonConvert.DeserializeObject<DbDef>(File.ReadAllText(_currentEnv.Db.DbDefinition));
            var enums = dbDefs.Enumerations.ToDictionary(d => d.Name, StringComparer.OrdinalIgnoreCase);
            bool existing = enums.ContainsKey(e.Name);
            enums[e.Name] = e;
            dbDefs.Enumerations = enums.Values.ToList();
            var newContent = OutputFormatting.Compactify(JsonConvert.SerializeObject(dbDefs, Formatting.Indented));
            return new FileSaver(_currentEnv.Db.DbDefinition, newContent).SaveIfChanged()
                ? $"EnumDefinition: {e.Name} " + (existing ? "updated." : "added.")
                : "EnumDefinition: Unchanged";
        }


        private string RunRepo(string sourceCode)
        {
            var sb = new StringBuilder();
            sb.AppendLine(RunProjFile(_currentEnv.Repo));
            if (_currentEnv.Repo.Creator.IsNullOrEmpty()) return sb.ToString();

            sb.Append(UpdateCreator());
            return sb.ToString();
        }

        private string UpdateCreator(bool includeContent = false)
        {
            if (_currentEnv.Repo.Creator.IsNullOrEmpty()) return "";
            var def = _currentEnv.GetDbDef();

            var cs = def.Bundles.Select(b => b.Table.Plural).ToList();
            var nonEnums = cs
                .Where(c => def.Enumerations.Any(e => e.Name.EqualsOic(c)))
                .ToList();

            var hcbs = new (string Keyword, string Text)[]
                {
                    ("ScriptOMaticConstructors", Run(cs, CreatorConstructor)),
                    ("ScriptOMaticProperties", Run(cs, CreatorProp, "\r\n")),
                    ("ScriptOMaticTypeLookup", Run(nonEnums, CreatorTypeLookup))
                }
                .Select(t => HandCodedBlock.ShortComment(t.Keyword, t.Text)).ToList();

            var content = File.ReadAllText(_currentEnv.Repo.Creator);
            content = hcbs.Aggregate(content, (txt, b) => b.ApplyTo(txt));
            var changed = new FileSaver(_currentEnv.Repo.Creator, content).SaveIfChanged();
            if (!includeContent)
                return "Creator: " + (changed ? "Updated" : "Unchanged") + "\r\n";
            var sb = new StringBuilder();
            sb
                .AppendLine(content)
                .AppendLine("#################################")
                .AppendLine(changed ? "Updated" : "Unchanged");
            return sb.ToString();
        }

        private string CreatorConstructor(string c) => $"                [typeof({c}Repo)] = ()=>new {c}Repo(this)";
        private string CreatorProp(string c) => $"        public {c}Repo {c} => Get<{c}Repo>();";
        private string CreatorTypeLookup(string c) => $"                [typeof({StringUtil.ToSingle(c)})] = {c}";
        private string Run(IEnumerable<string> classes, Func<string, string> f, string separator = ",\r\n") => classes.Select(f).Join(separator);
        //[typeof(Report)]= Reports
        private string RunRepo_Between(string current, string newLine, string indent, string separator = "") => current
            .SplitLines(true)
            .Select(l => l.Trim().TrimEnd(','))
            .Where(l => !l.IsNullOrWhitespace())
            .ToList()
            .Do(ls => ls.Add(newLine))
            .Select(l => indent + l)
            .OverwritingToDictionary(l => l.RemoveAllWhitespace(), l => l)
            .Values
            .AsSorted()
            .Join(separator + "\r\n") + $"\r\n{indent}";
        private string RunController() => RunProjFile(_currentEnv.Controller);

        private string RunProjFile(ProjectSettings proj)
        {
            var repoName = _currentBundle.Table.Plural;
            var filePath = Path.Combine(proj.Dir, $"{repoName}Repo.cs");
            var fileUpdated = new FileSaver(filePath, GetContent(_currentBundle, ContentType.Repo))
                .WithBlocks(RepoRenderer.Blocks)
                .SaveIfChanged() ? "Added/Updated" : "Unchanged";
            var projUpdated = new CsProject(proj.Project).Add_Compile(filePath).Save() ? "Added" : "Unchanged";
            return $@"{repoName}:
* Content {fileUpdated}
* Project entry {projUpdated}";
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            btnSettings.Enabled = false;
            var frm = new SettingsForm(_settings, cbEnvironment.SelectedItem?.ToString());
            if (frm.ShowDialog() != DialogResult.OK) return;
            SaveSettings();
            btnSettings.Enabled = true;
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            btnCompile.Enabled = false;
            txtResult.Text = string.Empty;
            // var buildProc = new Proc("MsBuild", _currentSettings.MsBuildPath, Path.GetDirectoryName(_currentSettings.ServerSolution), Path.GetFileName(_currentSettings.ServerSolution));
            var supplyChainProc = new Proc("SupplyChain", _settings.SupplyChainPath, Path.GetDirectoryName(_settings.SupplyChainPath), $"all /{_currentEnv.Name}");

            // buildProc.Log += LogToTxt;
            supplyChainProc.Log += LogToTxt;

            Task.Run(async () =>
            {
                //var err = await buildProc.RunAsync();
                //if (null == err)
                //    err = await supplyChainProc.RunAsync();
                var err = await supplyChainProc.RunAsync();

                MessageBox.Show(err == null ? "Done" : err);
                btnCompile.InvokeIfNeeded(b => b.Enabled = true);
            });


        }
        private void LogToTxt(string text)
        {
            txtResult.InvokeIfNeeded(() =>
            {
                txtResult.SelectionStart = txtResult.TextLength;
                txtResult.SelectedText = $"{text}\r\n";
            });
        }

        private void cbEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itm = cbEnvironment.SelectedItem?.ToString();
            _currentEnv = null != itm ? _settings.GetEnvironment(itm) : null;
            _spPage.Env = _currentEnv;
            
            btnRun.Enabled = _currentEnv != null;
            btnCompile.Enabled = _currentEnv != null;
            _bundles = _currentEnv.GetDbDef().Bundles.ToDictionary(b => b.Table.Name);
            _spPage.Bundles = _bundles;
        }

        private void btnDrawSql_Click(object sender, EventArgs e)
        {
            btnDrawSql.Enabled = false;
            txtResult.Text = string.Empty;
            var procs = chkDrawSqlWithSPs.Enabled && chkDrawSqlWithSPs.Checked ? " /procs" : "";
            var outFile = !chkDrawSqlOutputPath.Checked || ctxtDrawSqlPath.Text.IsNullOrWhitespace() ? "" : $" /out={ctxtDrawSqlPath.Text}";
            var drawSqlProc = new Proc("DrawSql", _settings.DrawSqlPath, Path.GetDirectoryName(_settings.DrawSqlPath), $"/envName={_currentEnv.Name} /yes /clear{procs}{outFile}");
            drawSqlProc.Log += LogToTxt;

            Task.Run(async () =>
            {
                var err = await drawSqlProc.RunAsync();
                MessageBox.Show(err ?? "Done");
            });
            btnDrawSql.Enabled = true;

        }

        private void btnUpdateCreator_Click(object sender, EventArgs e)
        {
            
            var res = SQLInjector.SQLInjector.Run(_currentEnv, true, true ,true);
            txtResult.Text = res;
            MessageBox.Show("Done!");
            btnUpdateCreator.Enabled = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //if (_currentEnv.Repo.Creator.IsNullOrEmpty()) return;
            //var dbDefs = File.Exists(_currentEnv.Db.DbDefinition)
            //    ? JsonConvert.DeserializeObject<DbDef>(File.ReadAllText(_currentEnv.Db.DbDefinition))
            //    : new DbDef();
            //var keywords = new[] { "GetBetween", "GetFor" };
            //var sb = new StringBuilder();
            //foreach (var b in dbDefs.Bundles)
            //{
            //    foreach (var p2 in b.AllProcedures())
            //    {
            //        if (!keywords.Any(p2.Name.ContainsOic)) continue;
            //        if (p2.ColFields == null)
            //            sb.AppendLine($"{b.ClassName}.{p2.Name} has no ColFields.");
            //    }
            //}
            //txtResult.Text = sb.ToString();
            btnCheck.Enabled = false;

            var res = SQLInjector.SQLInjector.Run(_currentEnv);
            txtResult.Text = res;
            MessageBox.Show("Done!");
            btnCheck.Enabled = true;
        }

        private void btnDrawSqlBrowse_Click(object sender, EventArgs e)
        {
            btnDrawSqlBrowse.Enabled = false;
            var sfd = new SaveFileDialog() { AddExtension = true, CheckPathExists = true, Filter = "SQL files|*.sql|All files (*.*)|*.*", DefaultExt = ".sql", OverwritePrompt = true, Title = "Please select where to store the generated SQL" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            ctxtDrawSqlPath.Text = sfd.FileName;
            chkDrawSqlOutputPath.Checked = true;
            btnDrawSqlBrowse.Enabled = true;
        }

        private void chkDrawSqlOutputPath_CheckedChanged(object sender, EventArgs e)
        {
            chkDrawSqlWithSPs.Enabled = chkDrawSqlOutputPath.Checked;
        }

        private void ctxtDrawSqlPath_TextChanged(object sender, EventArgs e)
        {
            if (ctxtDrawSqlPath.Text.IsNullOrEmpty())
                chkDrawSqlOutputPath.Checked = false;
        }

        private void tcContent_TabIndexChanged(object sender, EventArgs e)
        {
            if (tcContent.TabPages.Count <= 0) return;
            if (null == _currentBundle) return;
            txtResult.Text = GetContent(_currentBundle, (ContentType) tcContent.SelectedTab.Tag);
        }

        private void tcContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcContent.TabPages.Count <= 0) return;
            if (null == _currentBundle) return;
            try
            {
                txtResult.Text = GetContent(_currentBundle, (ContentType)tcContent.SelectedTab.Tag);
                txtResult.ForeColor = SystemColors.WindowText;
            }
            catch (Exception exception)
            {
                txtResult.Text = exception.DeepToString();
                txtResult.ForeColor = Color.DarkRed;
            }
            
        }

        private void btnUpdateLinkSps_Click(object sender, EventArgs e)
        {
            btnUpdateLinkSps.Enabled = false;
            var sql = new IndentingStringBuilder("  ")
                .AppendObject(new SqlLinks(_currentEnv.GetDbDef().LinkTables))
                .ToString();
            txtResult.Text = RunSql(sql);
            var path = Path.Combine(_currentEnv.Db.RestoreDir, "Structure", "LinkProcedures.sql");
            File.WriteAllText(path, sql);
            btnUpdateLinkSps.Enabled = true;
        }
    }
}
