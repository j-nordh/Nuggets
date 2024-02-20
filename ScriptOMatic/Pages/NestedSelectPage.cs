using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Pages
{
    public partial class NestedSelectPage : GeneratorPage
    {
        private readonly List<Child> _children;
        
        public NestedSelectPage()
        {
            InitializeComponent();
            _children = new List<Child>();
        }
        private class Child
        {
            public Child(string name, string prefix, string target, List<string> columns, string masterColumn, string joinColumn)
            {
                Name = name;
                Prefix = prefix;
                Target = target;
                Columns = columns;
                MasterColumn = masterColumn;
                JoinColumn = joinColumn;
            }

            public string Name { get; }
            public string Prefix { get; }
            public string Target { get; }
            public List<string> Columns { get; }
            public string MasterColumn { get; }
            public string JoinColumn { get; }
        }


        private void lstvChildren_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || lstvChildren.SelectedItems.Count <= 0) return;
            lstvChildren.SelectedItems.Cast<ListViewItem>()
                .Select(itm => itm.Tag).Cast<Child>()
                .ToList()
                .ForEach(c => _children.Remove(c));
            UpdateChildList();
        }
        private void UpdateChildList()
        {
            lstvChildren.BeginUpdate();
            lstvChildren.Items.Clear();
            lstvChildren.Items.AddRange(_children.Select(c => new ListViewItem(new[] { c.Name, c.Prefix }) { Tag = c }).ToArray());
            lstvChildren.EndUpdate();
        }

        //public override void OnMasterDrop(string text)
        //{
        //    _masterColumns = text.SplitREE(", ");
        //    Regenerate();
        //}

        //public override void OnChildDrop(string text)
        //{
        //    var childColumns = text.SplitREE(", ");

        //    var props = new GeneratorChildProps(_masterColumns, childColumns);
        //    if (props.ShowDialog(this) == DialogResult.OK)
        //    {
        //        _children.Add(new Child(props.ChildName, props.Prefix, props.Target, childColumns.ToList(),
        //            props.MasterColumn, props.JoinColumn));
        //    }
        //    Regenerate();
        //}

        private void Regenerate()
        {
            var sb = new StringBuilder();

            if (_masterColumns.IsNullOrEmpty())
            {
                RaiseNewContent(ContentType.NotSpecified, "No master provided");
                return;
            }

            if (chkForXml.Checked)
            {
                sb.Append(@"SELECT(ISNULL((
  SELECT 
    'LOADED' AS '@updatestate', ");
            }
            else
            {
                sb.Append("SELECT ");
            }

            sb.Append($"{txtMasterPrefix.Text}." + string.Join($", {txtMasterPrefix.Text}.", _masterColumns));
            foreach (var child in _children)
            {
                if (chkForXml.Checked)
                {
                    sb.AppendLine(",").Append(
                        $"case when {child.Prefix}.{child.JoinColumn} IS NULL then null else 'LOADED' end  AS '{child.Target}/@updatestate'");
                }
                foreach (var col in child.Columns)
                {
                    sb.AppendLine(",")
                        .Append(child.Prefix)
                        .Append('.')
                        .Append(col)
                        .Append(" AS '")
                        .Append(child.Target)
                        .Append("/")
                        .Append(col)
                        .Append("' ");
                }

            }
            sb.AppendLine($"FROM {txtMasterTable.Text} AS {txtMasterPrefix.Text}");
            foreach (var child in _children)
            {
                sb.AppendLine(
                    $"LEFT JOIN {child.Name} AS {child.Prefix} ON {txtMasterPrefix.Text}.{child.MasterColumn} = {child.Prefix}.{child.JoinColumn}");
            }

            if (chkForXml.Checked)
            {
                sb.Append(
                    $@"FOR xml PATH('{txtElementName.Text}'), TYPE),''))
FOR xml PATH('{txtRootName.Text
                        }'), ELEMENTS");
            }
            RaiseNewContent(ContentType.Sql, sb.ToString());
        }

        private void chkForXml_CheckedChanged(object sender, EventArgs e)
        {
            txtElementName.Enabled = chkForXml.Checked;
            txtRootName.Enabled = chkForXml.Checked;
            Regenerate();
        }

        private void txtElementName_TextChanged(object sender, EventArgs e)
        {
            Regenerate();
        }

        private void txtRootName_TextChanged(object sender, EventArgs e)
        {
            Regenerate();
        }
    }
}
