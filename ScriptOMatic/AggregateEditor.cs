using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SupplyChain.Dto;
using UtilClasses.Extensions.Enumerables;
using UtilClasses.Winforms;

namespace ScriptOMatic
{
    public partial class AggregateEditor : OkCancelForm
    {
        public Aggregate Agg;
        public AggregateEditor(Aggregate a)
        {

            InitializeComponent();
            ltxtTable.Text = a.Table;
            ltxtAlias.Text = a.Alias;
            ltxtRepo.Text = a.Repo;
            ltxtTargetType.Text = a.TargetType;
            chkReadonly.Checked = a.ReadOnly;
            ltxtShortName.Text = a.ShortName ?? a.Table.Where(char.IsUpper).AsString();
            Agg = a;
        }

        protected override void OnOk()
        {
            Agg.Table = ltxtTable.Text;
            Agg.Alias = ltxtAlias.Text;
            Agg.Repo = ltxtRepo.Text;
            Agg.ReadOnly = chkReadonly.Checked;
            Agg.TargetType = ltxtTargetType.Text;
            Agg.ShortName = ltxtShortName.Text;
            base.OnOk();
        }

        private void AggregateEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
