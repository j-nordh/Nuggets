using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SupplyChain.Dto;
using UtilClasses.Extensions.Enums;
using UtilClasses.Extensions.Objects;

namespace ScriptOMatic.Pages
{
    public partial class FieldModeSelector : Form
    {
        private class ModeItem
        {
            public ColumnProperties.Modes Mode { get; }

            public ModeItem(ColumnProperties.Modes mode)
            {
                Mode = mode;
            }

            public override string ToString() => Mode.ToString();
        }
        public FieldModeSelector()
        {
            InitializeComponent();
            cmboMode.Items.Clear();
            cmboMode.Items.AddRange(EnumExtensions.Values<ColumnProperties.Modes>().Select(m => new ModeItem(m))
                .Cast<object>().ToArray());
        }

        public ColumnProperties.Modes Mode
        {
            get{ return cmboMode.SelectedItem.As<ModeItem>().Mode;}
            set { cmboMode.SelectedItem = cmboMode.Items.Cast<ModeItem>().FirstOrDefault(m => m.Mode == value); }
        }

        public string Type
        {
            get { return txtType.Text; }
            set { txtType.Text = value; }
        }

        private void cmboMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
