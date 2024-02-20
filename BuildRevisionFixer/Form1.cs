using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilClasses.Extensions.Strings;

namespace BuildRevisionFixer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var ass = Assembly.GetEntryAssembly();
            var txt = $"The file version of entry was: {GetFileVersion(ass)}";
        }

        public static string GetFileVersion(Assembly assembly)
        {

            var value = assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
                .OfType<AssemblyFileVersionAttribute>()
                .SingleOrDefault();

            return value != null ? value.Version : "?.?.?.?";
        }

        public static string GetAssemblyVersion(Assembly assembly)
        {

            var value = assembly.GetCustomAttributes(typeof(AssemblyVersionAttribute), false)
                .OfType<AssemblyVersionAttribute>()
                .SingleOrDefault();

            return value != null ? value.Version : "?.?.?.?";
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                InitialDirectory = "C:\\",
                Filter = "Excel files (*.csproj)|*.csproj",
                Title = "Please specify a project file",
                FilterIndex = 0,
                RestoreDirectory = true,
                CheckFileExists = true
            };

            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            txtFileDestination.Text = dlg.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var dirPath = Path.GetDirectoryName(txtFileDestination.Text);
                if (!Directory.Exists(dirPath))
                {
                    MessageBox.Show("The specified path does not seem to point to a file in an existing directory...",
                        "Could not find directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(txtFileDestination.Text))
                {
                    MessageBox.Show("The specified path does not seem to point to an existing file",
                        "Could not find file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Path.GetExtension(txtFileDestination.Text).EndsWithOic("csproj"))
                {
                    MessageBox.Show("Not a valid file extension on destination file", "Error in input",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("The specified path does not seem to point to an existing file",
                    "Could not find file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Path was ok, run program
            try
            {
                BuildRevisionFixer.Run(txtFileDestination.Text, majorTextbox.AsInteger, minorTextbox.AsInteger,
                    buildTextbox.AsInteger);

                MessageBox.Show($"The run completed succesfully for file: {txtFileDestination.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception occured: {ex.Message} \n" + $"Stacktrace: {ex.StackTrace}", 
                    "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
