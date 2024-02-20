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
using UtilClasses.Extensions.Dictionaries;

namespace ScriptOMatic.Pages
{
    public partial class AggregateForm : Form
    {
        readonly TableNode _parent;
        Dictionary<string, TableNode> _tableDict;
        Aggregate _current;
        public AggregateForm(TableNode parent, Aggregate a)
        {
            _parent = parent;
            _current = a;
            InitializeComponent();
            _tableDict = parent.Flatten().Skip(1).ToDictionary(t => t.Name, StringComparer.OrdinalIgnoreCase);
            primaryKeyBS.DataSource = parent.Columns;
            tableBS.DataSource = parent.Children;
            aggregateBS.DataSource = _current;
        }

        private void AggregateForm_Load(object sender, EventArgs e)
        {
            
            TableLookUpEdit.Properties.DataSource = _parent.Children;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TableLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            //_current.Columns = _tableDict.Maybe(TableLookUpEdit.EditValue.ToString())?.Columns;
            //foreignKeyBS.DataSource = _current.Columns;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Validate();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
