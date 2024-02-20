using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScriptOMatic
{
    public partial class AttributeEditor : Form
    {
        public AttributeDef Def
        {
            get => new AttributeDef()
            {
                Column = lcbAttributeColumn.SelectedString,
                Name = ctxtAttributeName.Text,
                Type = ctxtType.Text,
                Style = rbPresentIfTrue.Checked ? AttributeStyle.PresentIfTrue : AttributeStyle.CastedValue
            };
            set
            {
                lcbAttributeColumn.Select(value.Column);
                ctxtAttributeName.Text = value.Name;
                ctxtType.Text = value.Type;
                rbPresentIfTrue.Checked = value.Style == AttributeStyle.PresentIfTrue;
                rbStyleParent.Checked = value.Style == AttributeStyle.CastedValue;
            }
        }
        public AttributeEditor(IEnumerable<ColumnProperties> columns)
        {
            InitializeComponent();
            lcbAttributeColumn.AddRange(columns.Select(c => c.Name));
        }

        private void SomethingChanged(object sender, EventArgs e)
        {
            ctxtType.Enabled = rbStyleParent.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
