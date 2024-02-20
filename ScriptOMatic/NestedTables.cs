using System;
using System.Windows.Forms;
using ScriptOMatic.Pages;
using SupplyChain.Dto;

namespace ScriptOMatic
{
    public partial class NestedTables : UserControl
    {
        
        private TableNode _selectedNode = null;
        private ColumnProperties _selectedColumn;
        public NestedTables()
        {
            InitializeComponent();
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            gbTable.Enabled = false;
            gbColumn.Enabled = false;
            switch (e.Node.Tag)
            {
                case TableNode tn:
                    _selectedNode = tn;
                    gbTable.Enabled = true;
                    txtAlias.Text = tn.Alias;
                    txtPropName.Text = tn.FieldName;
                    break;
                case ColumnProperties cp:
                    _selectedColumn = cp;
                    gbColumn.Enabled = true;
                    //chkIncludeColumn.Checked = cp.Mode != ColumnProperties.Modes.Omit;
                    break;
            }
        }

        private void txtPropName_TextChanged(object sender, EventArgs e)
        {
            _selectedNode.FieldName = txtPropName.Text;
            Updated?.Invoke(this,null);
        }

        public EventHandler Updated;

        private void txtAlias_TextChanged(object sender, EventArgs e)
        {
            _selectedNode.Alias = txtAlias.Text;
            Updated?.Invoke(this, null);
        }

        private void chkIncludeColumn_CheckedChanged(object sender, EventArgs e)
        {
            //_selectedColumn.Mode = chkIncludeColumn.Checked ? ColumnProperties.Modes.Like : ColumnProperties.Modes.Omit;
            Updated?.Invoke(this, null);
        }

        private void NestedTables_Load(object sender, EventArgs e)
        {
            gbTable.Enabled = false;
            gbColumn.Enabled = false;
        }

        public void Set(TreeNode root)
        {
            tree.Nodes.Clear();
            tree.Nodes.Add(root);
            tree.ExpandAll();
        }
    }
}
