using SupplyChain.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic
{
    public partial class JsonFieldForm : Form
    {
        private readonly CodeEnvironment _env;

        public JsonField Field => new JsonField()
        {
            Column = lddColumn.SelectedString,
            Type = chkList.Checked ? $"List<{lddType.SelectedString}>" : lddColumn.SelectedString,
            Alias = ltxtAlias.Text
        };

        public JsonFieldForm(CodeEnvironment env, IEnumerable<ColumnProperties> columns)
        {
            InitializeComponent();
            _env = env;
            lddColumn.AddRange(columns.Where(c => c.Type.EqualsOic("nvarchar")).Select(c => c.Name));
            lddType.AddRange(File.ReadAllLines(env.Dto.NamespaceMap)
                .SkipWhile(l => !l.StartsWithOic(env.Dto.Namespace))
                .Skip(1)
                .TakeWhile(l => l.Length > 0 && Char.IsWhiteSpace(l[0]))
                .Select(l=>l.Trim()).ToArray());
        }

        public JsonFieldForm(CodeEnvironment env, IEnumerable<ColumnProperties> columns, JsonField field): this(env,columns)
        {
            lddColumn.Select(field.Column);
            lddType.Select(field.Type.StripAllGenerics());
            chkList.Checked = field.Type.StartsWithOic("List<");
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

        private void JsonFieldForm_Load(object sender, EventArgs e)
        {

        }

        private void lddColumn_Load(object sender, EventArgs e)
        {
            ltxtAlias.Text = lddColumn.SelectedObject.ToString();
        }
    }
}
