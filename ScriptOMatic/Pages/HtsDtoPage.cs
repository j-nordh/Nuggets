using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UtilClasses;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic.Pages
{
    public partial class HtsDtoPage : GeneratorPage
    {
        private List<ColumnProperties> _columns;
        public HtsDtoPage()
        {
            InitializeComponent();
            _columns = new List<ColumnProperties>();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new DbLoaderForm(Env.Db);
            if (frm.ShowDialog() != DialogResult.OK) return;
            _columns = frm.Columns;
            txtClassName.Text = frm.ViewName.SubstringAfter("tbl");
            if (txtClassName.Text.EndsWith("s"))
                txtClassName.Text = txtClassName.Text.Substring(0, txtClassName.Text.Length - 1);
            RaiseNewContent(ContentType.DtoClass, Generate(), txtClassName.Text);
            chkHasWriteId.Checked = _columns.Any(c => c.Name.EqualsOic("id"));
            chkHasWriteId.Enabled = chkHasWriteId.Checked;
        }

        private string Generate() => IndentingStringBuilder.SourceFileBuilder(
            $"public class {txtClassName.Text}{Implements()}", ctxtNamespace.Text, "", Using())
            .AppendObjects(_columns, c => c.ToDtoString(chkProperties.Checked))
            .Maybe(chkCloneable.Checked, CloneWithConstructor)
            .Maybe(chkStateful.Checked, Hasher)
            .ToString();
        private IEnumerable<string> Using()
        {
            yield return "System";
            if (chkHasWriteId.Checked || chkStateful.Checked || chkCloneable.Checked) yield return "Common.Interfaces";
            if (chkStateful.Checked) { yield return "System.Runtime.Serialization"; yield return "Newtonsoft.Json"; }
        }

        //private IndentingStringBuilder Clone(IndentingStringBuilder sb) => sb
        //    .AppendLines($"public {txtClassName.Text} Clone() => new {txtClassName.Text}()", "{")
        //    .AppendLines(_columns.Select(c => $"{c.CodeName} = {c.CodeName}").Join(",\r\n"))
        //    .Outdent().AppendLine("};");

        private IndentingStringBuilder CloneWithConstructor(IndentingStringBuilder sb) => sb
            .AppendLines($"public {txtClassName.Text}()",
            "{}",
            $"public {txtClassName.Text}({txtClassName.Text} o)",
            "{")
            .AppendObjects(_columns, c => $"{c.CodeName} = o.{c.CodeName}")
            .Outdent()
            .AppendLines("};", "public {txtClassName.Text} Clone() => new {txtClassName.Text}(this);");
            
        private IndentingStringBuilder Hasher(IndentingStringBuilder sb) =>
            sb.AppendLines("public override int GetHashCode()", "{", "var hash = 23;", "unchecked", "{")
            .AppendObjects(_columns, c => c.ToHashString())
            .AppendLines(
                "}",
                "return hash;",
                "}",
                "[JsonIgnore]",
                "public int OriginalStateCode { get; private set; }",
                "[OnDeserialized]",
                "internal void OnDeserialized(StreamingContext sc) => OriginalStateCode = GetStateCode();");

        private string Implements()
        {
            var lst = new List<string>();
            if (chkHasWriteId.Checked) lst.Add("IHasWriteId");
            if (chkStateful.Checked) lst.Add("IStateful");
            if (chkCloneable.Checked) lst.Add($"ICloneable<{txtClassName.Text}>");
            if (_columns.Any(c => c.Name.EqualsOic("id")))
                lst.Add(_columns.Any(c => c.Name.EqualsOic("name")) ? "IHasNameId" : "IHasId");

            return lst.Any() ? ": " + lst.Join(", ") : "";
        }
        private void SomethingChanged(object sender, EventArgs e)
        {
            RaiseNewContent(ContentType.DtoClass, Generate(), txtClassName.Text);
        }
    }
}
