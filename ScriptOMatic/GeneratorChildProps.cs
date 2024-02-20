using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptOMatic
{
    public partial class GeneratorChildProps : Form
    {
        public GeneratorChildProps(IEnumerable<string> masterColumns, IEnumerable<string> childColumns )
        {
            InitializeComponent();
            cmboMasterColumn.Items.Clear();
            cmboMasterColumn.Items.AddRange(masterColumns.Cast<object>().ToArray());

            cmboChildColumn.Items.Clear();
            cmboChildColumn.Items.AddRange(childColumns.Cast<object>().ToArray());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show($"The specified name ({txtName.Text}) is invalid.", @"Invalid name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPrefix.Text))
            {
                MessageBox.Show($"The specified prefix ({txtPrefix.Text}) is invalid.", @"Invalid prefix", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtTarget.Text))
            {
                MessageBox.Show($"The specified prefix ({txtTarget.Text}) is invalid.", @"Invalid prefix", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            DialogResult =DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public string ChildName => txtName.Text;
        public string Prefix => txtPrefix.Text;
        public string Target => txtTarget.Text;
        public string MasterColumn => cmboMasterColumn.Text;
        public string JoinColumn => cmboChildColumn.Text;
    }
}
