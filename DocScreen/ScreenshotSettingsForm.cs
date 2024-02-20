using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocScreen
{
    public partial class ScreenshotSettingsForm : Form
    {
        public string LastDestination { get; private set; }
        public ScreenshotSettingsForm()
        {
            InitializeComponent();
        }

        private void ScreenshotSettingsForm_Load(object sender, EventArgs e)
        {

        }

        public void Set(string name , DestinationUiConfig cfg)
        {
            LastDestination = name;
            gbName.Enabled = cfg.ShowName;
            gbPath.Enabled = cfg.ShowPath;

            flpOptions.Controls.Clear();
            flpOptions.Controls.AddRange(cfg.Checkboxes
                .Select(s => new CheckBox { Text = s })
                .ToArray());
        }
        public void Get(Screenshot s)
        {
            if (gbPath.Enabled)
                s.Path = txtPath.Text;
            if (gbName.Enabled)
                s.Name = txtName.Text;
            foreach(var c in flpOptions.Controls.OfType<CheckBox>())
            {
                s.Checkboxes[c.Text] = c.Checked;
            }
        }
    }
}
