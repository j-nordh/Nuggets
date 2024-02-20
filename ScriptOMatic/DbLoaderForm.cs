using ScriptOMatic.Pages;
using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using ScriptOMatic.Generate;
using SupplyChain.Dto.Extensions;
using UtilClasses;
using UtilClasses.Db.Extensions;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Exceptions;
using UtilClasses.Extensions.MathExtensions;
using UtilClasses.Extensions.Objects;
using UtilClasses.Extensions.Strings;
using SortOrder = System.Windows.Forms.SortOrder;

namespace ScriptOMatic
{
    public partial class DbLoaderForm : Form
    {
        private readonly CodeEnvironment _env;

        public DbLoaderForm(CodeEnvironment env)
        {
            _env = env;
            var settings = env.Db;
            InitializeComponent();
            Columns = null;

            if(settings.DbDefinition.IsNotNullOrEmpty())
            
            if (null == settings?.Server) return;
            txtServer.Text = settings.Server;
            btnConnect_Click(btnConnect, null);


            if (null == settings.Name) return;
            var db = cmboDatabases.Items.Cast<string>().FirstOrDefault(s => s.EqualsIc2(settings.Name));
            cmboDatabases.SelectedItem = db;

            
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                cmboDatabases.Items.Clear();
                using (var con = new SqlConnection($"Data Source={txtServer.Text}; Integrated Security=True;"))
                {
                    con.Open();
                    DataTable databases = con.GetSchema("Databases");
                    foreach (DataRow database in databases.Rows)
                    {
                        cmboDatabases.Items.Add(database.Field<String>("database_name"));
                    }
                }
                var enable = cmboDatabases.Items.Count > 0;
                lblDb.Enabled = enable;
                cmboDatabases.Enabled = enable;
            }
            catch (Exception exception)
            {
                MessageBox.Show("This went wrong:\r\n" + exception.DeepToString());
            }

        }

        private void cmboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lstVSources.Items.Clear();
                using (SqlConnection connection = new SqlConnection($"Data Source={txtServer.Text}; Database={cmboDatabases.SelectedItem}; Integrated Security=True;"))
                {
                    connection.Open();
                    DataTable schema = connection.GetSchema("Tables");
                    foreach (DataRow row in schema.Rows)
                    {
                        lstVSources.Items.Add(row[2].ToString());
                    }
                    lstVSources.Sort();
                }
                lstVSources.Sorting = SortOrder.Ascending;
                lstVSources.Sort();
                groupBox1.Enabled = lstVSources.Items.Count > 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show("This went wrong:\r\n" + exception.DeepToString());
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                ViewName = lstVSources.SelectedItems[0].Text;
                var loader = new DbLoader(_env);
                loader.Init(nudDepth.Value.RoundInt());
                Columns = loader.Columns(ViewName);
                RootNode = loader.GetNode(ViewName);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("This went wrong:\r\n" + exception.DeepToString());
                Columns = null;
            }
        }

       


        public string ViewName { get; private set; }

        public List<ColumnProperties> Columns { get; private set; }
        public TableNode RootNode { get; private set; }

        //public TableNode Relations => GetNode(lstVSources.SelectedItems[0].Text, null, null);

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstVSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = lstVSources.SelectedItems.Count > 0;
        }

        private void lstVSources_DoubleClick(object sender, EventArgs e)
        {
            btnOk_Click(sender, e);

        }
    }
}
