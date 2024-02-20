
using Newtonsoft.Json;
using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using SourceFuLib;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Extensions.Strings;
using UtilClasses.Extensions.Objects;
namespace ScriptOMatic
{
    public partial class CrudStudio : Form
    {
        SourceFuSettings _settings;
        CodeEnvironment _env;
        Dictionary<string, TableInfo> _tables;
        public CrudStudio()
        {
            InitializeComponent();
            _tables = new Dictionary<string, TableInfo>(StringComparer.OrdinalIgnoreCase);
        }

        private void lddlEnvironment_SelectedIndexChanged()
        {
            _env = lddlEnvironment.GetSelected<CodeEnvironment>();
            tvTables.Nodes.Clear();
            var conn = new SqlConnection($"Server={_env.Db.Server};Database={_env.Db.Name};Trusted_Connection=True;");
            conn.Open();
            conn
                .Query<string>("select TABLE_NAME from INFORMATION_SCHEMA.TABLES")
                .Select(t => _tables.GetOrAdd(t, CreateInfo))
                .Select(ti => new TreeNode(ti.Plural).Do(n => n.Tag = ti))
                .ForEach(n=>tvTables.Nodes.Add(n));
        }

        private TableInfo CreateInfo(string tableName) => new TableInfo()
        {
            Name = tableName,
            Plural = tableName.SubstringAfter("tbl"),
            Singular = GetSingleName(tableName)
        };
        private void CrudStudio_Load(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ScriptOMatic", "Settings.json");
            _settings = JsonConvert.DeserializeObject<SourceFuSettings>(File.ReadAllText(path, Encoding.UTF8));
            lddlEnvironment.ClearItems();
            lddlEnvironment.AddRange(_settings.Environments.Values);
            var env = ArgParser.Env;
            if (env.IsNullOrWhitespace()) return;
            lddlEnvironment.Select(env);
        }

        protected string GetSingleName(string plural)
        {
            if (plural.StartsWith("tbl"))
                plural = plural.SubstringAfter("tbl");
            if (plural.EndsWithOic("ies"))
                return plural.Substring(0, plural.Length - 3) + "y";
            if (plural.EndsWithOic("es"))
                return plural.Substring(0, plural.Length - 2);
            if (plural.EndsWithIc2("s"))
                return plural.Substring(0, plural.Length - 1);
            return plural;
        }
    }
}
