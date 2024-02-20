using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UtilClasses;
using SupplyChain.Dto;
using UtilClasses.Winforms;
using UtilClasses.Winforms.Extensions;
using UtilClasses.Extensions.Dictionaries;
using UtilClasses.Extensions.Enumerables;

namespace ScriptOMatic
{
    public partial class ReadForMaxEdit : OkCancelForm
    {
        public ReadWithCol Def { get; private set; }
        bool initDone;

        public ReadForMaxEdit(List<ColumnProperties> columns, string marker, ReadWithCol def, bool defaultReturnsList, bool showValue)
        {
            InitializeComponent();
            Text = $"{marker} Editor";
            var names = columns.Select(c => c.Name).ToArray();
            lbColumn.Items.AddRange(names);
            var existing = def?.AllParameters?.ToDictionaryOic(p => p.Name) ?? new DictionaryOic<ParamSpec>();
            var ps = names
                .Select(n => existing.Maybe(n) ?? new ParamSpec() { Name = n })
                .Select(p => SetLvi(new ListViewItem(), p))
                .ForEach(lvi=> lvi.Checked = existing.ContainsKey(lvi.Text))
                .ToArray();
            lvParameters.Items.AddRange(ps);
            Def = def;
            if (def == null)
            {
                Def = new ReadWithCol();
                initDone = true;
                chkReturnsList.Checked = defaultReturnsList;
                return;
            }

            lbColumn.SelectedItem = def.Column;
            chkReturnsList.Checked = def.ReturnsList;

            
            initDone = true;

        }
        protected override void OnOk()
        {
            Def.AllParameters.Clear();
            Def.AllParameters.AddRange(lvParameters.Items.Cast<ListViewItem>().Where(lvi => lvi.Checked).Select(lvi => lvi.Tag as ParamSpec));
            base.OnOk();
        }

        private ListViewItem SetLvi(ListViewItem lvi, ParamSpec ps)
        {
            lvi.Text = ps.Name;
            if (lvi.SubItems.Count < 2)
                lvi.SubItems.Add("");
            lvi.SubItems[1].Text = ps.Mode.GetShortName();
            lvi.Tag = ps;
            return lvi;
        }

        private void ReadForMaxEdit_Load(object sender, EventArgs args)
        {
            tsmiModeParameter.Click += SetModeOnSelected(ParameterMode.Parameter);
            tsmiModeNull.Click += SetModeOnSelected(ParameterMode.Null);
            tsmiModeQualifier.Click += SetModeOnSelected(ParameterMode.Qualifier);
        }

        private void lbColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initDone) return;
            var col = lbColumn.SelectedItem as string;
            Def.Column = col;
        }

        private void chkReturnsList_CheckedChanged(object sender, EventArgs e)
        {
            Def.ReturnsList = chkReturnsList.Checked;
        }


        private EventHandler SetModeOnSelected(ParameterMode mode) => (s, e) =>
        {
            lvParameters.Items.Update()
                .Selected()
                .With<ParamSpec>(ps => ps.Mode = mode)
                .With(SetLvi)
                .Run();
            var hasQualifier = lvParameters.Items
                                   .OfType<ParamSpec>()
                                   .Count(ps => ps.Mode == ParameterMode.Qualifier) >0;
            if(hasQualifier)
                chkReturnsList.CheckState = CheckState.Indeterminate;
            chkReturnsList.Enabled = !hasQualifier;
        };
    }
}

