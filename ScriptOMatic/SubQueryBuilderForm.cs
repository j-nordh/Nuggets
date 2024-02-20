using ScriptOMatic.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ScriptOMatic.Generate.Appendable;
using SupplyChain.Dto;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic
{
    public partial class SubQueryBuilderForm : Form
    {
        private readonly TableNode _source;
        private HashSet<string> _aliases;
        public SubQuery Query;

        public SubQueryBuilderForm(TableNode source)
        {
            InitializeComponent();
            _source = source;
            Query = new SubQuery();
        }

        public SubQueryBuilderForm(TableNode source, SubQuery q): this(source)
        {
            Query = q;
            ltxtQueryName.Text = q.Name;
        }

        private void SubQueryBuilderForm_Load(object sender, EventArgs e)
        {
            _aliases = new HashSet<string>();
            SetEmptyAliases(_source);
            lcmboSelectedTable.ClearItems();
            lcmboSelectedTable.AddRange(_source.Flatten().Skip(1));

        }


        private void lcmboSelectedTable_Load(object sender, EventArgs e)
        {

        }

        private List<TableNode> GetPath(TableNode from, List<TableNode> path = null)
        {
            if (null == path) path = new List<TableNode>();
            path.Add(from);
            if (from == lcmboSelectedTable.GetSelected<TableNode>()) return path;
            foreach (var child in from.Children)
            {
                var ret = GetPath(child, path);
                if (ret != null) return ret;
            }
            path.Remove(from);
            return null;
        }

        private void lcmboSelectedTable_SelectedIndexChanged()
        {
            //var path = GetPath(_source);
            //treeView1.Nodes.Clear();
            //treeView1.Nodes.AddRange(path.Select(n => n.CloneWithoutChildren().ToNode()).ToArray());
            //treeView1.ExpandAll();
            //ltxtQueryName.Text = lcmboSelectedTable.GetSelected<TableNode>().Name.SubstringAfter("tbl").TrimEnd('s');
        }

        public void SetEmptyAliases(TableNode node)
        {
            if(node.Alias.IsNullOrEmpty())
            {
                var alias = new string(node.Name.SubstringAfter("tbl").Where(char.IsUpper).ToArray());
                var attempt = alias;
                int count = 1;
                while (_aliases.Contains(attempt))
                {
                    attempt = alias + count;
                    count++;
                }
                node.Alias = attempt;
            }
            
            foreach (var n in node.Children)
            {
                SetEmptyAliases(n);
            }
        }

        private void ltxtQueryName_Load(object sender, EventArgs e)
        {

        }

        private void ltxtQueryName_TextChanged(object sender, EventArgs e)
        {
            Query.Name = ltxtQueryName.Text;
            rtxtQuery.Text = new AppendableSubQuery(Query, _source.Name).Render();
        }

        private void btnOK_Click(object sender, EventArgs e)
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
