using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SupplyChain.Dto;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SourceFuLib;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Winforms;

namespace ScriptOMatic
{
    public partial class SettingsForm : Form
    {
        readonly SourceFuSettings _settings;

        CodeEnvironment _current;
        public SettingsForm(SourceFuSettings settings, string current)
        {
            _settings = settings;
            InitializeComponent();
            NamespaceMapButtonEdit.ButtonClick += HandleOpenFile("namespace map", "Namespace maps (*.txt)|*.txt");
            DbDefinitionButtonEdit.ButtonClick += HandleOpenFile("database defintion", "DbDefinitions (*.json)|*.json");
            ProjectButtonEdit.ButtonClick += HandleOpenProject("DTO");
            beControllerProj.ButtonClick += HandleOpenProject("Controller");
            beRepoProj.ButtonClick += HandleOpenProject("Repository");
            DirButtonEdit.ButtonClick += HandleOpenDir("DTO");
            beControllerDir.ButtonClick += HandleOpenDir("Controller");
            beRepoDir.ButtonClick += HandleOpenDir("Repo");
            RefreshEnvironments(current);

        }
        private void RefreshEnvironments(string selected = null)
        {
            cbEnvironment.Items.AddRange(_settings.Environments.Keys.AsSorted().Cast<object>().ToArray());
            if (null == selected)
            {
                cbEnvironment.SelectedIndex = 0;
                return;
            }
            for (int i = 0; i < cbEnvironment.Items.Count; i++)
            {
                if (!cbEnvironment.Items[i].ToString().Equals(selected)) continue;
                cbEnvironment.SelectedIndex = i;
                break;
            }
        }

        private void SetCurrent(string current)
        {
            _current = _settings.GetEnvironment(current);
            if (null == _current) return;
            bsDbSettings.DataSource = _current.Db;
            bsDtoSettings.DataSource = _current.Dto;
            bsSettings.DataSource = _current;
            bsRepoSettings.DataSource = _current.Repo;
            bsControllerSettings.DataSource = _current.Controller;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Validate();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DirButtonEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
        private ButtonPressedEventHandler HandleOpenProject(string title) => (s, e) => HandleOpenFile($"{title} project", "CSharp projects (*.csproj)|*.csproj");
        private ButtonPressedEventHandler HandleOpenFile(string title, string filter) => (s, e) => OpenFile((ButtonEdit)s, $"Please select the {title} file", filter);
        private void OpenFile(ButtonEdit edit, string title, string filter)
        {

            var dlg = new OpenFileDialog()
            {
                InitialDirectory = edit.Text.IsNullOrWhitespace()
                ? Application.StartupPath
                : File.Exists(edit.Text)
                    ? Path.GetDirectoryName(edit.Text)
                    : edit.Text,
                Title = title,
                Multiselect = false,
                Filter = filter
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            edit.Text = dlg.FileName;
        }
        private ButtonPressedEventHandler HandleOpenDir(string title) => (s, e) => OpenDir((ButtonEdit)s, $"Please select the {title} directory");
        private void OpenDir(ButtonEdit edit, string title)
        {
            var dlg = new FolderBrowserDialog()
            {
                Description = title,
                ShowNewFolderButton = false,
                SelectedPath = edit.Text.IsNullOrWhitespace() ? Application.StartupPath : edit.Text
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            edit.Text = dlg.SelectedPath;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var frm = new InputForm(MessageBoxButtons.OKCancel, "What should the new environment be called?", "Create environment");
            if (frm.ShowDialog() != DialogResult.OK) return;
            var name = frm.Value.ToString();
            if (_settings.Environments.ContainsKey(name))
            {
                MessageBox.Show("The selected name is already in use.", "Invalid name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetCurrent(name);
                return;
            }

            var sfd = new SaveFileDialog
            {
                FileName = "CodeEnvironment.json",
                DefaultExt = "json",
                OverwritePrompt = true,
                Title = "Please select where the environment should be saved"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var path = sfd.FileName;
            _settings.CreateEnvironment(name, path);
            RefreshEnvironments(name);
        }

        private void cbEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCurrent(cbEnvironment.SelectedItem?.ToString());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _settings.Environments.Remove(_current.Name);
            RefreshEnvironments();
        }
    }
}
