using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilClasses.Extensions.Strings;

namespace ScriptOMatic
{
    public partial class CallCompare : Form
    {
        private class Model
        {
            public class Parameter
            {
                public string Name { get; set; }
                public string DbType { get; set; }
                public string DefaultValue { get; set; }
                public string Val1 { get; set; }
                public string Val2 { get; set; }

                public ListViewItem GetListViewItem()
                {
                    return new ListViewItem(new[] {Name, DbType, Val1, Val2});
                }

                public bool IsIdentical()
                {
                    if (Val1 == null)
                    {
                        return Val2 == null;
                    }
                    return Val2 != null && Val1.Equals(Val2);
                }
            }

            public List<Parameter> Parameters { get; private set; }
            public string Name { get; private set; }
            public Model()
            {
                Parameters = new List<Parameter>();
            }

            public void ParseDefinition(string definition)
            {
                var attempt = definition.SubstringAfter("create Procedure").Trim();
                if (attempt.IsNullOrEmpty()) attempt = definition.SubstringAfter("alter procedure").Trim();
                if (attempt.IsNullOrEmpty()) attempt = definition.SubstringAfter("procedure").Trim();
                definition = attempt;
                Name = definition.SubstringBefore("\n").Replace("\r", "").Replace("[dbo].", "").Replace("[", "").Replace("]", "").Trim();
                definition = definition.PurgeComments().SubstringAfter("@");
                definition = definition.SubstringBefore("\r\nAS");
                var ps = definition.Split(new[] {'@', ','}, StringSplitOptions.RemoveEmptyEntries).ToList();
                Parameters.Clear();
                foreach (var p in ps)
                {
                    if (string.IsNullOrWhiteSpace(p)) continue;
                    var parts = p.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var res = new Parameter();
                    foreach (var part in parts)
                    {
                        if (res.Name.IsNullOrEmpty()) res.Name = "@" + part;
                        else if (res.DbType.IsNullOrEmpty()) res.DbType = part;
                        else if (part.StartsWith("(")) res.DbType += part;
                        else if (part.Contains("=")) continue;
                        else res.DefaultValue = part;
                    }
                    Parameters.Add(res);
                }
            }

            public void ParseCall(string txt, bool first)
            {
                if (txt.StartsWith("exec"))
                {
                    txt = txt.Substring(5);
                    string name = txt.SubstringBefore(" ");
                    txt = txt.Substring(name.Length + 1);
                    if (!name.Equals(Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        throw new Exception(
                            "Procedure name in call ({0}) does not match definition ({1}).".FormatWith(name, Name));
                    }

                    var tokens= new Tokenizer(txt);
                    int index = 0;
                    foreach (var token in tokens)
                    {
                        var parts = token.Split('=');
                        Parameter parameter=null;
                        string value="";
                        switch (parts.Length)
                        {
                            case 1:
                                parameter = Parameters[index];
                                ++index;
                                value = parts[0];
                                break;
                            case 2:
                                parameter =
                                    Parameters.First(
                                        p => p.Name.Equals(parts[0], StringComparison.CurrentCultureIgnoreCase));
                                value = parts[1];
                                break;
                        }
                        if (first)
                            parameter.Val1 = value;
                        else
                            parameter.Val2 = value;
                    }
                }   
            }

            private class Tokenizer:IEnumerable<String>
            {
                private readonly string _str;

                public Tokenizer(string str)
                {
                    _str = str;
                }

                public IEnumerator<string> GetEnumerator()
                {
                    bool inSingleQuote = false;
                    bool inDoubleQuote = false;
                    var sb = new StringBuilder();
                    foreach (var c in _str.ToCharArray())
                    {
                        switch (c)
                        {
                            case '\'':
                                if (!inDoubleQuote) inSingleQuote = !inSingleQuote;
                                sb.Append(c);
                                break;
                            case '"':
                                if (!inSingleQuote) inDoubleQuote = !inDoubleQuote;
                                sb.Append(c);
                                break;
                            case ',':
                                if (inSingleQuote || inDoubleQuote)
                                {
                                    sb.Append(c);
                                    continue;
                                }
                                var ret = sb.ToString();
                                sb.Clear();
                                yield return ret;
                                break;
                            default:
                                sb.Append(c);
                                break;
                        }
                    }
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
            }
        }

        private readonly Model _model;
        public CallCompare()
        {
            _model =new Model();
            InitializeComponent();
            txtDefinition.TextChanged += txtDefinition_TextChanged;
            txtCall1.TextChanged += txtCall1_TextChanged;
            txtCall2.TextChanged += txtCall2_TextChanged;
        }

        void txtCall2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _model.ParseCall(txtCall2.Text, false);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nått gick fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void txtCall1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _model.ParseCall(txtCall1.Text, true);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nått gick fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        void txtDefinition_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _model.ParseDefinition(txtDefinition.Text);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nått gick fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        private new void Update()
        {
            lbHeading.Text = _model.Name;
            lvResult.Items.Clear();
            lvResult.Items.AddRange(_model.Parameters
                .Where(p => !chkHideSame.Checked || !p.IsIdentical())
                .Where(p=> !chkHideNull.Checked || !(p.Val1.IsNullWhiteSpaceOr("null") && p.Val2.IsNullWhiteSpaceOr("null")))
                .Where(p=> !chkHideDefault.Checked || !(p.Val1.IsNullWhiteSpaceOr("default") && p.Val2.IsNullWhiteSpaceOr("default")))
                .Select(p => p.GetListViewItem())
                .ToArray());
            txtCall1.Enabled = _model.Parameters.Count > 0;
            txtCall2.Enabled = _model.Parameters.Count > 0;
            
        }
        private void chkHide_CheckedChanged(object sender, EventArgs e)
        {
            Update();
        }
    }

    static class OtherExtensions
    {
        //public static string SkipUntil(this string str, string needle, bool ignoreCase=true, bool skipNeedle=true)
        //{
        //    var start = str.IndexOf(needle,
        //        ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
        //    if (start ==-1) return "";

        //    if (skipNeedle) start += needle.Length;
        //        return str.Substring(start);
        //}
        //public static string TakeUntil(this string str, string needle, bool ignoreCase = true)
        //{
        //    var end = str.IndexOf(needle,
        //        ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
        //    return end == -1 ? str : str.Substring(0, end);
        //}

        public static string PurgeComments(this string str)
        {
            var sb = new StringBuilder(str.Length);
            using (var rdr = new StringReader(str))
            {
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    var commentStart = line.IndexOf(@"--", StringComparison.CurrentCulture);
                    if (commentStart >= 0)
                    {
                        line = line.Substring(0, commentStart);
                    }
                    line = line.Trim();
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        public static bool IsNullWhiteSpaceOr(this string str, string needle, bool ignoreCase=true)
        {
            return str.IsNullOrWhitespace() ||
                   str.Equals(needle,
                       ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        
        public static string FormatWith(this string str, params object[] ps)
        {
            return string.Format(str, ps);
        }
    }
}
