using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptOMatic.Generate;
using SupplyChain.Dto;
using UtilClasses.Extensions.Strings;
using UtilClasses;

namespace ScriptOMatic.Pages
{
    public partial class TableSelectPage : GeneratorPage
    {
        protected string _tableName;
        protected List<ColumnProperties> _columns;
        protected TableNode _rootNode;


        protected event Action TableChanged;
        public TableSelectPage()
        {
            InitializeComponent();
        }

        private void TableSelectPage_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var frm = new DbLoaderForm(Env)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            LoadTable(frm.ViewName, frm.Columns, frm.RootNode);
        }

        public void LoadTable(string tbl, List<ColumnProperties> cols, TableNode rootNode)
        {
            lblTable.Text = tbl;
            ctxtPluralName.Text = StringUtil.FixPluralization(tbl);
            ctxtSingularName.Text = StringUtil.ToSingle(tbl);
            _tableName = tbl;
            _columns = cols;
            _rootNode = rootNode;
            TableChanged?.Invoke();
        }

        public void LoadTable(string tbl)
        {
            var loader = new DbLoader(Env);
            loader.Init(2);
            LoadTable(tbl, loader.Columns(tbl), loader.GetNode(tbl));
        }
    }
}
